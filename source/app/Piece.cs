using System;
using System.Collections.Generic;
using System.ComponentModel;
using ConsoleChess.Enums;

namespace ConsoleChess.Abstraction;

/// <summary>
/// Represents a chess piece
/// </summary>
public class Piece {
    public static Piece Null => new(PieceType.Null, PieceColor.Null);
    private PieceType pieceType { get; set; }
    private PieceColor pieceColor { get; set; }
    public string squareInNotation { get; set; }
    public string squareToMoveInNotation { get; set; }
    public bool isCapturing { get; set; }
    public bool isChecking { get; set; }
    public bool isCheckingMate { get; set; }
    private Piece pieceToAttack { get; set; }
    private bool isLegalMove = true;

    /// <summary>
    /// Creates a new Piece object from a given PieceType and PieceColor
    /// </summary>
    /// <param name="pieceType"></param>
    /// <param name="pieceColor"></param>
    public Piece(PieceType pieceType, PieceColor pieceColor) {
        this.pieceType = pieceType;
        this.pieceColor = pieceColor;
    }

    /// <summary>
    /// Creates a new Piece object from a given chess notation
    /// </summary>
    /// <param name="inputChessNotation"></param>
    /// <param name="pieceColor"></param>
    /// <param name="board"></param>
    /// <exception cref="ArgumentException"></exception>
    public Piece(string inputChessNotation, PieceColor pieceColor, Board board) {
        this.pieceColor = pieceColor;
        this.pieceType = inputChessNotation[0] switch {
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

        if (inputChessNotation.Length == 7) {
            // Check if the piece is capturing by finding if 'x' is in the input (Eg. Bd4**x**e5+)
            this.isCapturing = inputChessNotation[4] == 'x';
            // Check if the piece is checking by finding if '+' is in the input (Eg. Bd4xe5**+**)
            this.isChecking = inputChessNotation[inputChessNotation.Length - 1] == '+';
            // Check if the piece is checking mate by finding if '#' is in the input (Eg. Bd4xe5+**#**)
            this.isCheckingMate = inputChessNotation[inputChessNotation.Length - 1] == '#';

            // Get the piece to attack by getting the last two characters of the input
            this.pieceToAttack = board.GetPiece(inputChessNotation[2..4]);

            // Im not sure how to check if it's a legal move so
            // TODO: Check for legal moves

            // Get the Piece that this is on
            this.squareInNotation = inputChessNotation[1..2];

            // Get the Piece that this is moving to
            this.squareToMoveInNotation = inputChessNotation[5..6];

        }

    }

    public Piece GetPieceToAttack() {
        return this.pieceToAttack;
    }

}