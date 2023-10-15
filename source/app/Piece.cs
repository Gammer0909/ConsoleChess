using System;
using System.Collections.Generic;
using System.ComponentModel;
using ConsoleChess.Enums;

namespace ConsoleChess.Abstraction;

/// <summary>
/// Represents a chess piece
/// </summary>
public class Piece {
    public static Piece Null => new(PieceType.Null, PieceColor.Null, "", "", false, false, false, null, false);
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
    /// Creates a new Piece object by manually setting each property; Shouldn't ever be used.
    /// </summary>
    /// <param name="pieceType">The type of piece</param>
    /// <param name="pieceColor">The color of the piece</param>
    /// <param name="squareInNotation">The square that the piece is on</param>
    /// <param name="squareToMoveInNotation">The square that the piece is moving to</param>
    /// <param name="isCapturing">Is the piece capturing something?</param>
    /// <param name="isChecking">Is the piece a check?</param>
    /// <param name="isCheckingMate">Is the piece checkmate?</param>
    /// <param name="pieceToAttack">The piece that is being attacked. If the piece is not attacking anything, set this to Piece.Null</param>
    /// <param name="isLegalMove">Is the move legal?</param>
    public Piece(PieceType pieceType, PieceColor pieceColor, string squareInNotation, string squareToMoveInNotation, bool isCapturing, bool isChecking, bool isCheckingMate, Piece? pieceToAttack, bool isLegalMove) {
        this.pieceType = pieceType;
        this.pieceColor = pieceColor;
        this.squareInNotation = squareInNotation;
        this.squareToMoveInNotation = squareToMoveInNotation;
        this.isCapturing = isCapturing;
        this.isChecking = isChecking;
        this.isCheckingMate = isCheckingMate;
        this.pieceToAttack = pieceToAttack;
        this.isLegalMove = isLegalMove;
    }

    /// <summary>
    /// Creates a new Piece object with just the Piece type, Color, and square specified
    /// </summary>
    /// <param name="pieceType">The type of piece</param>
    /// <param name="pieceColor">The color of the piece</param>
    /// <param name="squareInNotation">The square that the piece is on</param>
    public Piece(PieceType pieceType, PieceColor pieceColor, string squareInNotation) {
        this.pieceType = pieceType;
        this.pieceColor = pieceColor;
        this.squareInNotation = squareInNotation;
        // Set the rest to empty values
        this.squareToMoveInNotation = "";
        this.isCapturing = false;
        this.isChecking = false;
        this.isCheckingMate = false;
        this.pieceToAttack = Piece.Null;
        this.isLegalMove = true;
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

        for (int i = 0; i < inputChessNotation.Length; i++) {

            // See if the current character is a letter A-H
            if (inputChessNotation[i] >= 'A' && inputChessNotation[i] <= 'H') {
                // See if the next character is a number 1-8
                if (inputChessNotation[i + 1] >= '1' && inputChessNotation[i + 1] <= '8' && squareInNotation == null) {
                    // If it is, then we have found the square that the piece is on
                    this.squareInNotation = inputChessNotation.Substring(i, 2);
                }
            }

            // See if the current character is a letter A-H
            if (inputChessNotation[i] >= 'A' && inputChessNotation[i] <= 'H') {
                // See if the next character is a number 1-8
                if (inputChessNotation[i + 1] >= '1' && inputChessNotation[i + 1] <= '8' && squareToMoveInNotation == null) {
                    // If it is, then we have found the square that the piece is moving to
                    this.squareToMoveInNotation = inputChessNotation.Substring(i, 2);
                }
            }

            if (inputChessNotation[i] == 'x') {
                this.isCapturing = true;
            }
            if (inputChessNotation[i] == '+') {
                this.isChecking = true;
            }
            if (inputChessNotation[i] == '#') {
                this.isCheckingMate = true;
            }
        }

        // Just in case, we know the piece's square is guaranteed to @ 1-2
        if (this.squareInNotation == null) {
            this.squareInNotation = inputChessNotation.Substring(1, 2);
        }

        if (this.isCapturing) {
            // Length is one extra!
            this.pieceToAttack = board.GetPiece(inputChessNotation.Substring(4, 2));
            // We can also get the square through the piece we got, provided it's null
            this.squareToMoveInNotation = this.pieceToAttack.squareInNotation;
        } else {
            // Length is normal, so the index starts at 3
            this.pieceToAttack = Piece.Null;
            this.squareToMoveInNotation = inputChessNotation.Substring(3, 2);
        }

    }

    public Piece GetPieceToAttack() {
        return this.pieceToAttack;
    }

    /// <summary>
    /// Returns the PieceType of the Piece
    /// </summary>
    /// <returns></returns>
    public PieceType GetPieceType() {
        return this.pieceType;
    }

    /// <summary>
    /// Returns the PieceColor of the Piece
    /// </summary>
    /// <returns></returns>
    public PieceColor GetPieceColor() {
        return this.pieceColor;
    }

    public override string ToString() {
        return $"{this.pieceColor} {this.pieceType} {this.squareInNotation} {this.squareToMoveInNotation}";
    }

}