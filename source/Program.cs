using System;
using System.Collections.Generic;

class Chess {


    char[][] boardASCII = {
        new char[] {'R', 'N', 'B', 'Q', 'K', 'B', 'N', 'R'},
        new char[] {'P', 'P', 'P', 'P', 'P', 'P', 'P', 'P'},
        new char[] {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        new char[] {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        new char[] {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        new char[] {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        new char[] {'p', 'p', 'p', 'p', 'p', 'p', 'p', 'p'},
        new char[] {'r', 'n', 'b', 'q', 'k', 'b', 'n', 'r'}
    };


    char[][] boardUnicode = {
        new char[] {'♜', '♞', '♝', '♛', '♚', '♝', '♞', '♜'},
        new char[] {'♟', '♟', '♟', '♟', '♟', '♟', '♟', '♟'},
        new char[] {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        new char[] {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        new char[] {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        new char[] {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        new char[] {'♙', '♙', '♙', '♙', '♙', '♙', '♙', '♙'},
        new char[] {'♖', '♘', '♗', '♕', '♔', '♗', '♘', '♖'}
    };
    
    private bool useUnicode = true;
    private bool isWhiteTurn = true;
    

}