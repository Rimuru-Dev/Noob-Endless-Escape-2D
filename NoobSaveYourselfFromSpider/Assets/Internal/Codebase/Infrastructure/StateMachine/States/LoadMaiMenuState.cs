// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Internal.Codebase.Infrastructure.Factory.UI;
using Internal.Codebase.Infrastructure.Services.Curtain;
using Internal.Codebase.Infrastructure.Services.SceneLoader;
using Internal.Codebase.Infrastructure.Services.StaticData;
using Internal.Codebase.Infrastructure.StateMachine.Interfaces;
using Internal.Codebase.Runtime.MainMenu;
using Internal.Codebase.Utilities.Constants;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Internal.Codebase.Infrastructure.StateMachine.States
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
            PrepareUI();

            // *** Hide Curtain *** //
            var config = staticData.ForCurtain();
            curtain.HideCurtain(config.HideDelay);

            Object.FindObjectOfType<Button>().onClick.AddListener(OnSceneLoaded);
        }

        private void PrepareUI()
        {
            uiFactory.CreateMainMenuRoot();
            uiFactory.CreateDynamicCanvas();

            MainMenuCanvasView mainMenu = uiFactory.CreateMainMenuCanvas();
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
}