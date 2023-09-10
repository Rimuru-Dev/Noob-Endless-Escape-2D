// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

namespace Internal.Codebase.Infrastructure.GeneralGameStateMachine.Interfaces
{
    public interface IStateNext : IExitableState
    {
        public void Enter();
    }
}