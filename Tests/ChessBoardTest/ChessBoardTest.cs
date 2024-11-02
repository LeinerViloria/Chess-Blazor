
using ChessAI.Game.Enums;
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
}