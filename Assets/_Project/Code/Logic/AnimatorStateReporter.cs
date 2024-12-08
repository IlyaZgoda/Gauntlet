using Code.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Code.Logic
{
    public class AnimatorStateReporter : StateMachineBehaviour
    {
        private IAnimationStateReader _stateReader;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            FindReader(animator);

            _stateReader.OnEnter(stateInfo.shortNameHash);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            FindReader(animator);

            _stateReader.OnExit(stateInfo.shortNameHash);
        }

        private void FindReader(Animator animator)
        {
            if (_stateReader != null)
                return;

            _stateReader = animator.gameObject.GetComponent<IAnimationStateReader>();
        }

    }
}
