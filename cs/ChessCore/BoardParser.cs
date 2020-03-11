using System;
using System.Linq;

namespace Chess
{
    public class BoardParser
    {
        public Board ParseBoard(String[] lines)
        {
            if (lines.Length != 8)
                throw new ArgumentException("Should be exactly 8 lines");

            if (lines.Any(line => line.Length != 8))
                throw new ArgumentException("All lines should have 8 chars length");

            var cells = new Piece[8][];
            for (Int32 y = 0; y < 8; y++)
            {
                String line = lines[y];

                if (line == null) throw new Exception("incorrect input");
                cells[y] = new Piece[8];

                for (Int32 x = 0; x < 8; x++)
                    cells[y][x] = ParsePiece(line[x]);
            }

            return new Board(cells);
        }

        private static Piece ParsePiece(Char pieceSign)
        {
            PieceColor color = char.IsUpper(pieceSign) ? PieceColor.White : PieceColor.Black;
            PieceType pieceType = ParsePieceType(char.ToUpper(pieceSign));
            return pieceType == null ? null : new Piece(pieceType, color);
        }

        private static PieceType ParsePieceType(Char sign)
        {
            return sign switch
            {
                'R' => PieceType.Rook,
                'K' => PieceType.King,
                'N' => PieceType.Knight,
                'B' => PieceType.Bishop,
                'Q' => PieceType.Queen,
                '.' => null,
                ' ' => null,
                _ => throw new FormatException("Unknown chess piece " + sign)
            };
        }
    }
}