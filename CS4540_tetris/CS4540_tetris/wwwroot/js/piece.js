/// <summary>
///  Author:    Tetrominoes Team
///  Date:      12/6/2019
///  Course:    CS 4540, University of Utah, School of Computing
 /// Copyright: CS 4540 and Tetrominoes Tesm - This work may not be copied for use in Academic Coursework.

 /// We, Tetrominoes Team, certify that we wrote this code from scratch and did not copy it in part or whole from
 /// another source.  Any references used in the completion of the assignment are cited in my README file.
   /// Purpose: The purpose of this document is to set handle pieces of the game
/// </summary>

//handle a tetromino piece has it moves about the board
export class Piece {
    //constructor for a tetromino piece
    constructor(tetromino, color) {
        this.tetromino = tetromino;
        this.color = color;

        this.current = 0;

        this.x = 3;
        this.y = -2;
    }

    //get where piece is on x axis
    getX() {
        return this.x;
    }

    //get where piece is on y axis
    getY() {
        return this.y;
    }

    //update the x axis for where piece is
    updateX(delta) {
        this.x += delta;
    }

    //update where piece is on y axis
    updateY(delta) {
        this.y += delta;
    }

    //get piece coloe
    getColor() {
        return this.color;
    }

    //is the spot where the piece is empty?
    isEmpty(row, col) {
        if (row < 0 || row >= this.tetromino[this.current].length)
            return false;
        if (row < 0 || row >= this.tetromino[this.current].length)
            return false;

        return !this.tetromino[this.current][row][col];
    }

    //get length of te piece
    getLength() {
        return this.tetromino[this.current].length;
    }

    //get the next pattern of the board
    nextPattern() {
        this.current = (this.current + 1) % this.tetromino.length;
    }

    //get the previous pattern of the board
    prevPattern() {
        this.current = (this.current + this.tetromino.length - 1) % this.tetromino.length;
    }
}