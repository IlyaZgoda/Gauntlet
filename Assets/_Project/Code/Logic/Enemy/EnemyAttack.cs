using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Code.Logic.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private float _attackCooldown = 1f;
        [SerializeField] private float _cleavage = 0.5f;
        [SerializeField] private float _effectiveDistance = 0.5f;
        [SerializeField] private float _damage = 10;

        private int _layerMask;
        private float _cooldown;
        private bool _isAttacking;
        private bool _attackIsActive;

        private void Awake() =>
          _layerMask = 1 << LayerMask.NameToLayer("Player");

        private void Update()
        {
            UpdateCooldown();

            if (CanAttack())
            {
                StartAttack();
            }       
        }

        private void OnAttack()
        {
            if (Hit(out Collider2D[] hit))
            {
                IHealth target = null;
                hit.FirstOrDefault(x => x.transform.parent.TryGetComponent<IHealth>(out target));
                target?.TakeDamage(_damage);
            }          
        }

        private void OnAttackEnded()
        {
            _cooldown = _attackCooldown;
            _isAttacking = false;
        }

        public void DisableAttack() =>
          _attackIsActive = false;

        public void EnableAttack() =>
          _attackIsActive = true;

        private bool CooldownIsUp() =>
          _cooldown <= 0f;

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
                _cooldown -= Time.deltaTime;
        }

        private bool Hit(out Collider2D[] target)
        {
            target = Physics2D.OverlapCircleAll(StartPoint(), _cleavage, _layerMask);

            return target != null;
        }

        private Vector2 StartPoint() =>
            new Vector2(transform.position.x, transform.position.y) * _effectiveDistance;

        private bool CanAttack() =>
            _attackIsActive && !_isAttacking && CooldownIsUp();   

        private void StartAttack()
        {
            _animator.PlayAttack();
            _isAttacking = true;
        }
    }
}

