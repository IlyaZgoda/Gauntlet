using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Code.Logic.Enemy
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private AgentMoveToPlayer _agentMove;

        [SerializeField] private float _cooldown;
        private bool _hasAggroTarget;

        private WaitForSeconds _switchFollowOffAfterCooldown;
        private Coroutine _aggroCoroutine;

        private void Start()
        {
            _switchFollowOffAfterCooldown = new WaitForSeconds(_cooldown);

            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;
        }

        private void OnDestroy()
        {
            _triggerObserver.TriggerEnter -= TriggerEnter;
            _triggerObserver.TriggerExit -= TriggerExit;
        }

        private void TriggerEnter(Collider2D obj)
        {
            if (_hasAggroTarget) return;

            StopAggroCoroutine();

            SwitchFollowOn();
        }

        private void TriggerExit(Collider2D obj)
        {
            if (!_hasAggroTarget) return;

            _aggroCoroutine = StartCoroutine(SwitchFollowOffAfterCooldown());
        }

        private void StopAggroCoroutine()
        {
            if (_aggroCoroutine == null) return;

            StopCoroutine(_aggroCoroutine);
            _aggroCoroutine = null;
        }

        private IEnumerator SwitchFollowOffAfterCooldown()
        {
            yield return _switchFollowOffAfterCooldown;

            SwitchFollowOff();
        }

        private void SwitchFollowOn()
        {
            _hasAggroTarget = true;
            _agentMove.enabled = false;
        }

        private void SwitchFollowOff()
        {
            _agentMove.enabled = true;
            _hasAggroTarget = false;
        }

    }
}
