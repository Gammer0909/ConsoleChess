using System;
using System.Collections.Generic;
using ConsoleChess.Graphics;
using ConsoleChess.Enums;
using ConsoleChess.Abstraction;
using System.IO;

namespace ConsoleChess.Abstraction;

// SINGLETONS, SINGLETONS, SINGLETONS!
/// <summary>
/// The chess board that the game is played on
/// </summary>
public class Board {

    /// <summary>
    /// The file that the game is being logged to
    /// </summary>
    private static readonly string logFile = "game.pgn";

    private int turnNumber = 1;

    public bool gameOver = false;

    public Board() {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
    }

    public Board(bool useUnicode) {
        this.useUnicode = useUnicode;
        Console.OutputEncoding = System.Text.Encoding.UTF8;
    }


    char[][] boardASCII = {
        new char[] {'r', 'n', 'b', 'q', 'k', 'b', 'n', 'r'},
        new char[] {'p', 'p', 'p', 'p', 'p', 'p', 'p', 'p'},
        new char[] {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        new char[] {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        new char[] {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        new char[] {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        new char[] {'P', 'P', 'P', 'P', 'P', 'P', 'P', 'P'},
        new char[] {'R', 'N', 'B', 'Q', 'K', 'B', 'N', 'R'}
    };


    char[][] boardUnicode = {
        //           A    B    C    D    E    F    G    H
        new char[] {'♖', '♘', '♗', '♕', '♔', '♗', '♘', '♖'}, // 8
        new char[] {'♙', '♙', '♙', '♙', '♙', '♙', '♙', '♙'}, // 7
        new char[] {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '}, // 6
        new char[] {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '}, // 5
        new char[] {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '}, // 4
        new char[] {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '}, // 3
        new char[] {'♟', '♟', '♟', '♟', '♟', '♟', '♟', '♟'}, // 2
        new char[] {'♜', '♞', '♝', '♛', '♚', '♝', '♞', '♜'}, // 1
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

    public void DrawBoard(bool useColor) {
        Console.ResetColor();
        Console.Clear();
        Console.WriteLine("  A B C D E F G H");
        // If we're debugging just print the raw data
        if (useColor) {
            if (this.useUnicode) {
                foreach (char[] c in boardUnicode) {
                    foreach (char c2 in c) {
                        Console.Write(" " + c2);
                    }
                    Console.WriteLine();
                }
            } else {
                foreach (char[] c in boardASCII) {
                    foreach (char c2 in c) {
                        Console.Write(" " + c2);
                    }
                    Console.WriteLine();
                }
            }
            return;
        }
        if (isWhiteTurn) {
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
        } else {
            for (int i = 7; i >= 0; i--) {
                Console.Write(8 - i + " ");
                for (int j = 7; j >= 0; j--) {
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
            Console.WriteLine("  H G F E D C B A");
        }
    }

    public void Move(bool logMoves) {
        Console.Write(turnNumber + ". " + (isWhiteTurn ? "White" : "Black") + " > ");
        string? input = Console.ReadLine();

        // If the input is 'exit' then close the game
        if (input == "exit") {
            // We'll count this as a draw, and add ½–½ to the log
            if (logMoves) {
                using (StreamWriter sw = File.AppendText(logFile)) {
                    sw.WriteLine("\n½-½");
                }
            }
            Environment.Exit(0);
        }

        // Make sure input is not null
        if (input == null) {
            Console.WriteLine("Please input a move in algebraic chess notation");
            return;
        }

        // Check if the string is in algebraic chess notation
        if (!IsInChessNotation(input)) {
            Console.WriteLine("Please input a move in algebraic chess notation");
            return;
        }

        // Convert the input to a move
        Move move = new Move(input, isWhiteTurn ? PieceColor.White : PieceColor.Black, this);


        // Check if the move is legal (It always will be because of my niaive lack of implementation)
        if (!move.isLegalMove) {
            Console.WriteLine("Illegal move");
            this.Move(logMoves);
        }


        // First, get the piece that is moving
        Piece movingPiece = move.GetMovingPiece();

        // Next, Find out where it is moving to
        (int, int) targetSquare = ConvertNotationToIndexes(move.movingPiece.squareToMoveInNotation);

        // Next, find out where it is moving from
        (int, int) startingSquare = ConvertNotationToIndexes(move.movingPiece.squareInNotation);
        
        // Next, move the piece
        if (useUnicode) {
            boardUnicode[targetSquare.Item1][targetSquare.Item2] = boardUnicode[startingSquare.Item1][startingSquare.Item2];
            boardUnicode[startingSquare.Item1][startingSquare.Item2] = ' ';
        } else {
            boardASCII[targetSquare.Item1][targetSquare.Item2] = boardASCII[startingSquare.Item1][startingSquare.Item2];
            boardASCII[startingSquare.Item1][startingSquare.Item2] = ' ';
        }

        // BUT WAIT! If it's checkmate, end the game there.
        if (move.isCheckingMate) {
            Console.WriteLine($"Checkmate! Congratulations to {(!isWhiteTurn ? "Black" : "White")}!");
            isGameOver = true;
            this.DrawBoard(false);
            if (logMoves) {
                // Append the win to the log, where 1-0 is a white win, 0-1 is a black win
                using (StreamWriter sw = File.AppendText(logFile)) {
                    if (isWhiteTurn) {
                        sw.WriteLine("\n1-0");
                    } else {
                        sw.WriteLine("\n0-1");
                    }
                }
            }
            Environment.Exit(0);
        }


        // Log the move
        if (logMoves) {
            LogMove(move);
        }

        // Increment the turn number
        turnNumber++;

        // Switch the turn
        isWhiteTurn = !isWhiteTurn;

    }

    private void LogMove(Move move) {

        using (StreamWriter sw = File.AppendText(logFile)) {
            if (isWhiteTurn) {
                sw.Write($"{turnNumber}. ");
            }
            sw.Write($"{move.movingPiece.squareInNotation}{(move.isCapturing ? "x" : "")}{move.movingPiece.squareToMoveInNotation}{(move.isChecking ? "+" : "")}{(move.isCheckingMate ? "#" : "")} ");
            if (!isWhiteTurn)
                sw.WriteLine();
        }

    }

    /// <summary>
    ///  Checks if the input is in algebraic chess notation
    /// </summary>
    /// <param name="input">the input string to check</param>
    /// <returns>true if the input string is in algebraic chess notation</returns>
    private bool IsInChessNotation(string input) {
        // Gosh dang this method is an abomination of ifs and elses

        // Algebraic chess notation example
        // Bd4xe5
        // B - Piece
        // x - Capture
        // e5 - Target square
        // Bd4e5


        // Check if the input is at least 5 characters long
        if (input.Length < 5) {
            return false;
        }

        // Check if the first character is a letter (Eg. **B**d4xe5)
        if (!char.IsLetter(input[0])) {
            return false;
        }

        // Check if the second character is a character (Eg. B**d**4xe5)
        if (!char.IsLetter(input[1])) {
            return false;
        }

        // If the fourth character is a character, and it's an 'x' then it's a capture and the chess notation is a character longer (Eg. Bd4**x**e5)
        if (char.IsLetter(input[3]) && input[3] == 'x') {
            // Check if the fifth character is a letter (Eg. Bd4x**e**5)
            if (!char.IsLetter(input[4])) {
                return false;
            }

            // Check that the sixth character is a number (Eg. Bd4xe**5**)
            if (!char.IsNumber(input[5])) {
                return false;
            }

        } else {
            // If the fourth character is a letter, then it's a target square (Eg. Bd4**e**5)
            if (!char.IsLetter(input[3])) {
                return false;
            }

            // Check if the fifth character is a number (Eg. Bd4e**5**)
            if (!char.IsNumber(input[4])) {
                return false;
            }
        }

        if (input.Length > 5) {
            if (input[5] != '+' && input[5] != '#') {
                return false;
            }
        }

        // If the input is longer than 6 characters, then check if the seventh character is a '+' or a '#' (Eg. Bd4xe5**+** or Eg. Bd4xe5**#**)
        if (input.Length > 6) {
            if (input[6] != '+' && input[6] != '#') {
                return false;
            }
        }

        // If the input is longer than 7 characters, then it's invalid.
        if (input.Length > 7) {
            return false;
        }

        return true;


    }

    /// <summary>
    /// Converts a chess notation square to a row and column index
    /// </summary>
    /// <param name="notationSquare">The chess notation square to convert</param>
    /// <returns>A tuple containing the row and column index</returns>
    private (int, int) ConvertNotationToIndexes(string notationSquare) {
        // First index from the board
        int columnIndex = notationSquare[0] switch {
            'A' => 0,
            'a' => 0,
            'B' => 1,
            'b' => 1,
            'C' => 2,
            'c' => 2,
            'D' => 3,
            'd' => 3,
            'E' => 4,
            'e' => 4,
            'F' => 5,
            'f' => 5,
            'G' => 6,
            'g' => 6,
            'H' => 7,
            'h' => 7,
            _ => throw new ArgumentException("Invalid chess notation")
        };

        int rowIndex = notationSquare[1] switch {
            '1' => 7,
            '2' => 6,
            '3' => 5,
            '4' => 4,
            '5' => 3,
            '6' => 2,
            '7' => 1,
            '8' => 0,
            _ => throw new ArgumentException("Invalid chess notation")
        };

        return (rowIndex, columnIndex);
    }

    public Piece GetPiece(string inputChessNotation) {
        // First index from the board
        int columnIndex = inputChessNotation[0] switch {
            'A' => 0,
            'a' => 0,
            'B' => 1,
            'b' => 1,
            'C' => 2,
            'c' => 2,
            'D' => 3,
            'd' => 3,
            'E' => 4,
            'e' => 4,
            'F' => 5,
            'f' => 5,
            'G' => 6,
            'g' => 6,
            'H' => 7,
            'h' => 7,
            _ => throw new ArgumentException("Invalid chess notation")
        };

        int rowIndex = inputChessNotation[1] switch {
            '1' => 7,
            '2' => 6,
            '3' => 5,
            '4' => 4,
            '5' => 3,
            '6' => 2,
            '7' => 1,
            '8' => 0,
            _ => throw new ArgumentException("Invalid chess notation")
        };

        char pieceCharacter = boardASCII[rowIndex][columnIndex];

        PieceType type = pieceCharacter switch {
            'P' => PieceType.Pawn,
            'p' => PieceType.Pawn,
            'R' => PieceType.Rook,
            'r' => PieceType.Rook,
            'N' => PieceType.Knight,
            'n' => PieceType.Knight,
            'B' => PieceType.Bishop,
            'b' => PieceType.Bishop,
            'Q' => PieceType.Queen,
            'q' => PieceType.Queen,
            'K' => PieceType.King,
            'k' => PieceType.King,
            _ => throw new ArgumentException("Invalid chess notation")
        };

        PieceColor color = pieceCharacter switch {
            'P' => PieceColor.White,
            'R' => PieceColor.White,
            'N' => PieceColor.White,
            'B' => PieceColor.White,
            'Q' => PieceColor.White,
            'K' => PieceColor.White,
            'p' => PieceColor.Black,
            'r' => PieceColor.Black,
            'n' => PieceColor.Black,
            'b' => PieceColor.Black,
            'q' => PieceColor.Black,
            'k' => PieceColor.Black,
            _ => throw new ArgumentException("Invalid chess notation")
        };

        return new Piece(type, color, inputChessNotation);
        
    }

}
