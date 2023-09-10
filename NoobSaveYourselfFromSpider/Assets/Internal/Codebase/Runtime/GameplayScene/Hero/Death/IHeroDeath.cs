using System;
using UnityEngine;

namespace Internal.Codebase.Runtime.GameplayScene.Hero.Death
{
    public interface IHeroDeath : IDisposable
    {
        public void PerformDeath(Collider2D collision);
        public void Subscribe(Action action);
        public void Unsubscribe(Action action);
        public void UnsubscribeAll();
    }
}