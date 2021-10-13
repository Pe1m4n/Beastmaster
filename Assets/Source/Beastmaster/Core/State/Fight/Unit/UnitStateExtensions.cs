using System;
using Beastmaster.Core.Primitives;

namespace Beastmaster.Core.State.Fight
{
    public static class UnitStateExtensions
    {
        internal static void AddUnit(this FightState state, int playerId, int unitType, Coordinates coordinates)
        {
            var tile = state.GetTile(coordinates);
            if (tile.OccupantId >= 0)
            {
                throw new InvalidOperationException($"Can't spawn unit {unitType} on tile {coordinates} since it's already occupied");
            }

            var unit = new UnitState()
            {
                OwnerId = playerId,
                Coordinates = coordinates,
                Id = state.UnitsSpawned++,
                UnitTypeId = unitType
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
    }
}