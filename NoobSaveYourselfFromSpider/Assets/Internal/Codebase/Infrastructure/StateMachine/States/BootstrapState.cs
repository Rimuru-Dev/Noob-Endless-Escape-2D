// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Zenject;
using DG.Tweening;
using Internal.Codebase.Runtime.Settings;
using Internal.Codebase.Utilities.Constants;
using Internal.Codebase.Infrastructure.Services.Curtain;
using Internal.Codebase.Infrastructure.Services.CloudSave;
using Internal.Codebase.Infrastructure.Services.StaticData;
using Internal.Codebase.Infrastructure.Services.SceneLoader;
using Internal.Codebase.Infrastructure.StateMachine.Interfaces;

namespace Internal.Codebase.Infrastructure.StateMachine.States
{
    public sealed class BootstrapState : IStateNext
    {
        private readonly ICurtainService curtain;
        private readonly IStaticDataService staticData;
        private readonly ISceneLoaderService sceneLoader;
        private readonly IYandexGamesCloudSaveService yandexGamesCloudSaveService;
        private GameStateMachine gameStateMachine;

        [Inject]
        public BootstrapState(
            ICurtainService curtain,
            IStaticDataService staticData,
            ISceneLoaderService sceneLoader,
            IYandexGamesCloudSaveService yandexGamesCloudSaveService)
        {
            this.staticData = staticData;
            this.sceneLoader = sceneLoader;
            this.curtain = curtain;
            this.yandexGamesCloudSaveService = yandexGamesCloudSaveService;
        }

        public void Init(GameStateMachine stateMachine) =>
            gameStateMachine = stateMachine;

        public void Enter()
        {
            PrepareServices();
            AppyGeneralSettings();
            LoadMainMenuScene();
        }

        public void Exit()
        {
        }

        private void PrepareServices()
        {
            // ** User Data ** //
            yandexGamesCloudSaveService.Init();

            // ** Tweens ** //
            DOTween.Init();

            // ** Static Data ** //
            staticData.Initialize();

            // ** Curtain ** //
            curtain.Init();
            curtain.ShowCurtain(false);
        }

        private static void AppyGeneralSettings()
        {
            var fps = new FPSSettings();
            fps.ApplyTargetFrameRate();
        }

        private void LoadMainMenuScene() =>
            sceneLoader.LoadScene(SceneName.Menu, OnSceneLoaded);

        private void OnSceneLoaded() =>
            gameStateMachine.EnterState<LoadMainMenuState>();
    }
}