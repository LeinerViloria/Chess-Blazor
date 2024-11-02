using ChessAI.Game.Enums;
using ChessAI.Models;

namespace ChessAI.Game
{
    // Represents castling rights and tracks whether the king or rooks have moved
    public class CastlingRights
    {
        public bool CanWhiteCastleKingside { get; set; } = true;
        public bool CanWhiteCastleQueenside { get; set; } = true;
        public bool CanBlackCastleKingside { get; set; } = true;
        public bool CanBlackCastleQueenside { get; set; } = true;

        public PieceMovementTracker WhiteKingMovement { get; set; } = new PieceMovementTracker();
        public PieceMovementTracker BlackKingMovement { get; set; } = new PieceMovementTracker();

        public bool CanCastle(PieceColor color, bool kingside)
        {
            var tracker = color == PieceColor.White ? WhiteKingMovement : BlackKingMovement;
            return kingside ? tracker.CanCastleKingside() : tracker.CanCastleQueenside();
        }

        public void ApplyCastlingMove(PieceColor color, bool kingside, ChessBoard board)
        {
            var row = color == PieceColor.White ? 0 : 7;
            var kingStartCol = 4;
            var rookStartCol = kingside ? 7 : 0;
            var kingEndCol = kingside ? 6 : 2;
            var rookEndCol = kingside ? 5 : 3;

            board.MovePiece(board.Pieces[row, kingStartCol], row, kingEndCol);
            board.MovePiece(board.Pieces[row, rookStartCol], row, rookEndCol);

            var tracker = color == PieceColor.White ? WhiteKingMovement : BlackKingMovement;
            tracker.MarkKingMoved();
            tracker.MarkRookMoved(kingside);
        }

        public void UpdateCastlingRights(ChessPiece piece, ChessMove move)
        {
            if (piece.Type == PieceType.King)
            {
                if (piece.Color == PieceColor.White)
                {
                    CanWhiteCastleKingside = false;
                    CanWhiteCastleQueenside = false;
                }
                else
                {
                    CanBlackCastleKingside = false;
                    CanBlackCastleQueenside = false;
                }
            }
            else if (piece.Type == PieceType.Rook)
            {
                var tracker = piece.Color == PieceColor.White ? WhiteKingMovement : BlackKingMovement;
                tracker.MarkRookMovedByColumn(move.FromCol);
            }
        }
    }
}
