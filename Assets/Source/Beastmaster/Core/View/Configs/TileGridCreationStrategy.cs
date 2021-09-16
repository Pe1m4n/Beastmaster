using Beastmaster.Core.Primitives;
using Beastmaster.Core.State;
using UnityEngine;

namespace Beastmaster.Core.View.Configs
{
    public abstract class TileGridCreationStrategy : ScriptableObject
    {
        public GameObject TilePrefab;
        public float TileWidth;

        public abstract TileView[] CreateTileGrid(FightState state, Transform parentTransform);
        public abstract int GetTileIndexFromCoordinates(FightState state, Coordinates coordinates);
    }
}