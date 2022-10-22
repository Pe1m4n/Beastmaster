using System;
using System.Collections.Generic;
using Common.Serialization;

namespace Beastmaster.Core.State.Fight.Serializers
{
    public class BinaryConvertersContainer
    {
        private readonly Dictionary<Type, IInstanceBinaryConverter> _converters =
            new Dictionary<Type, IInstanceBinaryConverter>();

        public void AddConverter<T>(InstanceBinaryConverter<T> converter)
        {
            _converters.Add(typeof(T), converter);
        }
            
        public InstanceBinaryConverter<T> GetConverter<T>()
        {
            if (!_converters.TryGetValue(typeof(T), out var converter))
                throw new InvalidOperationException($"There's no pooled converter for type {nameof(T)}");

            return converter as InstanceBinaryConverter<T>;
        }
    }
}