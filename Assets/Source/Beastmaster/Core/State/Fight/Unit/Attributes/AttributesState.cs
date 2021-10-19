using System.Collections.Generic;
using Beastmaster.Core.Primitives;

namespace Beastmaster.Core.State.Fight
{
    
    public class AttributesState
    {
        public readonly Dictionary<AttributeType, int> Values;

        public AttributesState(Dictionary<AttributeType, int> defaultValues)
        {
            Values = defaultValues;
        }
    }
}