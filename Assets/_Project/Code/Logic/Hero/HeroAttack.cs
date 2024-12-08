using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Logic.Hero
{
    public class HeroAttack : MonoBehaviour
    {
        [SerializeField] private HeroAnimator _heroAnimator;
        private InputActions _inputActions;

        private void Awake()
        {
            _inputActions = new InputActions();
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
            if (!_heroAnimator.IsAttacking)
                _heroAnimator.PlayAttack();
        }

    }
}
