using System.IO;
using Beastmaster.Core.Primitives;
using Common.Serialization;

namespace Beastmaster.Core.State.Fight.Serializers
{
    public class PathBinaryConverter : InstanceBinaryConverter<Path>
    {
        public override void Write(Path obj, BinaryWriter writer)
        {
            writer.Write(obj.Length);
            for (int i = 0; i < obj.Length; i++)
                StateConvertersContainer.GetConverter<Coordinates>().Write(obj[i], writer);
        }

        public override Path Read(BinaryReader reader)
        {
            var path = new Path();
            var length = reader.ReadInt32();
            for (int i = 0; i < length; i++)
                path.Add(StateConvertersContainer.GetConverter<Coordinates>().Read(reader));
            
            return path;
        }

        public override int GetBinarySize(Path obj)
        {
            return StateConvertersContainer.GetConverter<Coordinates>().GetBinarySize(Coordinates.None) * obj.Length;
        }
    }
}