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
using AbyssMoth.Internal.Codebase.Utilities.Constants;
using UnityEngine;
using UnityEngine.UI;
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

            Object.FindObjectOfType<Button>().onClick.AddListener(OnSceneLoaded);
        }

        public void Exit()
        {
        }

        private void OnSceneLoaded()
        {
            curtain.ShowCurtain(true, () =>
            {
                sceneLoader.LoadScene(SceneName.Gameplay,
                    () => { gameStateMachine.EnterState<GameplaySceneState>(); });
            });
        }
    }

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