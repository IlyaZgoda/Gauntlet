using Code.Infrastructure.States;
using Zenject;

namespace Code.Infrastructure.Factories
{
    public class StateFactory : IStateFactory
    {
        private readonly IInstantiator _instantiator;

        public StateFactory(IInstantiator instantiator) =>
            _instantiator = instantiator;   

        public TState Create<TState>() where TState : class, IExitableState =>       
            _instantiator.Instantiate<TState>();   
    }
}
