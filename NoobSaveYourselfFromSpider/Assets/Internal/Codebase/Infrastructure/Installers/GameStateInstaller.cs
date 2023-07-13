// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System.Diagnostics.CodeAnalysis;
using AbyssMoth.Internal.Codebase.Infrastructure.StateMachine;
using AbyssMoth.Internal.Codebase.Infrastructure.StateMachine.States;
using Zenject;

namespace AbyssMoth.Internal.Codebase.Infrastructure.Installers
{
    [SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
    public class GameStateInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameStateMachine();
        }

        private void BindGameStateMachine()
        {
            Container.Bind<BootstrapState>().AsSingle();
            Container.Bind<LoadMaiMenuState>().AsSingle();

            Container.Bind<GameStateMachine>().AsSingle();
        }
    }
}