// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Project: "Murders Drones Endless Way" 
//   Contact me: 
//          - Gmail:    rimuru.dev@gmail.com
//          - GitHub:   https://github.com/RimuruDev
//          - LinkedIn: https://www.linkedin.com/in/rimuru/
//          - GitHub Organizations: https://github.com/Rimuru-Dev
//
// **************************************************************** //

using System;
using System.Collections.Generic;
using Internal.Codebase.Runtime.GameplayScene.Hero.View;
using Internal.Codebase.Runtime.GameplayScene.Obstacles;
using Internal.Codebase.Utilities.Exceptions;
using UnityEngine;

namespace Internal.Codebase.Runtime.GameplayScene.Hero
{
    public sealed class HeroController : IDisposable
    {
        private readonly HeroViewController heroView;
        private readonly IHeroDeath heroDeath;

        public HeroController(HeroViewController heroView, IHeroDeath heroDeath)
        {
            this.heroView = heroView;
            this.heroDeath = heroDeath;

            Prepare();
        }

        ~HeroController() =>
            Dispose();

        // Pass to the stage exit state of gameplay to be cleared.
        public void Dispose()
        {
            heroView.DeathObserver.OnCollisionEnter2D -= heroDeath.PerformDeath;
            heroDeath?.Dispose();
        }

        private void Prepare()
        {
            heroView.DeathObserver.OnCollisionEnter2D += heroDeath.PerformDeath;
        }
    }

    public interface IHeroDeath : IDisposable
    {
        public void PerformDeath(Collider2D collision);
        public void Subscribe(Action action);
        public void Unsubscribe(Action action);
    }

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