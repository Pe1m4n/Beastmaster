using System;
using System.Collections.Generic;
using UnityEngine;

namespace Beastmaster.Core.View.Units
{
    public class UnitPrefabProvider : ScriptableObject, IUnitPrefabProvider
    {
        [SerializeField] private List<IdPrefabPair> _unitPrefabs;
        
        public GameObject GetPrefabForUnitTypeId(int typeId)
        {
            foreach (var unitData in _unitPrefabs)
            {
                if (unitData.TypeId == typeId)
                {
                    return unitData.Prefab;
                }
            }
            throw new ArgumentException($"There is no UnitView prefab for type id {typeId}");
        }

        [Serializable]
        private class IdPrefabPair
        {
            public int TypeId;
            public GameObject Prefab;
        }
    }
}