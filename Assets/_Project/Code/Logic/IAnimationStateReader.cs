using Code.Logic;

namespace Code.Logic
{
    public interface IAnimationStateReader
    {
        void OnEnter(int stateHash);
        void OnExit(int stateHash);
        AnimatorState State { get; }
    }
}
