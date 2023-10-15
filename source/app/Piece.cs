using System;
using System.Collections.Generic;
using System.ComponentModel;
using ConsoleChess.Enums;

namespace ConsoleChess.Abstraction;

public class Piece {
    public static Piece Null => new(PieceType.Null, PieceColor.Null);
    private PieceType pieceType { get; set; }
    private PieceColor pieceColor { get; set; }
    private bool isCapturing { get; set; }
    private bool isChecking { get; set; }
    private bool isCheckingMate { get; set; }
    private Piece? pieceToAttack { get; set; }
    private bool isLegalMove = false;

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

        if (inputChessNotation.Length == 5) {
            // Check if the piece is capturing by finding if 'x' is in the input
            this.isCapturing = inputChessNotation[1] == 'x';
            // Check if the piece is checking by finding if '+' is in the input
            this.isChecking = inputChessNotation[4] == '+';
            // Check if the piece is checking mate by finding if '#' is in the input
            this.isCheckingMate = inputChessNotation[4] == '#';

            // Get the piece to attack by getting the last two characters of the input
            this.pieceToAttack = board.GetPiece(inputChessNotation[2..4]);

            // Im not sure how to check if it's a legal move so
            // TODO: Check for legal moves
        
        }

    }



}