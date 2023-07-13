// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using DG.Tweening;
using Internal.Codebase.Infrastructure.Services.Curtain;
using Internal.Codebase.Infrastructure.Services.SceneLoader;
using Internal.Codebase.Infrastructure.Services.StaticData;
using Internal.Codebase.Infrastructure.Services.Storage;
using Internal.Codebase.Infrastructure.StateMachine.Interfaces;
using Internal.Codebase.Utilities.Constants;
using Zenject;

namespace Internal.Codebase.Infrastructure.StateMachine.States
{
    public sealed class BootstrapState : IStateNext
    {
        private readonly ICurtainService curtain;
        private readonly ISceneLoaderService sceneLoader;
        private readonly IStaticDataService staticData;
        private readonly IStorageService storageService;
        private IGameStateMachine gameStateMachine;

        [Inject]
        public BootstrapState(
            IStaticDataService staticData,
            ISceneLoaderService sceneLoader,
            ICurtainService curtain,
            IStorageService storageService)
        {
            this.staticData = staticData;
            this.sceneLoader = sceneLoader;
            this.curtain = curtain;
            this.storageService = storageService;
        }

        public void Init(IGameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            PrepareServices();

            sceneLoader.LoadScene(SceneName.Menu, OnSceneLoaded);
        }

        public void Exit()
        {
        }

        private void PrepareServices()
        {
            // ** Localozation ** //
            // LocalizationManager.Read();

            // ** Tweens ** //
            DOTween.Init();

            // ** Static Data ** //
            staticData.Initialize();

            // ** Curtain ** //
            curtain.Init();
            curtain.ShowCurtain(false);
        }

        private void OnSceneLoaded()
        {
            gameStateMachine.EnterState<LoadMaiMenuState>();
        }
    }
}