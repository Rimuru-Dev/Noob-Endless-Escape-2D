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
using Internal.Codebase.Runtime.MainMenu.Animation;
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
        private readonly IPersistenProgressService persistenProgressService;
        private readonly IStaticDataService staticData;
        private readonly IUIFactory uiFactory;
        private IGameStateMachine gameStateMachine;

        private MainMenuCanvasView mainMenu;

        [Inject]
        public LoadMaiMenuState(
            IStaticDataService staticData,
            ICurtainService curtain,
            IUIFactory uiFactory,
            ISceneLoaderService sceneLoader,
            IPersistenProgressService persistenProgressService)
        {
            this.sceneLoader = sceneLoader;
            this.persistenProgressService = persistenProgressService;
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
        }

        private void PrepareUI()
        {
            uiFactory.CreateMainMenuRoot();
            uiFactory.CreateDynamicCanvas();

            mainMenu = uiFactory.CreateMainMenuCanvas();

            var storage = persistenProgressService.GetStoragesData();

            mainMenu.Emerald.Initialize(storage);
            mainMenu.Fish.Initialize(storage);
            mainMenu.BestDistance.Initialize(storage);
            storage.Refresh();

            mainMenu.PlayButton.onClick.AddListener(OnSceneLoaded);

            mainMenu.gameObject
                .GetComponent<CharacterSwitcher>()
                .Initialize(storage, persistenProgressService);
        }

        public void Exit()
        {
            mainMenu.PlayButton.onClick.RemoveListener(OnSceneLoaded);
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