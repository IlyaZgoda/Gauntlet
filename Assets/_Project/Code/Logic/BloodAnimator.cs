using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Code.Logic
{
    public class BloodAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private static readonly int BloodHash = Animator.StringToHash("Random"); 

        public void PlayBlood()
        {
            System.Random random = new ();
            var value = random.Next(1, 7);

            _animator.SetInteger(BloodHash, value);
        }

        public void StopBlood() =>
            _animator.SetInteger(BloodHash, 0);
    }
}
