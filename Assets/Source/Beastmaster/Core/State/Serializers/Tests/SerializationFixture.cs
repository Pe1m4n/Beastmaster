using Beastmaster.Core.Primitives;
using Common.Serialization;
using NUnit.Framework;
using UnityEngine;

namespace Beastmaster.Core.State.Fight.Serializers.Tests
{
    [TestFixture]
    public class SerializationFixture
    {
        [Test]
        public void Test_PostDeserialization_Equals()
        {
            var coordinates = new Coordinates() { X = 7, Y = 9 };
            var bytes = BinarySerializer.Serialize(coordinates);
            var deserializedCoordinates = BinarySerializer.Deserialize<Coordinates>(bytes);
            Debug.Assert(coordinates.Equals(deserializedCoordinates));
        }
    }
}