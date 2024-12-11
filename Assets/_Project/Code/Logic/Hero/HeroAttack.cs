using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Logic.Hero
{
    public class HeroAttack : MonoBehaviour
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _damageRadius;
        [SerializeField] private float _beginAttackWindow = 0f;
        [SerializeField] private float _endAttackWindow = 1f;
        [SerializeField] private float _attackCooldown = 0.01f;
        private float _cooldown;
        private bool _canPerformSecondAttack = false;
        [SerializeField] private HeroAnimator _animator;
        private InputActions _inputActions;
        private static int _layerMask;

        private void Awake()
        {
            _inputActions = new InputActions();
            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        private void OnEnable()
        {
            _inputActions.Enable();
            _inputActions.Player.Attack.performed += OnAttack;
            _inputActions.Player.SpecialAttack.performed += OnSpecialAttack;
        }

        private void OnDisable()
        {
            _inputActions.Disable();
            _inputActions.Player.Attack.performed -= OnAttack;
            _inputActions.Player.SpecialAttack.performed -= OnSpecialAttack;
        }

        private void Update()
        {
            UpdateCooldown();
        }

        private void OnAttack(InputAction.CallbackContext context)
        {
            if (CooldownIsUp())
            {
                Hit(1);
                StartCoroutine(CheckForSecondAttack()); 
                _animator.PlayAttack();

                _cooldown = _attackCooldown;
            }
            else if (_canPerformSecondAttack)
            {
                Hit(1.1f);
                _animator.PlaySecondAttack();
                _canPerformSecondAttack = false;
            }
        }

        private void OnSpecialAttack(InputAction.CallbackContext context)
        {
            if (!_animator.IsSpecialAttacking)
            {
                Hit(2);
                _animator.PlaySpecialAttack();
            }
        }

        private void Hit(float coef)
        {
            var enemies = GetHittables();

            if (enemies == null || enemies.Length == 0)
                return;

            for (int i = 0; i < enemies.Length; i++)
                enemies[i].transform.parent.GetComponent<IHealth>().TakeDamage(_damage * coef);

        }

        private Collider2D[] GetHittables() =>
            Physics2D.OverlapCircleAll(StartPoint(), _damageRadius, _layerMask);

        private Vector2 StartPoint() =>
          new(transform.position.x, transform.position.y);

        private IEnumerator CheckForSecondAttack()
        {
            yield return new WaitForSeconds(_beginAttackWindow);
            _canPerformSecondAttack = true;
            yield return new WaitForSeconds(_endAttackWindow - _beginAttackWindow);
            _canPerformSecondAttack = true;
        }

        private bool CooldownIsUp() =>
         _cooldown <= 0f;

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
                _cooldown -= Time.deltaTime;
        }
    }
}

