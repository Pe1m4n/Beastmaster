using System;
using Beastmaster.Core.Primitives;
using Beastmaster.Core.State;
using Beastmaster.Core.State.Fight;
using Beastmaster.Core.View.Configs;
using Common.PathFinding;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Beastmaster.Core.View
{
    public class TilesView : IDisposable
    {
        private readonly TileGridCreationStrategy _gridCreationStrategy;
        private readonly TileView[] _tileViews;
        
        public TilesView(TileGridCreationStrategy gridCreationStrategy,
            Transform rootTransform,
            FightStateContainer fightStateContainer)
        {
            _gridCreationStrategy = gridCreationStrategy;
            var tilesRoot = new GameObject("TilesView");
            tilesRoot.transform.parent = rootTransform;

            _tileViews = gridCreationStrategy.CreateTileGrid(fightStateContainer.State, tilesRoot.transform);
        }
        
        public void ApplyState(ViewState state)
        {
            var pathData = state.PlayerState.CurrentPathData;
            
            for (int i = 0; i < _tileViews.Length; i++)
            {
                var tile = _tileViews[i];

                if (tile.Coordinates.Equals(state.PlayerState.HoveredTile))
                {
                    tile.SetColor(Color.yellow);
                }
                else if (pathData.AvailableForPathing(tile.Coordinates.X, tile.Coordinates.Y))
                {
                    tile.SetColor(Color.green);
                }
                else
                {
                    tile.SetColor(Color.white);
                }
            }
        }

        public void Dispose()
        {
            foreach (var tileView in _tileViews)
            {
                Object.Destroy(tileView);
            }
        }

        public Vector3 GetViewPosition(FightState state, Coordinates coordinates)
        {
            var viewIndex = _gridCreationStrategy.GetTileIndexFromCoordinates(state, coordinates);
            return _tileViews[viewIndex].transform.position;
        }
    }
}