using System;
using UnityEngine;

namespace Internal.Codebase.Runtime.General.TriggerObservers
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Collider))]
    public sealed class TriggerObserver3D : MonoBehaviour
    {
        public event Action<Collider> OnCollisionEnter;
        public event Action<Collider> OnCollisionStay;
        public event Action<Collider> OnCollisionExit;

        private void OnTriggerEnter(Collider other) =>
            OnCollisionEnter?.Invoke(other);

        private void OnTriggerStay(Collider other) =>
            OnCollisionStay?.Invoke(other);

        private void OnTriggerExit(Collider other) =>
            OnCollisionExit?.Invoke(other);
    }
}