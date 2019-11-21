const SQ = 20;
const VACANT = "white";

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

const O = [
    [
        [0, 0, 0, 0],
        [0, 1, 1, 0],
        [0, 1, 1, 0],
        [0, 0, 0, 0],
    ]
];

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

const PIECES = [
    [Z, "red"],
    [S, "green"],
    [T, "yellow"],
    [O, "blue"],
    [L, "purple"],
    [I, "cyan"],
    [J, "orange"]
];

class Piece {
    constructor(tetromino, color) {
        this.tetromino = tetromino;
        this.color = color;

        this.current = 0;

        this.x = 3;
        this.y = -2;
    }

    getPiece() {
        return this.tetromino[this.current];
    } 

    getX() {
        return this.x;
    }

    getY() {
        return this.y;
    }

    updateX(delta) {
        this.x += delta;
    }

    updateY(delta) {
        this.y += delta;
    }

    getColor() {
        return this.color;
    }

    isEmpty(row, col) {
        if (row < 0 || row >= this.tetromino[this.current].length)
            return false;
        if (row < 0 || row >= this.tetromino[this.current].length)
            return false;

        return !this.tetromino[this.current][row][col];
    }

    getLength() {
        return this.tetromino[this.current].length;
    }

    nextPattern() {
        this.current = (this.current + 1) % this.tetromino.length;
    }

    prevPattern() {
        this.current = (this.current + this.tetromino.length - 1) % this.tetromino.length;
    }
}

class Tetris {
    

    constructor(num_rows, num_cols, cvs) {
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

        this.score = 0;
        this.piece = this.randomPiece();
        this.drawBoard();
        this.gameOver = false;
    }

    drawSquare(x, y, color) {
        this.ctx.fillStyle = color;
        this.ctx.fillRect(x * SQ, y * SQ, SQ, SQ);

        this.ctx.strokeStyle = "Black";
        this.ctx.strokeRect(x * SQ, y * SQ, SQ, SQ);
    }

    drawEmpty() {
        for (var r = 0; r < this.num_rows; r++) {
            for (var c = 0; c < this.num_cols; c++) {
                this.drawSquare(c, r, VACANT);
            }
        }
    }

    drawBoard() {
        for (var r = 0; r < this.num_rows; r++) {
            for (var c = 0; c < this.num_cols; c++) {
                this.drawSquare(c, r, this.board[r][c]);
            }
        }
    }

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

    clearBoard() {
        for (var r= 0; r < this.num_rows; r++) {
            this.board[r] = [];
            for (var c = 0; c < this.num_cols; c++) {
                this.board[r][c] = VACANT;
            }
        }
    }

    clearRow(r) {
        debugger;
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

        this.score += 10;
        this.drawBoard();
    }

    randomPiece() {
        let index = Math.floor(Math.random() * PIECES.length)
        return new Piece(PIECES[index][0], PIECES[index][1])
    }

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

    lockPiece() {
        for (var r = 0; r < this.piece.getLength(); r++) {
            for (var c = 0; c < this.piece.getLength(); c++) {
                // we skip the vacant squares
                if (this.piece.isEmpty(r, c)) {
                    continue;
                }
                // pieces to lock on top = game over
                if (this.piece.getY() + r < 0) {
                    alert("Game Over");
                    // stop request animation frame
                    this.gameOver = true;
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
                this.clearRow(r)
            }
        }
        // update the board
        this.drawBoard();
    }

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

    isGameOver() {
        return this.gameOver;
    }

}

cvs = document.getElementById("game");
tetris = new Tetris(20, 10, cvs);

let dropStart = Date.now();

function gameLoop() {
    let now = Date.now();
    let delta = now - dropStart;
    if (delta > 1000) {
        tetris.movePiece(0, 1);
        dropStart = Date.now();
    }
    if (!tetris.isGameOver()) {
        requestAnimationFrame(gameLoop);
    }
}

document.addEventListener("keydown", control);

function control(event) {
    if (event.keyCode == 37) {
        tetris.movePiece(-1, 0);
    } else if (event.keyCode == 38) {
        tetris.rotate();
    } else if (event.keyCode == 39) {
        tetris.movePiece(1, 0);
    } else if (event.keyCode == 40) {
        tetris.movePiece(0, 1);
    }
}