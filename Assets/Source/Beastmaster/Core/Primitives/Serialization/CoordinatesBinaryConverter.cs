using System.IO;
using Beastmaster.Core.Primitives;
using Common.Serialization;

namespace Beastmaster.Core.State.Fight.Serializers
{
    public class CoordinatesBinaryConverter : InstanceBinaryConverter<Coordinates>
    {
        public override void Write(Coordinates obj, BinaryWriter writer)
        {
            writer.Write(obj.X);
            writer.Write(obj.Y);
        }

        public override Coordinates Read(BinaryReader reader)
        {
            var x = reader.ReadInt32();
            var y = reader.ReadInt32();
            return new Coordinates() { X = x, Y = y };
        }

        public override int GetBinarySize(Coordinates obj)
        {
            return SizeOfUtils.GetSizeOf(obj.X) * SizeOfUtils.GetSizeOf(obj.Y);
        }
    }
}