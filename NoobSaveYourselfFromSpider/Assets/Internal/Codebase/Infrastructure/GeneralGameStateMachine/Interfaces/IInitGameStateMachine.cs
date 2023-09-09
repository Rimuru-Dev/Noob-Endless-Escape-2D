// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Internal.Codebase.Infrastructure.GeneralGameStateMachine.StateMachine;

namespace Internal.Codebase.Infrastructure.GeneralGameStateMachine.Interfaces
{
    public interface IInitGameStateMachine
    {
        public void Init(GameStateMachine stateMachine);
    }
}