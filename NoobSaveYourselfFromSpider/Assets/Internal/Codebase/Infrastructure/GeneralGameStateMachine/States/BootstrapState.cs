// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using DG.Tweening;
using Internal.Codebase.Infrastructure.GeneralGameStateMachine.Interfaces;
using Internal.Codebase.Infrastructure.GeneralGameStateMachine.StateMachine;
using Internal.Codebase.Infrastructure.Services.CloudSave;
using Internal.Codebase.Infrastructure.Services.Curtain;
using Internal.Codebase.Infrastructure.Services.SceneLoader;
using Internal.Codebase.Infrastructure.Services.StaticData;
using Internal.Codebase.Runtime.General.Settings;
using Internal.Codebase.Utilities.Constants;
using Zenject;

namespace Internal.Codebase.Infrastructure.GeneralGameStateMachine.States
{
    public sealed class BootstrapState : IStateNext
    {
        private readonly ICurtainService curtain;
        private readonly IStaticDataService staticData;
        private readonly ISceneLoaderService sceneLoader;
        private readonly IYandexSaveService yandexSaveService;
        private GameStateMachine gameStateMachine;

        [Inject]
        public BootstrapState(
            ICurtainService curtain,
            IStaticDataService staticData,
            ISceneLoaderService sceneLoader,
            IYandexSaveService yandexSaveService)
        {
            this.staticData = staticData;
            this.sceneLoader = sceneLoader;
            this.curtain = curtain;
            this.yandexSaveService = yandexSaveService;
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
            yandexSaveService.Init();

            DOTween.Init();

            staticData.Initialize();

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