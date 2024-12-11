using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Logic
{
    public enum AnimatorState
    {
        Unknown,
        Idle,
        Attack,
        SecondAttack,
        SpecialAttack,
        Run,
        Died,
        TakeDamage
    }
}
