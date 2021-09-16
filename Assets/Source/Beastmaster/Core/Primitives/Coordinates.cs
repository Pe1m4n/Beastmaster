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
    }
}