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
    public class HeroHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private float _currentHP;
        [SerializeField] private float _maxHP;
        private CoreEventBus _eventBus;

        public event Action HealthChanged;

        [Inject]
        public void Construct(CoreEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public float Current
        {
            get => _currentHP;
            set
            {
                if (value >= 0 && value <= Max)
                {
                    _currentHP = value;

                    HealthChanged?.Invoke();
                    _eventBus.PlayerHealthChanged?.Invoke(_currentHP);
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

