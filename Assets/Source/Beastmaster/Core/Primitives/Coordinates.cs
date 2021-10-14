using System;

namespace Beastmaster.Core.Primitives
{
    [Serializable]
    public struct Coordinates
    {
        public static Coordinates None = new Coordinates() { X = int.MinValue, Y = int.MinValue };
        public int X;
        public int Y;

        public override string ToString()
        {
            return $"[{X}|{Y}]";
        }

        public bool Equals(int x, int y)
        {
            return X == x && Y == y;
        }
    }

    public static class CoordinatesExtensions
    {
        public static Direction GetLookAtDirection(this Coordinates origin, Coordinates target)
        {
            if (target.X > origin.X && target.Y == origin.Y)
                return Direction.Right;
            if (target.X < origin.X && target.Y == origin.Y)
                return Direction.Left;
            if (target.X == origin.X && target.Y > origin.Y)
                return Direction.Top;
            if (target.X == origin.X && target.Y < origin.Y)
                return Direction.Bottom;
            
            
            if (target.X > origin.X && target.Y > origin.Y)
                return Direction.TopRight;
            if (target.X > origin.X && target.Y < origin.Y)
                return Direction.BottomRight;
            if (target.X < origin.X && target.Y > origin.Y)
                return Direction.TopLeft;
            
            return Direction.BottomRight;
        }
    }
}