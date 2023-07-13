﻿// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AbyssMoth.Internal.Codebase.Infrastructure.StateMachine.Interfaces;
using AbyssMoth.Internal.Codebase.Infrastructure.StateMachine.States;
using Zenject;

namespace AbyssMoth.Internal.Codebase.Infrastructure.StateMachine
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
    [SuppressMessage("ReSharper", "SuggestBaseTypeForParameterInConstructor")]
    public sealed class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> states;
        private IExitableState currentState;

        [Inject]
        public GameStateMachine(BootstrapState bootstrapState, LoadMaiMenuState loadMaiMenuState)
        {
            states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = bootstrapState,
                [typeof(LoadMaiMenuState)] = loadMaiMenuState
            };
        }

        public void EnterState<TState>() where TState : IExitableState
        {
            currentState?.Exit();

            var state = states[typeof(TState)];

            (state as IStateNext)?.Enter();

            currentState = state;
        }

        public void EnterState<TState, TArgs>(TArgs args) where TState : IExitableState
        {
            currentState?.Exit();

            var state = states[typeof(TState)];

            (state as IStateWithArgument<TArgs>)?.Enter(args);

            currentState = state;
        }

        public void Init()
        {
            states[typeof(BootstrapState)].Init(this);
            states[typeof(LoadMaiMenuState)].Init(this);
        }
    }
}