using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Code.Logic.Enemy
{
    public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
    {
        [SerializeField] private Animator _animator;

        private static readonly int HorizontalHash = Animator.StringToHash("Horizontal");
        private static readonly int VerticalHash = Animator.StringToHash("Vertical");
        private static readonly int LastHorizontalHash = Animator.StringToHash("LastHorizontal");
        private static readonly int LastVerticalHash = Animator.StringToHash("LastVertical");

        private static readonly Dictionary<AnimatorState, int> StateHashes = new()
        {
            { AnimatorState.Idle, Animator.StringToHash("Idle") },
            { AnimatorState.Attack, Animator.StringToHash("Attack") },
            { AnimatorState.Run, Animator.StringToHash("Run") },
            { AnimatorState.TakeDamage, Animator.StringToHash("TakeDamage")},
            { AnimatorState.Died, Animator.StringToHash("Died") }
        };

        public Action<AnimatorState> StateEntered;
        public Action<AnimatorState> StateExited;

        public AnimatorState State { get; private set; }
        public bool IsAttacking => State == AnimatorState.Attack;

        public void PlayRun(Vector2 moveInput)
        {
            _animator.SetFloat(HorizontalHash, moveInput.x);
            _animator.SetFloat(VerticalHash, moveInput.y);
            _animator.SetTrigger(StateHashes[AnimatorState.Run]);
        }

        public void PlayIdle(Vector2 moveInput)
        {
            _animator.SetFloat(LastHorizontalHash, moveInput.x);
            _animator.SetFloat(LastVerticalHash, moveInput.y);
            _animator.SetTrigger(StateHashes[AnimatorState.Idle]);
        }

        public void PlayAttack() =>
            _animator.SetTrigger(StateHashes[AnimatorState.Attack]);

        public void PlayTakingDamage() =>
            _animator.SetTrigger(StateHashes[AnimatorState.TakeDamage]);

        public void PlayDeath() =>
            _animator.SetTrigger(StateHashes[AnimatorState.Died]);

        public void OnEnter(int stateHash)
        {
            State = StateFor(stateHash);

            StateEntered?.Invoke(State);
            //Debug.Log($"State {State} entered");
        }

        public void OnExit(int stateHash)
        {
            var state = StateFor(stateHash);
            StateExited?.Invoke(state);
            //Debug.Log($"State {State} exited");
        }

        private AnimatorState StateFor(int stateHash)
        {
            foreach (var state in StateHashes)
            {
                if (state.Value.Equals(stateHash))
                    return state.Key;
            }

            return AnimatorState.Unknown;
        }

        private void OnDeathEnd() =>
            _animator.speed = 0;
    }
}

