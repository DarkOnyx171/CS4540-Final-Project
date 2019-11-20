import "constants.js"

class Piece {
    constructor(tetromino, color) {
        this.tetromino = tetromino;
        this.color = color;

        this.current = 0;

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