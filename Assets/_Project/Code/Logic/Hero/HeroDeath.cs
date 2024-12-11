using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Code.Logic.Hero
{
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private HeroHealth _health;
        [SerializeField] private HeroMove _move;
        [SerializeField] private HeroAttack _attack;
        [SerializeField] private HeroAnimator _animator;

        private bool _isDead;

        public event Action Happened;

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

            Happened?.Invoke();
        }
    }
}
