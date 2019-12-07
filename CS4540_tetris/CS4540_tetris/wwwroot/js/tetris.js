/// <summary>
///  Author:    Tetrominoes Team
///  Date:      12/6/2019
///  Course:    CS 4540, University of Utah, School of Computing
/// Copyright: CS 4540 and Tetrominoes Tesm - This work may not be copied for use in Academic Coursework.

/// We, Tetrominoes Team, certify that we wrote this code from scratch and did not copy it in part or whole from
/// another source.  Any references used in the completion of the assignment are cited in my README file.
/// Purpose: The purpose of this document is to handle the tetris game
/// </summary>

const SQ = 20;
const VACANT = "white";

//matrices for design for bar being in different orientations
const I = [
    [
        [0, 0, 0, 0],
        [1, 1, 1, 1],
        [0, 0, 0, 0],
        [0, 0, 0, 0],
    ],
    [
        [0, 0, 1, 0],
        [0, 0, 1, 0],
        [0, 0, 1, 0],
        [0, 0, 1, 0],
    ],
    [
        [0, 0, 0, 0],
        [0, 0, 0, 0],
        [1, 1, 1, 1],
        [0, 0, 0, 0],
    ],
    [
        [0, 1, 0, 0],
        [0, 1, 0, 0],
        [0, 1, 0, 0],
        [0, 1, 0, 0],
    ]
];

//matrices for design for L being in different orientations
const J = [
    [
        [1, 0, 0],
        [1, 1, 1],
        [0, 0, 0]
    ],
    [
        [0, 1, 1],
        [0, 1, 0],
        [0, 1, 0]
    ],
    [
        [0, 0, 0],
        [1, 1, 1],
        [0, 0, 1]
    ],
    [
        [0, 1, 0],
        [0, 1, 0],
        [1, 1, 0]
    ]
];

//matrices for design for L being in different orientations
const L = [
    [
        [0, 0, 1],
        [1, 1, 1],
        [0, 0, 0]
    ],
    [
        [0, 1, 0],
        [0, 1, 0],
        [0, 1, 1]
    ],
    [
        [0, 0, 0],
        [1, 1, 1],
        [1, 0, 0]
    ],
    [
        [1, 1, 0],
        [0, 1, 0],
        [0, 1, 0]
    ]
];

//matrices for design for square
const O = [
    [
        [0, 0, 0, 0],
        [0, 1, 1, 0],
        [0, 1, 1, 0],
        [0, 0, 0, 0],
    ]
];

//matrices for design for S being in different orientations
const S = [
    [
        [0, 1, 1],
        [1, 1, 0],
        [0, 0, 0]
    ],
    [
        [0, 1, 0],
        [0, 1, 1],
        [0, 0, 1]
    ],
    [
        [0, 0, 0],
        [0, 1, 1],
        [1, 1, 0]
    ],
    [
        [1, 0, 0],
        [1, 1, 0],
        [0, 1, 0]
    ]
];

//matrices for design for bar with line being in different orientations
const T = [
    [
        [0, 1, 0],
        [1, 1, 1],
        [0, 0, 0]
    ],
    [
        [0, 1, 0],
        [0, 1, 1],
        [0, 1, 0]
    ],
    [
        [0, 0, 0],
        [1, 1, 1],
        [0, 1, 0]
    ],
    [
        [0, 1, 0],
        [1, 1, 0],
        [0, 1, 0]
    ]
];

//matrices for design for Z being in different orientations
const Z = [
    [
        [1, 1, 0],
        [0, 1, 1],
        [0, 0, 0]
    ],
    [
        [0, 0, 1],
        [0, 1, 1],
        [0, 1, 0]
    ],
    [
        [0, 0, 0],
        [1, 1, 0],
        [0, 1, 1]
    ],
    [
        [0, 1, 0],
        [1, 1, 0],
        [1, 0, 0]
    ]
];

//the color of the pieces
const PIECES = [
    [Z, "red"],
    [S, "green"],
    [T, "yellow"],
    [O, "blue"],
    [L, "purple"],
    [I, "cyan"],
    [J, "orange"]
];

//handle a tetromino piece has it moves about the board
class Piece {
    //constructor for a tetromino piece
    constructor(tetromino, color) {
        this.tetromino = tetromino;
        this.color = color;

        this.current = 0;

        this.x = 3;
        this.y = -2;
    }

    // get the current orientation
    getPiece() {
        return this.tetromino[this.current];
    }

    //get tetromino aray
    getTetromino() {
        return this.tetromino;
    }

    //get curent tetromino index
    getCurrent() {
        return this.current;
    }

    //get where piece is on x axis
    getX() {
        return this.x;
    }

    //get where piece is on y axis
    getY() {
        return this.y;
    }

    //update piece's x
    updateX(delta) {
        this.x += delta;
    }

    //update piece's y
    updateY(delta) {
        this.y += delta;
    }

    //get piece's color
    getColor() {
        return this.color;
    }

    //check if the cell[row][col] is empty
    isEmpty(row, col) {
        if (row < 0 || row >= this.tetromino[this.current].length)
            return false;
        if (row < 0 || row >= this.tetromino[this.current].length)
            return false;

        return !this.tetromino[this.current][row][col];
    }

    //get tetromino size
    getLength() {
        return this.tetromino[this.current].length;
    }

    //change current to the next index
    nextPattern() {
        this.current = (this.current + 1) % this.tetromino.length;
    }

    //change current to the prev index
    prevPattern() {
        this.current = (this.current + this.tetromino.length - 1) % this.tetromino.length;
    }
}

//a class to handle the tetris game
class Tetris {
    constructor(num_rows, num_cols, cvs, is_multi = false) {
        // Limit num_rows between [10-30] 
        if (num_rows > 30) {
            num_rows = 30;
        }
        if (num_rows < 10) {
            num_rows = 10;
        }

        // Limit num_cols between [10-20]
        if (num_cols > 20) {
            num_cols = 20;
        }
        if (num_cols < 10) {
            num_cols = 10;
        }

        this.ctx = cvs.getContext("2d");

        this.num_rows = num_rows;
        this.num_cols = num_cols;

        this.board = [];
        this.clearBoard();

        this.scorestreak = 0;
        this.score = 0;
        this.piece = this.randomPiece();
        this.drawBoard();
        this.gameOver = false;

        var date = new Date();
        this.startTime = date.getTime();
        this.is_multi = is_multi;
    }

    //function used to draw a square
    drawSquare(x, y, color, offset = 0) {
        this.ctx.fillStyle = color;
        this.ctx.fillRect(x * SQ + offset, y * SQ, SQ, SQ);

        this.ctx.strokeStyle = "Black";
        this.ctx.strokeRect(x * SQ + offset, y * SQ, SQ, SQ);
    }

    //draw the empty board
    drawEmpty() {
        for (var r = 0; r < this.num_rows; r++) {
            for (var c = 0; c < this.num_cols; c++) {
                this.drawSquare(c, r, VACANT);
            }
        }
    }

    //draw the board
    drawBoard() {
        for (var r = 0; r < this.num_rows; r++) {
            for (var c = 0; c < this.num_cols; c++) {
                this.drawSquare(c, r, this.board[r][c]);
            }
        }
    }

    //draw the moving piece
    drawPiece() {
        this.drawEmpty();
        this.drawBoard();
        var piece = this.piece.getPiece();
        for (var r = 0; r < piece.length; r++) {
            for (var c = 0; c < piece.length; c++) {
                if (this.piece.isEmpty(r, c)) {
                    continue;
                }
                this.drawSquare(c + this.piece.getX(), r + this.piece.getY(), this.piece.getColor());
            }
        }
    }

    //clear the board data
    clearBoard() {
        for (var r = 0; r < this.num_rows; r++) {
            this.board[r] = [];
            for (var c = 0; c < this.num_cols; c++) {
                this.board[r][c] = VACANT;
            }
        }
    }

    //clear the row r
    clearRow(r) {
        if (r >= this.num_rows) {
            return;
        }

        for (var c = 0; c < this.num_cols; c++) {
            this.board[r][c] = VACANT;
        }

        for (; r > 0; r--) {
            this.board[r] = this.board[r - 1];
        }

        for (var c = 0; c < this.num_cols; c++) {
            this.board[0][c] = VACANT;
        }

        this.drawBoard();
    }

    //add a row to the bottom (with a random empty cell)
    addBottomRow() {
        var r = this.num_rows - 1;
        var empty = Math.floor(Math.random() * Math.floor(this.num_cols));

        for (var row = 0; row < this.num_rows; row++) {
            this.board[row] = this.board[row + 1];
        }

        this.board[r] = [];
        for (var c = 0; c < this.num_cols; c++) {
            this.board[r][c] = "grey";
        }

        this.board[r][empty] = VACANT;
        this.drawBoard();
    }

    //generate random piece
    randomPiece() {
        let index = Math.floor(Math.random() * PIECES.length)
        return new Piece(PIECES[index][0], PIECES[index][1])
    }

    //check for collision at (x,y) location between the piece and the board
    collision(x, y) {
        var pieceX = this.piece.getX()
        var pieceY = this.piece.getY()

        for (var r = 0; r < this.piece.getLength(); r++) {
            for (var c = 0; c < this.piece.getLength(); c++) {
                if (this.piece.isEmpty(r, c)) {
                    continue;
                }

                let newX = pieceX + c + x;
                let newY = pieceY + r + y;

                if (newX < 0 || newX >= this.num_cols || newY >= this.num_rows) {
                    return true;
                }

                if (newY < 0) {
                    continue;
                }

                if (this.board[newY][newX] != VACANT) {
                    return true;
                }
            }
        }

        return false;
    }

    //mode the piece
    movePiece(delta_x, delta_y) {
        if (!this.collision(delta_x, delta_y)) {
            this.piece.updateX(delta_x);
            this.piece.updateY(delta_y);
        } else if (delta_y) {
            this.lockPiece();
            this.piece = this.randomPiece();
        }
        tetris.drawPiece();
    }

    //lock the piece to the board
    lockPiece() {
        for (var r = 0; r < this.piece.getLength(); r++) {
            for (var c = 0; c < this.piece.getLength(); c++) {
                // we skip the vacant squares
                if (this.piece.isEmpty(r, c)) {
                    continue;
                }
                // pieces to lock on top = game over
                if (this.piece.getY() + r < 0) {
                    //alert("Game Over!\n" + "Score: " + this.score);
                    // stop request animation frame
                    this.gameOver = true;
                    var end = new Date();
                    this.duration = end.getTime() - this.startTime;
                    return;
                }
                // we lock the piece
                this.board[this.piece.getY() + r][this.piece.getX() + c] = this.piece.getColor();
            }
        }

        // remove full rows
        for (var r = 0; r < this.num_rows; r++) {
            let isRowFull = true;
            for (c = 0; c < this.num_cols; c++) {
                isRowFull = isRowFull && (this.board[r][c] != VACANT);
            }
            if (isRowFull) {
                this.clearRow(r);
                this.score += 100;
                document.getElementById("score").innerText = this.score;
                this.scorestreak++;
                if (this.scorestreak == 3 && this.is_multi) {
                    SendRow();
                    this.scorestreak = 0;
                }
            }
        }
        // update the board
        this.drawBoard();
    }

    //rotate the piece
    rotate() {
        this.piece.nextPattern();
        let kick = 0;

        if (this.collision(0, 0)) {
            if (this.piece.getX() > this.num_cols / 2) {
                kick = -1; // we need to move the piece to the left
            } else {
                kick = 1; // we need to move the piece to the right
            }
        }

        if (!this.collision(kick, 0)) {
            this.piece.updateX(kick);
        } else {
            this.piece.prevPattern();
        }
        this.drawPiece();
    }

    //check if game is over
    isGameOver() {
        return this.gameOver;
    }

    //get game score
    getScore() {
        return this.score;
    }

    //get game duration
    getDuration() {
        if (this.gameOver) {
            return this.duration;
        } else {
            var current = new Date();
            return current.getTime() - this.startTime;
        }
    }

    //get JSON data for the current board and piece
    getTetrisJson() {
        var tetrisJson = {};

        var boardJson = {};
        boardJson["size"] = { height: this.num_rows, width: this.num_cols };
        boardJson["board"] = this.board;

        var pieceJson = {};
        pieceJson["location"] = { x: this.piece.getX(), y: this.piece.getY() };
        pieceJson["piece"] = this.piece.getPiece();
        pieceJson["color"] = this.piece.getColor();

        tetrisJson["board"] = boardJson;
        tetrisJson["piece"] = pieceJson;
        return JSON.stringify(tetrisJson);
    }

    //draw external board with the tetris JSON
    drawExternalBoard(tetrisJsonString, offset) {
        var tetrisObj = JSON.parse(tetrisJsonString);
        let height = tetrisObj["board"]["size"]["height"];
        let width = tetrisObj["board"]["size"]["width"];
        let board = tetrisObj["board"]["board"];

        let x = tetrisObj["piece"]["location"]["x"];
        let y = tetrisObj["piece"]["location"]["y"];
        let piece = tetrisObj["piece"]["piece"];
        let color = tetrisObj["piece"]["color"];

        // Clear board
        for (var r = 0; r < height; r++) {
            for (var c = 0; c < width; c++) {
                this.drawSquare(c, r, VACANT, offset);
            }
        }

        // Draw board
        for (var r = 0; r < height; r++) {
            for (var c = 0; c < width; c++) {
                this.drawSquare(c, r, board[r][c], offset);
            }
        }

        // Draw piece
        for (var r = 0; r < piece.length; r++) {
            for (var c = 0; c < piece.length; c++) {
                if (piece[r][c]) {
                    this.drawSquare(c + x, r + y, color, offset);
                }
            }
        }
    }
}


cvs = document.getElementById("game");
players = 1;
activegame = false;
dropStart = Date.now();

//start a two player game
function twoplayergame() {
    tetris = new Tetris(20, 10, cvs, true);
    cvs.width = 500;
    players = 2;
    start.style.display = "hidden";
    activegame = true;
    gameLoop();
}

//start a one player game
function oneplayergame() {
    tetris = new Tetris(20, 10, cvs);
    cvs.width = 200;
    players = 1;
    start.style.display = "hidden";
    activegame = true;
    gameLoop();
}

//End game state
function resetgame() {
    start.style.display = "visible";
    activegame = false;
    swal({title: "Game Over!",
        text: "Score: " + tetris.getScore(),
        button: "Okay"
    });
    saveScore();	
}

//Save the score to database using AJAX
function saveScore() {
    debugger;
    $.ajax({
        url: "/SaveScore",
        method: "POST",
        data: {
            score: tetris.getScore(),
            duration: tetris.getDuration(),
            is_single: players == 2,
        }
    }).done(function (result) {
        // Do something	
    });
}

//the tetris game loop
function gameLoop() {
    if (activegame) {
        let now = Date.now();
        let delta = now - dropStart;
        if (players == 2) {
            SendGameState();
        }
        if (delta > 1000) {
            tetris.movePiece(0, 1);
            dropStart = Date.now();
        }
        if (!tetris.isGameOver()) {
            //game is running
            requestAnimationFrame(gameLoop);
        }
        else {
            //game is over
            if (players == 2) {
                SendGameover();
            }
            else {
                resetgame();
            }
        }
    }
}

document.addEventListener("keydown", control);

//event handler for game controller keys
function control(event) {
    if (activegame == true) {
        if (event.keyCode == 37) {
            event.preventDefault();
            tetris.movePiece(-1, 0);
        } else if (event.keyCode == 38) {
            event.preventDefault();
            tetris.rotate();
        } else if (event.keyCode == 39) {
            event.preventDefault();
            tetris.movePiece(1, 0);
        } else if (event.keyCode == 40) {
            event.preventDefault();
            tetris.movePiece(0, 1);
        }
    }
};