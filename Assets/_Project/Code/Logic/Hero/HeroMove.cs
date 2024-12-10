using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Logic.Hero
{
    public class HeroMove : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 1f;
        [SerializeField] private HeroAnimator _animator;
        private Rigidbody2D _rigidBody;
        private InputActions _inputActions;
        private Vector2 _moveInput;
        private Vector2 _smoothedMoveInput;
        private Vector2 _smoothedMoveVelocity;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _inputActions = new InputActions();
        }      

        private void FixedUpdate()
        {
            if (_animator.IsAttacking)
            {
                _rigidBody.linearVelocity = Vector2.zero;
                return;
            }
            _smoothedMoveInput = Vector2.SmoothDamp(
                _smoothedMoveInput, 
                _moveInput, 
                ref _smoothedMoveVelocity, 
                0.1f);

            _rigidBody.linearVelocity = _smoothedMoveInput * _moveSpeed;

            _animator.PlayRun(_moveInput);

            if (_moveInput != Vector2.zero)
            {
                _animator.PlayIdle(_moveInput);
            }
        }   
        
        private void OnMove(InputAction.CallbackContext context) =>
            _moveInput = context.ReadValue<Vector2>();

        private void OnEnable()
        {
            _inputActions.Enable();
            _inputActions.Player.Move.performed += OnMove;
            _inputActions.Player.Move.canceled += OnMove;   
        }

        private void OnDisable()
        {
            _inputActions.Disable();
            _inputActions.Player.Move.performed -= OnMove;
            _inputActions.Player.Move.canceled -= OnMove;
        }
    }
}
