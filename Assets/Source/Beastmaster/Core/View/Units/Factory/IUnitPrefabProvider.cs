using UnityEngine;

namespace Beastmaster.Core.View.Units
{
    public interface IUnitPrefabProvider
    {
        public GameObject GetPrefabForUnitTypeId(int typeId);
    }
}