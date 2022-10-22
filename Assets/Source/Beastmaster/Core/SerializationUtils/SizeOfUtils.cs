using System;
using System.Collections.Generic;

namespace Beastmaster.Core.State.Fight.Serializers
{
    public static class SizeOfUtils
    {
        private static readonly Dictionary<Type, int> _sizes = new Dictionary<Type, int>()
        {
            { typeof(int), sizeof(int) },
            { typeof(bool), sizeof(bool) },
            { typeof(byte), sizeof(byte) }
        };

        public static int GetSizeOf<T>(T value)
        {
            if (!_sizes.TryGetValue(typeof(T), out var size))
                throw new InvalidOperationException($"There's no cached size for {nameof(T)}");

            return size;
        }
    }
}