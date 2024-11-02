
using ChessAI.Game.Enums;
using ChessAI.Game.Utils;

namespace ChessAI.Tests;

public class UtilitiesTest
{
    [Fact]
    public void EvenNumberZoneIsLight()
    {
        // Arrange
        var RowZone = 0;
        var ColZone = 0;

        // Act
        var Result = Utilities.GetSquareColor(RowZone, ColZone);

        // Assert
        Assert.Equal(SquareColor.Light, Result);
    }

    [Fact]
    public void OddNumberZoneIsDark()
    {
        // Arrange
        var RowZone = 0;
        var ColZone = 1;

        // Act
        var Result = Utilities.GetSquareColor(RowZone, ColZone);

        // Assert
        Assert.Equal(SquareColor.Dark, Result);
    }
}