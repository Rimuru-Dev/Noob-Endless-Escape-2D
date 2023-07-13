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
    public sealed class LoadMaiMenuState : IStateNext
    {
        private readonly ICurtainService curtain;
        private readonly ISceneLoaderService sceneLoader;
        private readonly IStaticDataService staticData;
        private readonly IUIFactory uiFactory;
        private IGameStateMachine gameStateMachine;

        [Inject]
        public LoadMaiMenuState(
            IStaticDataService staticData,
            ICurtainService curtain,
            IUIFactory uiFactory,
            ISceneLoaderService sceneLoader)
        {
            this.sceneLoader = sceneLoader;
            this.staticData = staticData;
            this.curtain = curtain;
            this.uiFactory = uiFactory;
        }

        public void Init(IGameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            // *** Hide Curtain *** //
            var config = staticData.ForCurtain();
            curtain.HideCurtain(config.HideDelay);
        }

        public void Exit()
        {
        }

        private void OnSceneLoaded()
        {
            // curtain.ShowCurtain(true,
            //     () =>
            //     {
            //         sceneLoader.LoadScene(//* Next Scene Name*//,
            //             () => { gameStateMachine.EnterState<//* Next Scene *//>(); });
            //     });
        }
    }
}