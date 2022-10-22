using Beastmaster.Core.Primitives;
using Beastmaster.Core.State.Fight.Serializers;
using Common.Serialization;

namespace Beastmaster.Core.State.Fight
{
    [BinarySerialized(typeof(PathBinaryConverter))]
    public class Path
    {
        public const int MAXIMUM_LENGTH = 15;
        
        [BinarySerializedField] private int _lenght;
        [BinarySerializedField] private readonly Coordinates[] _points = new Coordinates[MAXIMUM_LENGTH];
        public int Length => _lenght;
        
        public Coordinates this[int i] => _points[Length - 1 - i];

        public void Add(Coordinates coordinates)
        {
            _points[_lenght++] = coordinates;
        }
    }
}