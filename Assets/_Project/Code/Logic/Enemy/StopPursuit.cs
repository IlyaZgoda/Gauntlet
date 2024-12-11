using Code.Logic.Hero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Logic.Enemy
{
    public class StopPursuit : MonoBehaviour
    {

        private HeroDeath _heroDeath;
        [SerializeField] private EnemyAttack _enemyAttack;
        [SerializeField] private AgentMoveToPlayer _agentMoveToPlayer;

        private void Start()
        {
            if ( _heroDeath != null ) 
                _heroDeath.Happened += SwitchPursuitOff;
        }

        private void OnDestroy()
        {
            if (_heroDeath != null)
                _heroDeath.Happened -= SwitchPursuitOff;
        }

        public void Construct(HeroDeath heroDeath) =>
            _heroDeath = heroDeath;

        private void SwitchPursuitOff()
        {
            _enemyAttack.enabled = false;
            _agentMoveToPlayer.enabled = false;
        }

    }
}
