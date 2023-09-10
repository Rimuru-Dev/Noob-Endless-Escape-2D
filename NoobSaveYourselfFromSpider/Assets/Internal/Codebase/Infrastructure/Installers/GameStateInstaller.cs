// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System.Diagnostics.CodeAnalysis;
using Internal.Codebase.Infrastructure.GeneralGameStateMachine.StateMachine;
using Internal.Codebase.Infrastructure.GeneralGameStateMachine.States;
using Zenject;

namespace Internal.Codebase.Infrastructure.Installers
{
    [SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
    public sealed class GameStateInstaller : MonoInstaller
    {
        public override void InstallBindings() =>
            BindGameStateMachine();

        private void BindGameStateMachine()
        {
            Container.Bind<BootstrapState>().AsSingle();
            Container.Bind<LoadMainMenuState>().AsSingle();
            Container.Bind<GameplaySceneState>().AsSingle();
            Container.Bind<GameOverState>().AsSingle();

            Container.Bind<GameStateMachine>().AsSingle();
        }
    }
}