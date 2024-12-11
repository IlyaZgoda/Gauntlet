using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Code.Logic.Enemy
{
    public class AgentMoveToPlayer : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private EnemyAnimator _animator;
        private Transform _heroTransform;
        private Vector2 _lastDirection;

        private void Update()
        {
            if (_heroTransform && !IsHeroReached() && enabled)
            {
                var direction = GetLookDirection();
                _agent.SetDestination(_heroTransform.position);
                _animator?.PlayRun(direction);

                if(direction != Vector2.zero)
                    _lastDirection = direction;
            }
            else
            {
                _animator?.PlayIdle(_lastDirection);
            }
        }

        public void Construct(Transform hero) =>
            _heroTransform = hero;

        private bool IsHeroReached() =>
            Vector2.Distance(_agent.transform.position, _heroTransform.position) <= _agent.stoppingDistance;

        private Vector2 GetLookDirection() =>
            _agent.velocity.normalized;  
    }
}
