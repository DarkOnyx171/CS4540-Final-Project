import "constants.js"
import "piece.js"

class Tetris {
    constructor(num_rows, num_cols) {
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

        this.num_rows = num_rows;
        this.num_cols = num_cols;

        this.board = [];
        this.clearBoard();

        this.score = 0;
        this.piece = randomPiece();
    }

    clearBoard() {
        for (r = 0; r < this.num_rows; r++) {
            this.board[r] = [];
            for (c = 0; c < this.num_cols; c++) {
                this.board[r][c] = VACANT;
            }
        }
    }

    clearRow(r) {
        if (r >= this.num_rows) {
            return;
        }

        for (c = 0; c < this.num_cols; c++) {
            this.board[r][c] = VACANT;
        }

        for (; r > 0; r++) {
            this.board[r] = this.board[r - 1];
        }

        for (c = 0; c < this.num_cols; c++) {
            this.board[0][c] = VACANT;
        }

        this.score += 10;
    }

    randomPiece() {
        let index = Math.floor(Math.random() * PIECES.length)
        return new Piece(PIECES[index][0], PIECES[index][1])
    }

    collision(x, y) {
        pieceX = this.piece.getX()
        pieceY = this.piece.getY()

        for (r = 0; r < this.piece.getLength(); r++) {
            for (c = 0; c < this.piece.getLength(); c++) {
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
        } else {
            this.lockPiece();
            this.piece = randomPiece();
        }
    }

    lockPiece() {
        for (r = 0; r < this.piece.getLength(); r++) {
            for (c = 0; c < this.piece.getLength(); c++) {
                // we skip the vacant squares
                if (this.piece.isEmpty(r, c)) {
                    continue;
                }
                // pieces to lock on top = game over
                if (this.piece.getY() + r < 0) {
                    alert("Game Over");
                    // stop request animation frame
                    gameOver = true;
                    return;
                }
                // we lock the piece
                board[this.piece.getY() + r][this.piece.getX() + c] = this.piece.getColor();
            }
        }

        // remove full rows
        for (r = 0; r < this.num_rows; r++) {
            let isRowFull = true;
            for (c = 0; c < this.num_cols; c++) {
                isRowFull = isRowFull && (board[r][c] != VACANT);
            }
            if (isRowFull) {
                clearRow(r)
            }
        }
        // update the board
        // drawBoard();
    }

    rotate() {
        let nextPattern = this.tetromino[(this.tetrominoN + 1) % this.tetromino.length];
        let kick = 0;

        if (this.collision(0, 0, nextPattern)) {
            if (this.x > COL / 2) {
                kick = -1; // we need to move the piece to the left
            } else {
                kick = 1; // we need to move the piece to the right
            }
        }

        if (!this.collision(kick, 0, nextPattern)) {
            this.x += kick;
            this.tetrominoN = (this.tetrominoN + 1) % this.tetromino.length; // (0+1)%4 => 1
            this.activeTetromino = this.tetromino[this.tetrominoN];
        }
    }

}