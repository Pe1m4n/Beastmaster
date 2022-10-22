using System.IO;

namespace Common.Serialization
{
    public abstract class InstanceBinaryConverter<T>: IInstanceBinaryConverter
    {
        public abstract void Write(T obj, BinaryWriter writer);
        public abstract T Read(BinaryReader reader);
        public abstract int GetBinarySize(T obj);
    }

    public interface IInstanceBinaryConverter
    {
    }
}