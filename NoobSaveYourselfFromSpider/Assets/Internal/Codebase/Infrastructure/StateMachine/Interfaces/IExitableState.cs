// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

namespace Internal.Codebase.Infrastructure.StateMachine.Interfaces
{
    public interface IExitableState : IInitGameStateMachine
    {
        public void Exit();
    }
}