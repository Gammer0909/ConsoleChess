using System;
using System.Collections.Generic;
using ConsoleChess.Enums;
using ConsoleChess.Abstraction;

namespace ConsoleChess.Abstraction;

public class Move {

    private Piece movingPiece { get; set; }
    private Piece capturedPiece { get; set; }
    private bool isCapturing { get; set; }
    private bool isChecking { get; set; }
    private bool isCheckingMate { get; set; }
    private bool isLegalMove { get; set; }

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

    public Move(string inputChessNotation, PieceColor pieceColor, Board board) {
        this.movingPiece = new Piece(inputChessNotation, pieceColor, board);
        this.capturedPiece = new Piece(PieceType.Null, PieceColor.Null);
        this.isCapturing = inputChessNotation[1] == 'x';
        this.isChecking = inputChessNotation[^1] == '+';
        this.isCheckingMate = inputChessNotation[^1] == '#';
        this.isLegalMove = false;
    }


}