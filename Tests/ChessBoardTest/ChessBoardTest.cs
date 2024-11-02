
using ChessAI.Game;
using ChessAI.Game.Enums;
using ChessAI.Game.Utils;
using ChessAI.Models;

namespace ChessAI.Tests;

public class ChessBoardTest
{
    [Theory]
    [InlineData(PieceType.Rook, 2)]
    [InlineData(PieceType.Knight, 2)]
    [InlineData(PieceType.Bishop, 2)]
    [InlineData(PieceType.Queen, 1)]
    [InlineData(PieceType.King, 1)]
    [InlineData(PieceType.Pawn, 8)]
    public void BoardHasCorrectPiecesByColorWhenInitialize(PieceType Piece, byte Amout)
    {
        // Arrange
        var Board = new ChessBoard();
        var ColorsAmount = Enum.GetValues<PieceColor>().Length;

        // Act
        Board.InitializeBoard();
        var ZonesWithPieces = Board.Pieces.Cast<ChessPiece>()
            .Where(x => x?.Type == Piece)
            .ToHashSet();

        // Assert
        Assert.True(ZonesWithPieces.Count > 0);
        Assert.StrictEqual(ColorsAmount * Amout, ZonesWithPieces.Count);
    }

    [Theory]
    [InlineData(PieceType.Knight, new byte[] { 0, 1 }, new byte[] { 2, 2 }, true)]
    [InlineData(PieceType.King, new byte[] { 0, 4 }, new byte[] { 3, 2 }, false)]
    public void ValidateFirstMove_GameHasStartedNow(PieceType pieceType, byte[] from, byte[] to, bool IsValid)
    {
        // Arrange
        var (Row, Col) = (from[0], from[1]);
        var ToCoordinate = (Row: to[0], Col: to[1]);
        var Move = new ChessMove(Row, Col, ToCoordinate.Row, ToCoordinate.Col);
        var Game = new GameState();
        var Board = new ChessBoard(Initialize: true);
        var PieceToMove = Board.Pieces[Row, Col]!;

        // Act
        var IsValidMove = Utilities.IsValidMove(Game, Board, Move);

        // Assert
        Assert.StrictEqual(IsValid, IsValidMove);
        Assert.StrictEqual(pieceType, PieceToMove.Type);
    }
}