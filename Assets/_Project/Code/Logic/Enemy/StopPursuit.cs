using Code.Logic.Hero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Code.Logic.Enemy
{
    public class StopPursuit : MonoBehaviour
    {

        [SerializeField] private HeroDeath _heroDeath;
        [SerializeField] private EnemyAttack _enemyAttack;
        [SerializeField] private AgentMoveToPlayer _agentMoveToPlayer;

        private void Start()
        {
            _heroDeath.Happened += SwitchPursuitOff;
        }

        private void OnDestroy()
        {
            _heroDeath.Happened -= SwitchPursuitOff;
        }

        private void SwitchPursuitOff()
        {
            _enemyAttack.enabled = false;
            _agentMoveToPlayer.enabled = false;
        }

    }
}
