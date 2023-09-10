// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Internal.Codebase.Infrastructure.GeneralGameStateMachine.Interfaces;
using Internal.Codebase.Infrastructure.GeneralGameStateMachine.States;
using Zenject;

namespace Internal.Codebase.Infrastructure.GeneralGameStateMachine.StateMachine
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
    [SuppressMessage("ReSharper", "SuggestBaseTypeForParameterInConstructor")]
    public sealed class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> states;
        private IExitableState currentState;

        [Inject]
        public GameStateMachine(
            BootstrapState bootstrapState,
            LoadMainMenuState loadMainMenuState,
            GameplaySceneState gameplaySceneState,
            GameOverState gameOverState,
            RebirdthState rebirdthState)
        {
            states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = bootstrapState,
                [typeof(LoadMainMenuState)] = loadMainMenuState,
                [typeof(GameplaySceneState)] = gameplaySceneState,
                [typeof(GameOverState)] = gameOverState,
                [typeof(RebirdthState)] = rebirdthState,
            };
        }

        public void Init()
        {
            states[typeof(BootstrapState)].Init(this);
            states[typeof(LoadMainMenuState)].Init(this);
            states[typeof(GameplaySceneState)].Init(this);
            states[typeof(GameOverState)].Init(this);
            states[typeof(RebirdthState)].Init(this);
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
    }
}