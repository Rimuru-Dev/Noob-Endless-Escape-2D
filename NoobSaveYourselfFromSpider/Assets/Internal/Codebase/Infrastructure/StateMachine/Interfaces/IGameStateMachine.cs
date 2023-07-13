// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

namespace AbyssMoth.Internal.Codebase.Infrastructure.StateMachine.Interfaces
{
    public interface IGameStateMachine
    {
        public void EnterState<TState>() where TState : IExitableState;

        public void EnterState<TState, TArgs>(TArgs args) where TState : IExitableState;
    }
}