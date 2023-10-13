using System;
using System.Collections.Generic;
using ConsoleChess.Graphics;

class Chess {

    public Chess() {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
    }

    public Chess(bool useUnicode) {
        this.useUnicode = useUnicode;
        Console.OutputEncoding = System.Text.Encoding.UTF8;
    }


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

    int[][] boardColors = {
        new int[] {0, 1, 0, 1, 0, 1, 0, 1},
        new int[] {1, 0, 1, 0, 1, 0, 1, 0},
        new int[] {0, 1, 0, 1, 0, 1, 0, 1},
        new int[] {1, 0, 1, 0, 1, 0, 1, 0},
        new int[] {0, 1, 0, 1, 0, 1, 0, 1},
        new int[] {1, 0, 1, 0, 1, 0, 1, 0},
        new int[] {0, 1, 0, 1, 0, 1, 0, 1},
        new int[] {1, 0, 1, 0, 1, 0, 1, 0}
    };
    
    private bool useUnicode = true;
    private bool isWhiteTurn = true;
    private bool isGameOver = false;

    public void DrawBoard() {
        Console.ResetColor();
        Console.Clear();
        Console.WriteLine("  A B C D E F G H");
        for (int i = 0; i < 8; i++) {
            Console.Write(8 - i + " ");
            for (int j = 0; j < 8; j++) {
                if (useUnicode) {
                    if (boardColors[i][j] == 0)
                        Draw.Pixel(ConsoleColor.Gray, ConsoleColor.Black, boardUnicode[i][j]);
                    else
                        Draw.Pixel(ConsoleColor.DarkGray, ConsoleColor.White, boardUnicode[i][j]);
                    Console.Write(" ");
                    Console.ResetColor();
                } else {
                    if (boardColors[i][j] == 0)
                        Draw.Pixel(ConsoleColor.Gray, ConsoleColor.Black, boardASCII[i][j]);
                    else
                        Draw.Pixel(ConsoleColor.DarkGray, ConsoleColor.White, boardASCII[i][j]);
                    Console.Write(" ");
                    Console.ResetColor();
                }
            }
            Console.WriteLine(8 - i);
        }
        Console.WriteLine("  A B C D E F G H");
    }

    public void Move() {
        string input = Console.ReadLine();
    }

    private string StringToChessNotation(string input) {


        
    }

}

class Program {
    public static void Main(string[] args) {
        Chess chess = new Chess();
        chess.DrawBoard();
    }
}