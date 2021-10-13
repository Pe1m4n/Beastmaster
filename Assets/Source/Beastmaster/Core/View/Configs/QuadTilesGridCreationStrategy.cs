using Beastmaster.Core.Primitives;
using Beastmaster.Core.State;
using Beastmaster.Core.State.Fight;
using UnityEngine;

namespace Beastmaster.Core.View.Configs
{
    public class QuadTilesGridCreationStrategy : TileGridCreationStrategy
    {
        public override TileView[] CreateTileGrid(FightState state, Transform parentTransform)
        {
            var result = new TileView[state.Tiles.Length];
            for (var i = 0; i < state.Tiles.Length; i++)
            {
                var tileState = state.Tiles[i];
                var position = new Vector3()
                {
                    x = tileState.Coordinates.X * TileWidth,
                    y = 0,
                    z = tileState.Coordinates.Y * TileWidth
                };
                var tileGo = Instantiate(TilePrefab, position, Quaternion.identity);
                tileGo.transform.SetParent(parentTransform);
                tileGo.name = $"Tile[{tileState.Coordinates.X}|{tileState.Coordinates.Y}]";
                
                var tileView = tileGo.GetComponentInChildren<TileView>();
                tileView.Init(tileState.Coordinates);
                result[i] = tileView;
            }

            return result;
        }

        public override int GetTileIndexFromCoordinates(FightState state, Coordinates coordinates)
        {
            return coordinates.Y * state.FightConfig.LocationWidth + coordinates.X;
        }
    }
}