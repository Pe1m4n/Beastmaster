using Beastmaster.Core.Controllers;
using Beastmaster.Core.Primitives;
using Beastmaster.Core.State;
using Beastmaster.Core.View;
using Beastmaster.Core.View.Units;
using UnityEngine;

namespace Beastmaster.Core
{
    public class FightInputContainer : IFightInputProvider
    {
        private const int OBJECTS_TO_RAYCAST = 1;
        
        private readonly RaycastHit[] _raycastHits = new RaycastHit[OBJECTS_TO_RAYCAST];
        private readonly Camera _camera;

        private Coordinates _tileUnderCursor = Coordinates.None;
        private int _unitUnderCursor = FightStateConstants.NO_UNIT;
        private bool _lmbClicked;
        private bool _rmbClicked;

        public FightInputContainer(Camera camera)
        {
            _camera = camera;
        }

        public Coordinates GetTileUnderCursor()
        {
            return _tileUnderCursor;
        }

        public int GetUnitIdUnderCursor()
        {
            return _unitUnderCursor;
        }

        public bool LMBClicked()
        {
            return _lmbClicked;
        }

        public bool RMBClicked()
        {
            return _rmbClicked;
        }

        public void Tick()
        {
            //TODO: UI collision detection
            _lmbClicked = Input.GetMouseButtonDown(0);
            _rmbClicked = Input.GetMouseButtonDown(1);
            
            if (!TryRaycastGameObject(out var go))
            {
                _tileUnderCursor = Coordinates.None;
                _unitUnderCursor = FightStateConstants.NO_UNIT;
                return;
            }

            if (go.TryGetComponent<TileView>(out var tileView))
            {
                _tileUnderCursor = tileView.Coordinates;
                _unitUnderCursor = FightStateConstants.NO_UNIT;
                return;
            }

            if (go.TryGetComponent<UnitView>(out var unitView))
            {
                _tileUnderCursor = Coordinates.None;
                _unitUnderCursor = unitView.UnitId;
                return;
            }
        }
        
        private bool TryRaycastGameObject(out GameObject go)
        {
            go = null;
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.RaycastNonAlloc(ray, _raycastHits) <= 0)
            {
                return false;
            }
            
            go = _raycastHits[0].collider.gameObject;
            return true;
        }
    }
}