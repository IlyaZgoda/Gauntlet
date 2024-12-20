﻿using Code.Services.Observable;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Code.Logic.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private AgentMoveToPlayer _agentMoveToPlayer;
        [SerializeField] private NavMeshAgent _agent;
        private CoreEventBus _eventBus;

        public event Action Happened;

        [Inject]
        public void Construct(CoreEventBus eventBus) =>
            _eventBus = eventBus;

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
            _agent.isStopped = false;
            _agent.enabled = false;

            _animator.PlayDeath();
           
            StartCoroutine(DestroyTimer());

            Happened?.Invoke();
            _eventBus.EnemyDied?.Invoke();
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(5);
            Destroy(gameObject);
        }

    }
}
