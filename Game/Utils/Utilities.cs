
using ChessAI.Game.Enums;

namespace ChessAI.Game.Utils;

public static class Utilities
{
    public static SquareColor GetSquareColor(int row, int col) =>
        (row + col) % 2 == 0 ? SquareColor.Light : SquareColor.Dark;

    public static string GetSquareColorClass(int row, int col) =>
        GetSquareColor(row, col) is SquareColor.Light ? "light" : "dark";
}