using Code.Infrastructure.States;

namespace Code.Infrastructure.Factories
{
    public interface IStateFactory
    {
        public TState Create<TState>() where TState : class, IExitableState;
    }
}
