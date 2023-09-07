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

namespace Internal.Codebase.Infrastructure.Services
{
    public interface IUpdateable
    {
        public void OnUpdate();
    }

    public interface ILateUpdatable : IUpdateable
    {
        public void OnLateUpdate();
    }

    public interface IFixedUpdatable : IUpdateable
    {
        public void OnFixedUpdate();
    }

    public interface IUpdater : IDisposable
    {
        public void Register(params IUpdateable[] updateables);
        public void Unregister(IUpdateable updateable);
    }

    public sealed class Updater : IUpdater
    {
        private readonly HashSet<IUpdateable> updateableSet = new();
        private readonly HashSet<IFixedUpdatable> fixedUpdatableSet = new();
        private readonly HashSet<ILateUpdatable> lateUpdatableSet = new();

        public void Register(params IUpdateable[] updateables)
        {
            foreach (var updateable in updateables)
            {
                if (updateable == null || updateableSet.Contains(updateable))
                    continue;

                if (updateable is ILateUpdatable lateUpdatable)
                    lateUpdatableSet.Add(lateUpdatable);
                else if (updateable is IFixedUpdatable fixedUpdatable)
                    fixedUpdatableSet.Add(fixedUpdatable);
                else
                    updateableSet.Add(updateable);
            }
        }

        public void Unregister(IUpdateable updateable)
        {
            if (updateable is ILateUpdatable lateUpdatable)
                lateUpdatableSet.Remove(lateUpdatable);
            else if (updateable is IFixedUpdatable fixedUpdatable)
                fixedUpdatableSet.Remove(fixedUpdatable);
            else
                updateableSet.Remove(updateable);
        }

        public void OnUpdate()
        {
            foreach (var updateable in updateableSet)
                updateable?.OnUpdate();
        }

        public void OnFixedUpdate()
        {
            foreach (var fixedUpdatable in fixedUpdatableSet)
                fixedUpdatable?.OnFixedUpdate();
        }

        public void OnLateUpdate()
        {
            foreach (var lateUpdatable in lateUpdatableSet)
                lateUpdatable?.OnLateUpdate();
        }

        public void Dispose()
        {
            updateableSet.Clear();
            fixedUpdatableSet.Clear();
            lateUpdatableSet.Clear();
        }
    }

    public static class UpdateableExtensions
    {
        public static void Unregister(this IUpdateable updateable, Updater updater) =>
            updater.Unregister(updateable);
    }
}