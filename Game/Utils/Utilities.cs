
using ChessAI.Game.Enums;
using ChessAI.Models;

namespace ChessAI.Game.Utils;

public static class Utilities
{
    public static SquareColor GetSquareColor(int row, int col) =>
        (row + col) % 2 == 0 ? SquareColor.Light : SquareColor.Dark;

    public static string GetSquareColorClass(int row, int col) =>
        GetSquareColor(row, col) is SquareColor.Light ? "light" : "dark";

    public static bool IsValidDrag(GameState State, ChessPiece? piece) => 
        piece != null && piece.Color == State.ActiveColor;

    public static bool IsValidMove(GameState State, ChessBoard Board, ChessMove move)
    {
        var piece = Board.Pieces[move.FromRow, move.FromCol];
        if (!IsValidDrag(State, piece)) return false;

        var validMoves = Board.GetValidMoves(piece);
        return validMoves.Contains((move.ToRow, move.ToCol));
    }
}