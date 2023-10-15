using System;
using System.Collections.Generic;
using ConsoleChess.Enums;
using ConsoleChess.Abstraction;

namespace ConsoleChess.Abstraction;

/// <summary>
/// Represents a move in a game of chess
/// </summary>
public class Move {

    public Piece movingPiece { get; set; }
    public Piece capturedPiece { get; set; }
    public bool isCapturing { get; set; }
    public bool isChecking { get; set; }
    public bool isCheckingMate { get; set; }
    public bool isLegalMove { get; set; }

    /// <summary>
    /// Creates a new Move object by manually setting each property
    /// </summary>
    /// <param name="movingPiece">Piece that is moving/attacking</param>
    /// <param name="capturedPiece">Piece that is getting captured. If the move is only moving a piece, and not capturing, set this to Piece.Null</param>
    /// <param name="isCapturing">Is the move capturing something?</param>
    /// <param name="isChecking">Is the move a check?</param>
    /// <param name="isCheckingMate">Is the move checkmate?</param>
    /// <param name="isLegalMove">Is the move legal?</param>
    public Move(Piece movingPiece, Piece capturedPiece, bool isCapturing, bool isChecking, bool isCheckingMate, bool isLegalMove) {
        this.movingPiece = movingPiece;
        this.capturedPiece = capturedPiece;
        this.isCapturing = isCapturing;
        this.isChecking = isChecking;
        this.isCheckingMate = isCheckingMate;
        this.isLegalMove = isLegalMove;
    }

    /// <summary>
    /// Creates a new Move object from a given chess notation
    /// </summary>
    /// <param name="inputChessNotation">The algebraic chess notation of the move</param>
    /// <param name="pieceColor">The color of the piece</param>
    /// <param name="board">The board that the game is being played on</param>
    public Move(string inputChessNotation, PieceColor pieceColor, Board board) {

        // Algebraic chess notation example
        // Bd4xe5+
        // B - Piece
        // x - Capture
        // e5 - Target square
        // + - Check

        this.movingPiece = new Piece(inputChessNotation, pieceColor, board);
        this.capturedPiece = new Piece(PieceType.Null, PieceColor.Null, "");
        this.isCapturing = movingPiece.isCapturing;
        this.isChecking = movingPiece.isChecking;
        this.isCheckingMate = movingPiece.isCheckingMate;
        this.isLegalMove = true;

        if (this.isCapturing) {
            this.capturedPiece = this.movingPiece.GetPieceToAttack();
        }


    }

    /// <summary>
    /// Gets the piece that is moving
    /// </summary>
    /// <returns>This' movingPiece</returns>
    public Piece GetMovingPiece() {
        return this.movingPiece;
    }

    public override string ToString()
    {
        return $"{this.movingPiece} {this.capturedPiece} {this.isCapturing} {this.isChecking} {this.isCheckingMate} {this.isLegalMove}";
    }

}