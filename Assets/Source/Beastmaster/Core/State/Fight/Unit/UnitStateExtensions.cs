using System;
using System.Collections.Generic;
using Beastmaster.Core.Configs;
using Beastmaster.Core.Primitives;

namespace Beastmaster.Core.State.Fight
{
    public static class UnitStateExtensions
    {
        internal static void AddUnit(this FightState state, byte playerId, UnitConfig config, Coordinates coordinates)
        {
            var tile = state.GetTile(coordinates);
            if (tile.OccupantId >= 0)
            {
                throw new InvalidOperationException($"Can't spawn unit {config.UnitTypeId} on tile {coordinates} since it's already occupied");
            }

            var unit = new UnitState()
            {
                OwnerId = playerId,
                Coordinates = coordinates,
                Id = state.UnitsSpawned++,
                UnitTypeId = config.UnitTypeId,
                UnitConfig = config,
                Attributes = new AttributesState(new Dictionary<AttributeType, int>(config.Attributes))
            };

            state.Units.Add(unit);
            tile.OccupantId = unit.Id;
        }

        public static bool TryGetUnit(this FightState state, int unitId, out UnitState unitState)
        {
            unitState = null;
            if (unitId == FightStateConstants.NO_UNIT)
                return false;
            
            foreach (var unit in state.Units)
            {
                if (unit.Id != unitId)
                {
                    continue;
                }

                unitState = unit;
                return true;
            }

            return false;
        }

        public static Direction GetLookAtDirection(this UnitState unit, Coordinates target)
        {
            return unit.Coordinates.GetLookAtDirection(target);
        }
    }
}