using System;
using UnityEngine;

namespace Internal.Codebase.Runtime.General.TriggerObservers
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Collider2D))]
    public sealed class TriggerObserver2D : MonoBehaviour
    {
        public event Action<Collider2D> OnCollisionEnter2D;
        public event Action<Collider2D> OnCollisionStay2D;
        public event Action<Collider2D> OnCollisionExit2D;

        private void OnTriggerEnter2D(Collider2D other) =>
            OnCollisionEnter2D?.Invoke(other);

        private void OnTriggerStay2D(Collider2D other) =>
            OnCollisionStay2D?.Invoke(other);

        private void OnTriggerExit2D(Collider2D other) =>
            OnCollisionExit2D?.Invoke(other);
    }
}