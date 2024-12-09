using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Code.Logic.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private AgentMoveToPlayer _agentMoveToPlayer;

        public event Action Happened;

        private void Start() =>
            _health.HealthChanged += OnHealthChanged;
        
        private void OnDestroy() =>
            _health.HealthChanged -= OnHealthChanged;
        
        private void OnHealthChanged()
        {
            if (_health.Current <= 0)
                Die();
        }

        private void Die()
        {
            _health.HealthChanged -= OnHealthChanged;

            _agentMoveToPlayer.enabled = false;

            _animator.PlayDeath();
           
            StartCoroutine(DestroyTimer());

            Happened?.Invoke();
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(5);
            Destroy(gameObject);
        }

    }
}
