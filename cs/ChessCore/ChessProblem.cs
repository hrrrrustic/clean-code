using System;
using System.Linq;

namespace Chess
{
    public class ChessProblem
    {
        private readonly Board board;
        public ChessStatus ChessStatus;

        public ChessProblem(Board board)
        {
            this.board = board;
        }

        public void CalculateChessStatus(PieceColor color)
        {
            Boolean isCheck = IsCheck(color);
            Boolean hasMoves = HasMoves(color);

            ChessStatus = isCheck switch
            {
                true when hasMoves => ChessStatus.Check,
                true => ChessStatus.Mate,
                false when hasMoves => ChessStatus.Ok,
                false => ChessStatus.Stalemate
            };
        }

        private Boolean HasMoves(PieceColor color)
        {
            foreach (Location locFrom in board.GetPieces(color))
            foreach (Location locTo in board.GetPiece(locFrom).GetMoves(locFrom, board))
            {
                Boolean hasMoves = HasMoves(locTo, locFrom, color);

                if (hasMoves)
                    return true;
            }

            return false;
        }

        private Boolean HasMoves(Location locTo, Location locFrom, PieceColor color)
        {
            using TemporaryPieceMove _ = board.PerformTemporaryMove(locFrom, locTo);

            if (!IsCheck(color))
                return true;

            return false;
        }

        private Boolean IsCheck(PieceColor enemyColor)
        {
            return (from Location loc in board.GetPieces(enemyColor == PieceColor.White ? PieceColor.Black : PieceColor.White)
                let piece = board.GetPiece(loc)
                select piece.GetMoves(loc, board)).Any(moves => moves.Any(destination =>
                Piece.Is(board.GetPiece(destination), PieceColor.White, PieceType.King)));
        }
    }
}