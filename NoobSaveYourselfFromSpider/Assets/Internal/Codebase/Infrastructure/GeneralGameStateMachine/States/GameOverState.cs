// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Internal.Codebase.Infrastructure.GeneralGameStateMachine.Interfaces;
using Internal.Codebase.Infrastructure.GeneralGameStateMachine.StateMachine;

namespace Internal.Codebase.Infrastructure.GeneralGameStateMachine.States
{
    // В этом классе обработать вообще все что связано с GameOver
    // Очищать все ресы после перехода в MainMenu
    public sealed class GameOverState : IStateNext
    {
        public void Init(GameStateMachine stateMachine)
        {
        }

        public void Exit()
        {
        }

        public void Enter()
        {
        }
    }
    
    /// <summary>
    /// // TODO: Пересобрать мир но сохранить очки.
    /// </summary>
    public sealed class RebirdthState : IStateNext
    {
        public void Init(GameStateMachine stateMachine)
        {
        }

        public void Exit()
        {
        }

        public void Enter()
        {
        }
    }
}