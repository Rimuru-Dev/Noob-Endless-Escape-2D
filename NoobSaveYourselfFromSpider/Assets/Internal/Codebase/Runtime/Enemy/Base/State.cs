using System;
using UnityEngine;

namespace Internal.Codebase.Runtime.Enemy
{
    [Serializable]
    public abstract class State
    {
        public readonly GameObject GameObject;
        public Transform Transform { get; set; }

        protected State(GameObject gameObject) =>
            GameObject = gameObject;

        public abstract Type Tick();

        public virtual void Init()
        {
        }

        public virtual void OnStateExit()
        {
        }

        public virtual void OnStateEnter()
        {
        }
    }
}