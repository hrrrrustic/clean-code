using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess
{
    public class PieceType
    {
        public static readonly PieceType Rook = new PieceType(true, 'R', new Location(1, 0), new Location(0, 1));

        public static readonly PieceType King = new PieceType(false, 'K', new Location(1, 1), new Location(1, 0),
            new Location(0, 1));

        public static readonly PieceType Queen = new PieceType(true, 'Q', new Location(1, 1), new Location(1, 0),
            new Location(0, 1));

        public static readonly PieceType Bishop = new PieceType(true, 'B', new Location(1, 1));
        public static readonly PieceType Knight = new PieceType(false, 'N', new Location(2, 1), new Location(1, 2));

        private readonly Location[] directions;
        private readonly Boolean infinit;
        private readonly Char sign;

        private PieceType(Boolean infinit, Char sign, params Location[] directions)
        {
            this.infinit = infinit;
            this.sign = sign;
            this.directions = directions
                .Union(directions.Select(d => new Location(-d.X, d.Y)))
                .Union(directions.Select(d => new Location(d.X, -d.Y)))
                .Union(directions.Select(d => new Location(-d.X, -d.Y)))
                .ToArray();
        }

        public override String ToString()
        {
            return sign.ToString();
        }

        public IEnumerable<Location> GetMoves(Location location, Board board)
        {
            return directions.SelectMany(d => MovesInOneDirection(location, board, d, infinit));
        }

        private static IEnumerable<Location> MovesInOneDirection(Location from, Board board, Location dir,
            Boolean infinit)
        {
            Piece piece = board.GetPiece(from);
            Int32 distance = infinit ? int.MaxValue : 2;
            for (Int32 i = 1; i < distance; i++)
            {
                Location to = new Location(from.X + dir.X * i, from.Y + dir.Y * i);
                if (!board.Contains(to)) break;
                Piece destinationPiece = board.GetPiece(to);
                if (destinationPiece == null)
                {
                    yield return to;
                }
                else
                {
                    if (destinationPiece.Color != piece.Color)
                        yield return to;
                    yield break;
                }
            }
        }
    }
}