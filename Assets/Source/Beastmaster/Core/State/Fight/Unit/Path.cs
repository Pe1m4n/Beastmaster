using Beastmaster.Core.Primitives;

namespace Beastmaster.Core.State.Fight
{
    public class Path
    {
        public const int MAXIMUM_LENGTH = 15;
        
        private int _lenght;
        private readonly Coordinates[] _points = new Coordinates[MAXIMUM_LENGTH];
        public int Length => _lenght;
        
        public Coordinates this[int i] => _points[Length - 1 - i];

        public void Add(Coordinates coordinates)
        {
            _points[_lenght++] = coordinates;
        }
    }
}