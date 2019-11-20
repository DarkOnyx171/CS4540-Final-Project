import "constants.js"

class Piece {
    constructor(tetromino, color) {
        this.tetromino = tetromino;
        this.color = color;

        this.tetrominoN = 0;
        this.activeTetromino = this.tetromino[this.tetrominoN];

        this.x = 3;
        this.y = -2;
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
        if (row < 0 || row >= this.activeTetromino.length)
            return false;
        if (row < 0 || row >= this.activeTetromino.length)
            return false;

        return !this.activeTetromino[row][col];
    }

    getLength() {
        return this.activeTetromino.length;
    }
}