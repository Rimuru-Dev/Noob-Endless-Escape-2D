// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using System.Collections.Generic;

namespace Internal.Codebase.Runtime.Enemy
{
    public sealed class EnemyStateMachine
    {
        public event Action<State> OnStateChanged;
        public State CurrentState { get; private set; }

        private Dictionary<Type, State> availableState;

        public void OnUpdate()
        {
            var nextState = CurrentState?.Tick();

            if (nextState != null && nextState != CurrentState?.GetType())
                SwitchState(nextState);
        }

        public void SetStatus(Dictionary<Type, State> status)
        {
            availableState = status;
        }

        public void SetInitialState(Type state)
        {
            CurrentState = availableState[state];
        }

        public void SwitchState(Type nextState)
        {
            CurrentState?.OnStateExit();
            CurrentState = availableState[nextState];
            CurrentState.OnStateEnter();

            OnStateChanged?.Invoke(CurrentState);
        }
    }
}