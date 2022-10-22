using System;
using System.Collections.Generic;
using Common.Serialization;

namespace Beastmaster.Core.State.Fight.Serializers
{
    public static class StateConvertersContainer
    {
        private static readonly Dictionary<Type, IInstanceBinaryConverter> _converters =
            new Dictionary<Type, IInstanceBinaryConverter>();

        static StateConvertersContainer()
        {
            AddConverter(new PathBinaryConverter());
            AddConverter(new MoveUnitActionConverter());
            AddConverter(new CoordinatesBinaryConverter());
            BinarySerializer.ContainerResolver = GetConverter;
        }

        public static void AddConverter<T>(InstanceBinaryConverter<T> converter)
        {
            _converters.Add(typeof(T), converter);
        }
            
        public static InstanceBinaryConverter<T> GetConverter<T>()
        {
            var converter = GetConverter(typeof(T));
            return converter as InstanceBinaryConverter<T>;
        }

        private static IInstanceBinaryConverter GetConverter(Type type)
        {
            if (!_converters.TryGetValue(type, out var converter))
                throw new InvalidOperationException($"There's no pooled converter for type {type.Name}");
            
            return converter;
        }
    }
}