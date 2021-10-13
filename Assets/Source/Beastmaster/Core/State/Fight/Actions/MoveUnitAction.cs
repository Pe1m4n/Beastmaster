﻿using Beastmaster.Core.Controllers;
using Beastmaster.Core.Primitives;
using UnityEngine;

namespace Beastmaster.Core.State.Fight
{
    public class MoveUnitAction : AbstractStateAction<FightState, MoveUnitAction.Data>
    {
        public class Data : ActionData
        {
            public readonly int UnitId;
            public readonly Path Path;
                
            public Data(int unitId, Path path, bool immutable = false) : base(immutable)
            {
                Path = path;
                UnitId = unitId;
            }
        }

        protected override void Execute(FightState state, Data data)
        {
            if (!state.TryGetUnit(data.UnitId, out var unit))
            {
                Debug.LogError($"{nameof(MoveUnitAction)} Error: Couldn't get unit with id {data.UnitId}");
            }
            
            var coordinatesFrom = data.Path[0];
            var coordinatesTo = data.Path[data.Path.Length - 1];
            
            var origin = state.GetTile(coordinatesFrom);
            var destination = state.GetTile(coordinatesTo);
            
            Debug.Assert(unit.Coordinates.Equals(coordinatesFrom));
            Debug.Assert(destination.OccupantId == FightStateConstants.TILE_NOT_OCCUPIED);

            origin.OccupantId = FightStateConstants.TILE_NOT_OCCUPIED;
            destination.OccupantId = unit.Id;
            unit.Coordinates = coordinatesTo;
        }
    }
}