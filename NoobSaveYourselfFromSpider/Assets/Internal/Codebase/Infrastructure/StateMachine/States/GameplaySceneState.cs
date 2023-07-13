// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using AbyssMoth.Internal.Codebase.Infrastructure.Factory.UI;
using AbyssMoth.Internal.Codebase.Infrastructure.Services.Curtain;
using AbyssMoth.Internal.Codebase.Infrastructure.Services.SceneLoader;
using AbyssMoth.Internal.Codebase.Infrastructure.Services.StaticData;
using AbyssMoth.Internal.Codebase.Infrastructure.StateMachine.Interfaces;
using Zenject;

namespace AbyssMoth.Internal.Codebase.Infrastructure.StateMachine.States
{
    public sealed class GameplaySceneState : IStateNext
    {
        private readonly ICurtainService curtain;
        private readonly ISceneLoaderService sceneLoader;
        private readonly IStaticDataService staticData;
        private readonly IUIFactory uiFactory;
        private IGameStateMachine gameStateMachine;

        [Inject]
        public GameplaySceneState(
            ICurtainService curtain,
            ISceneLoaderService sceneLoader,
            IStaticDataService staticData,
            IUIFactory uiFactory)
        {
            this.curtain = curtain;
            this.sceneLoader = sceneLoader;
            this.staticData = staticData;
            this.uiFactory = uiFactory;
        }

        public void Init(IGameStateMachine gameStateMachine) =>
            this.gameStateMachine = gameStateMachine;

        public void Enter()
        {
            // *** Hide Curtain *** //
            var config = staticData.ForCurtain();
            curtain.HideCurtain(config.HideDelay);
        }

        public void Exit()
        {
        }
    }
}