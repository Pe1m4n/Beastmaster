using System;

namespace Common.Serialization
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Struct)]
    public class BinarySerializedAttribute : Attribute
    {
        public readonly Type ConverterClassType;

        public BinarySerializedAttribute(Type converterClassType)
        {
            ConverterClassType = converterClassType;
        }
    }
}