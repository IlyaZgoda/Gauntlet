using Code.Services.Observable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Logic.Hero
{
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private HeroHealth _health;
        [SerializeField] private HeroMove _move;
        [SerializeField] private HeroAttack _attack;
        [SerializeField] private HeroAnimator _animator;

        private bool _isDead;
        private CoreEventBus _eventBus;

        public event Action Happened;

        [Inject]
        public void Construct(CoreEventBus eventBus) =>
            _eventBus = eventBus;

        private void Start() =>
            _health.HealthChanged += HealthChanged;

        private void OnDestroy() =>
            _health.HealthChanged -= HealthChanged;   

        private void HealthChanged()
        {
            if (!_isDead && _health.Current <= 0)
                Die();
        }

        private void Die()
        {
            _isDead = true;
            _move.enabled = false;
            _attack.enabled = false;
            _animator.PlayDeath();
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            Happened?.Invoke();
            _eventBus.PlayerDied?.Invoke();
        }
    }
}
