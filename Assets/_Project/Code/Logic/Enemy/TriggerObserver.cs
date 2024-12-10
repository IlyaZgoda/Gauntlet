using System;
using UnityEngine;

namespace Code.Logic.Enemy
{
    public class TriggerObserver : MonoBehaviour
    {
        public event Action<Collider2D> TriggerEnter;
        public event Action<Collider2D> TriggerExit;

        private void OnTriggerEnter2D(Collider2D other)
        {
            TriggerEnter?.Invoke(other);
            Debug.Log(other.gameObject);
        }

        private void OnTriggerExit2D(Collider2D other) =>
          TriggerExit?.Invoke(other);
    }
}
