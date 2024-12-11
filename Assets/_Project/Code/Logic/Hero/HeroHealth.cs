using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Code.Logic.Hero
{
    public class HeroHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private float _currentHP;
        [SerializeField] private float _maxHP;

        public event Action HealthChanged;

        public float Current
        {
            get => _currentHP;
            set
            {
                if (value >= 0 && value <= Max)
                {
                    _currentHP = value;

                    HealthChanged?.Invoke();
                }
            }
        }

        public float Max
        {
            get => _maxHP;
            set => _maxHP = value;
        }

        public void TakeDamage(float damage)
        {
            if (Current <= 0)
                return;

            Current -= damage;

            _animator.PlayTakingDamage();
        }
    }
}

