using System.IO;
using Beastmaster.Core.State.Fight;
using Beastmaster.Core.State.Fight.Serializers;
using Common.Serialization;
using Path = Beastmaster.Core.State.Fight.Path;

namespace Beastmaster.Core.State
{
    public class MoveUnitActionConverter : InstanceBinaryConverter<MoveUnitAction.Data>
    {
        public override void Write(MoveUnitAction.Data obj, BinaryWriter writer)
        {
            writer.Write(obj.UnitId);
            writer.Write(obj.PlayerId);
            writer.Write(obj.Immutable);
            StateConvertersContainer.GetConverter<Path>().Write(obj.Path, writer);
        }

        public override MoveUnitAction.Data Read(BinaryReader reader)
        {
            var unitId = reader.ReadInt32();
            var playerId = reader.ReadByte();
            var immutable = reader.ReadBoolean();
            var path = StateConvertersContainer.GetConverter<Path>().Read(reader);
            return new MoveUnitAction.Data(playerId, unitId, path, immutable);
        }

        public override int GetBinarySize(MoveUnitAction.Data obj)
        {
            return SizeOfUtils.GetSizeOf(obj.UnitId) 
                + SizeOfUtils.GetSizeOf(obj.PlayerId) 
                + SizeOfUtils.GetSizeOf(obj.Immutable)
                + StateConvertersContainer.GetConverter<Path>().GetBinarySize(obj.Path);
        }
    }
}