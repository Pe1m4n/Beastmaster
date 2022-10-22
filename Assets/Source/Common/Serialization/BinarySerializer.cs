using System;
using System.IO;
using System.Reflection;

namespace Common.Serialization
{
    public static class BinarySerializer
    {
        public static Func<Type, IInstanceBinaryConverter> ContainerResolver;
        
        //TODO: rethink if ArraySegment is really needed (probably to get rid of allocations)
        public static byte[] Serialize<T>(T obj)
        {
            var converter = GetConverter<T>();
            var size = converter.GetBinarySize(obj);
            var bytes = new byte[size];
            using var stream = new MemoryStream(bytes);
            using var writer = new BinaryWriter(stream);
            converter.Write(obj, writer);
            return bytes;
        }

        public static T Deserialize<T>(byte[] bytes)
        {
            var converter = GetConverter<T>();
            using var stream = new MemoryStream(bytes);
            using var reader = new BinaryReader(stream);
            return converter.Read(reader);
        }

        private static InstanceBinaryConverter<T> GetConverter<T>()
        {
            if (ContainerResolver == null)
            {
                var attribute = typeof(T).GetCustomAttribute(typeof(BinarySerializedAttribute)) as BinarySerializedAttribute;
                if (attribute == null)
                    throw new InvalidOperationException(
                        $"Can't serialize/deserialize {nameof(T)} without {nameof(BinarySerializedAttribute)}");
                
                var converter = Activator.CreateInstance(attribute.ConverterClassType) as InstanceBinaryConverter<T>;
                if (converter == null)
                    throw new InvalidOperationException(
                        $"{nameof(T)} has wrong InstanceBinaryConverter set in {nameof(BinarySerializedAttribute)}");
                
                return converter;
            }

            var pooledConverter = ContainerResolver(typeof(T)) as InstanceBinaryConverter<T>;
            if (pooledConverter == null)
                throw new InvalidOperationException($"There's no pooled converter for type {nameof(T)}");
            
            return pooledConverter;
        }
    }
}