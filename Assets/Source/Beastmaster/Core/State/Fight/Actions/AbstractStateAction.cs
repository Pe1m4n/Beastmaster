using Common.Serialization;

namespace Beastmaster.Core.State.Fight
{
    public abstract class AbstractStateAction<TState, TData> where TData: ActionData
    {
        public void Execute(TState state, ActionData data)
        {
            Execute(state, data as TData);
        }
        
        protected abstract void Execute(TState state, TData data);
    }
    
    public abstract class ActionData
    {
        [BinarySerializedField] public readonly byte PlayerId;
        [BinarySerializedField] public readonly bool Immutable;

        protected ActionData(byte playerId, bool immutable)
        {
            PlayerId = playerId;
            Immutable = immutable;
        }
    }
}