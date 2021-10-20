using Beastmaster.Core.Primitives;

namespace Beastmaster.Core.State.Fight
{
    public class UnitsSystem
    {
        public void Turn(FightState state)
        {
            foreach (var unit in state.Units)
            {
                ResetAttributesOnTurn(unit);
            }
        }

        private void ResetAttributesOnTurn(UnitState state)
        {
            state.Attributes.Values[AttributeType.MovePoints] = state.UnitConfig.Attributes[AttributeType.MovePoints];
        }
    }
}