using System;
using System.Collections.Generic;
using Internal.Codebase.Runtime.GameplayScene.Obstacles;
using Internal.Codebase.Utilities.Exceptions;
using UnityEngine;

namespace Internal.Codebase.Runtime.GameplayScene.Hero.Death
{
    public sealed class HeroDeath : IHeroDeath
    {
        private event Action OnDeath;
        private readonly List<Action> deathListeners;

        public HeroDeath() =>
            deathListeners = new List<Action>();

        ~HeroDeath() =>
            Dispose();

        public void PerformDeath(Collider2D collision) =>
            collision.TryGetComponentInChildrenAndInvoke(out DeadlyObstacle _, InvokePerformDeath);

        public void Subscribe(Action action)
        {
            if (deathListeners.Contains(action) || action == null)
                return;

            deathListeners.Add(action);

            OnDeath += action;

            Debug.Log($"Subscribe: {action.Target.GetType().FullName}");
        }

        public void Unsubscribe(Action action)
        {
            if (!deathListeners.Contains(action) || action == null)
                return;

            deathListeners.Remove(action);

            OnDeath -= action;
        }

        public void Dispose() =>
            OnDeath.UnsubscribeAndRemoveAndClearAll(deathListeners);

        private void InvokePerformDeath() =>
            OnDeath?.Invoke();
    }
}