using System;

namespace Chess
{
    public class Location
    {
        public readonly Int32 X, Y;

        public Location(Int32 x, Int32 y)
        {
            X = x;
            Y = y;
        }

        public override String ToString()
        {
            return $"({X}, {Y})";
        }

        public override Int32 GetHashCode()
        {
            return unchecked((X * 397) ^ Y);
        }

        public override Boolean Equals(Object obj)
        {
            Location other = obj as Location;
            if (other == null) return false;
            return other.X == X && other.Y == Y;
        }
    }
}