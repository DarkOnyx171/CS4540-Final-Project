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

}