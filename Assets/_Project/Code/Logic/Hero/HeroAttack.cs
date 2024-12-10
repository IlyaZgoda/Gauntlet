using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Logic.Hero
{
    public class HeroAttack : MonoBehaviour
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _damageRadius;
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
        }

        private void OnDisable()
        {
            _inputActions.Disable();
            _inputActions.Player.Attack.performed -= OnAttack;
        }

        private void OnAttack(InputAction.CallbackContext context)
        {
            if (!_animator.IsAttacking)
            {
                Hit();
                _animator.PlayAttack();
            }
                
        }

        private void Hit()
        {
            var enemies = GetHittables();

            if (enemies == null || enemies.Length == 0)
                return;

            for (int i = 0; i < enemies.Length; i++)
                enemies[i].transform.parent.GetComponent<IHealth>().TakeDamage(_damage);
        }

        private Collider2D[] GetHittables() =>
            Physics2D.OverlapCircleAll(StartPoint(), _damageRadius, _layerMask);

        private Vector2 StartPoint() =>
          new(transform.position.x, transform.position.y);

    }
}

