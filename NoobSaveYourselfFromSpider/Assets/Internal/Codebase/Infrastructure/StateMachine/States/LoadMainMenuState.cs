﻿// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Internal.Codebase.Infrastructure.Factory.UI;
using Internal.Codebase.Infrastructure.Services.CloudSave;
using Internal.Codebase.Infrastructure.Services.Curtain;
using Internal.Codebase.Infrastructure.Services.SceneLoader;
using Internal.Codebase.Infrastructure.Services.StaticData;
using Internal.Codebase.Infrastructure.StateMachine.Interfaces;
using Internal.Codebase.Runtime;
using Internal.Codebase.Runtime.BiomeShop;
using Internal.Codebase.Runtime.EndlessLevelGenerationSolution.Configs;
using Internal.Codebase.Runtime.MainMenu;
using Internal.Codebase.Utilities.Constants;
using UnityEngine;
using YG;
using Zenject;

namespace Internal.Codebase.Infrastructure.StateMachine.States
{
    public sealed class LoadMainMenuState : IStateNext
    {
        private readonly ICurtainService curtain;
        private readonly ISceneLoaderService sceneLoader;
        private readonly IYandexSaveService saveService;
        private readonly IStaticDataService staticData;
        private readonly IUIFactory uiFactory;
        private GameStateMachine gameStateMachine;

        // private MainMenuCanvasView mainMenu;
        private BiomeShopView biomeShop;

        [Inject]
        public LoadMainMenuState(
            IUIFactory uiFactory,
            ICurtainService curtain,
            IStaticDataService staticData,
            ISceneLoaderService sceneLoader,
            IYandexSaveService saveService)
        {
            this.curtain = curtain;
            this.uiFactory = uiFactory;
            this.staticData = staticData;
            this.sceneLoader = sceneLoader;
            this.saveService = saveService;
        }

        public void Init(GameStateMachine stateMachine) =>
            gameStateMachine = stateMachine;

        public void Enter()
        {
            PrepareUI();
            HideCurtain();
        }

        // TODO: Added Disposable Service
        public void Exit()
        {
            biomeShop.PlayBiomeForest.onClick.RemoveListener(PlayForest);
            biomeShop.PlayBiomWinter.onClick.RemoveListener(PlayWinter);
            // mainMenu.PlayButton.onClick.RemoveListener(OnSceneLoaded);
        }

        private void PrepareUI()
        {
            uiFactory
                .CreateMainMenuUIRoot()
                .CreateMainMenuBackgraund()
                .CreateMainMenu();

            // Setup Biome Panel
            var uiRoot = uiFactory.MainMenuUIRoot;

            biomeShop = uiRoot.MenuCanvasView.BiomeShopView;
            biomeShop.Constructor(staticData);

            biomeShop.PlayBiomeForest.onClick.AddListener(PlayForest);
            biomeShop.PlayBiomWinter.onClick.AddListener(PlayWinter);

            // Setup Settings Panel
            uiRoot.MenuCanvasView.SettingsView.GeneralAudioHandler.Constructor(saveService);
        }

        private void PlayWinter()
        {
            YandexGame.savesData.storage.userBioms.selectionBiomId = BiomeTypeID.SnowyWastelands;
            OnSceneLoaded();
        }

        private void PlayForest()
        {
            YandexGame.savesData.storage.userBioms.selectionBiomId = BiomeTypeID.GreenPlains;
            OnSceneLoaded();
        }

        private void HideCurtain()
        {
            var config = staticData.ForCurtain();
            curtain.HideCurtain(config.HideDelay);
        }

        private void OnSceneLoaded() =>
            curtain.ShowCurtain(true, LoadGameplayScene);

        private void LoadGameplayScene() =>
            sceneLoader.LoadScene(SceneName.Gameplay, EnterGameplayState);

        private void EnterGameplayState() =>
            gameStateMachine.EnterState<GameplaySceneState>();
    }
}