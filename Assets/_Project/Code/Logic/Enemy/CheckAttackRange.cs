using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Code.Logic.Enemy
{
    public class CheckAttackRange : MonoBehaviour
    {
        [SerializeField] private EnemyAttack _attack;
        [SerializeField] private TriggerObserver _triggerObserver;

        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;

            _attack.DisableAttack();
        }

        private void TriggerExit(Collider2D obj)
        {
            _attack.DisableAttack();
        }

        private void TriggerEnter(Collider2D obj)
        {
            _attack.EnableAttack();
        }

    }
}
