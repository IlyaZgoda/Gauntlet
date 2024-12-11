using UnityEngine;
using UnityEngine.AI;

namespace Code.Logic.Enemy
{
    public class StopMovingOnAttack : MonoBehaviour
    {
        private EnemyAnimator _animator;
        private AgentMoveToPlayer _agent;

        private void Awake()
        {
            _animator = GetComponent<EnemyAnimator>();
            _agent = GetComponent<AgentMoveToPlayer>();
        }

        private void Start()
        {
            _animator.StateEntered += SwitchMovementOff;
            _animator.StateExited += SwitchMovementOn;
        }

        private void OnDestroy()
        {
            _animator.StateEntered -= SwitchMovementOff;
            _animator.StateExited -= SwitchMovementOn;
        }

        private void SwitchMovementOn(AnimatorState animatorState)
        {
            if (animatorState == AnimatorState.Attack)
                _agent.enabled = true;
        }

        private void SwitchMovementOff(AnimatorState animatorState)
        {
            if (animatorState == AnimatorState.Attack)
                _agent.enabled = false;
        }

    }
}
