// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using System.Linq;
using Cinemachine;
using Internal.Codebase.Infrastructure.AssetManagement;
using Internal.Codebase.Infrastructure.Factory;
using Internal.Codebase.Infrastructure.Factory.Game;
using Internal.Codebase.Infrastructure.Factory.Hero;
using Internal.Codebase.Infrastructure.Factory.UI;
using Internal.Codebase.Infrastructure.Services.Curtain;
using Internal.Codebase.Infrastructure.Services.PersistenProgress;
using Internal.Codebase.Infrastructure.Services.SceneLoader;
using Internal.Codebase.Infrastructure.Services.StaticData;
using Internal.Codebase.Infrastructure.StateMachine.Interfaces;
using Internal.Codebase.Runtime.EndlessLevelGenerationSolution.Handlers;
using Internal.Codebase.Runtime.GameplayScene;
using Internal.Codebase.Runtime.Hero;
using Internal.Codebase.Utilities.Constants;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Internal.Codebase.Infrastructure.StateMachine.States
{
    public sealed class GameplaySceneState : IStateNext
    {
        private readonly ICurtainService curtain;
        private readonly ISceneLoaderService sceneLoader;
        private readonly IStaticDataService staticData;
        private readonly IUIFactory uiFactory;
        private readonly IHeroFactory heroFactory;
        private readonly IGameFactory gameFactory;
        private readonly IPersistenProgressService persistenProgressService;
        private GameStateMachine gameStateMachine;

        [Inject]
        public GameplaySceneState(
            ICurtainService curtain,
            ISceneLoaderService sceneLoader,
            IStaticDataService staticData,
            IUIFactory uiFactory,
            IHeroFactory heroFactory,
            IGameFactory gameFactory,
            IPersistenProgressService persistenProgressService)
        {
            this.curtain = curtain;
            this.sceneLoader = sceneLoader;
            this.staticData = staticData;
            this.uiFactory = uiFactory;
            this.heroFactory = heroFactory;
            this.gameFactory = gameFactory;
            this.persistenProgressService = persistenProgressService;
        }

        public void Init(GameStateMachine gameStateMachine) =>
            this.gameStateMachine = gameStateMachine;

        public void Enter()
        {
            PrepareScene();

            // *** Hide Curtain *** //
            var config = staticData.ForCurtain();
            curtain.HideCurtain(config.HideDelay);
        }

        private SceneController sceneController;

        private void PrepareScene()
        {
            var hero = heroFactory.CreateHero();
            heroFactory.CreateHeroCamera();
            hero.transform.position = new Vector3(0, 7f, 0);

            var skinDatas = staticData.ForSkins().gameplaySkinDatas;
            var userSkin = persistenProgressService.GetStoragesData().userSkins;

            hero.HeroSpriteRenderer.sprite = skinDatas.FirstOrDefault(x => x.id == userSkin.selectionSkinId)!.icon;

            var levelGenerator = gameFactory.CreateLevelGenerator();
            levelGenerator.Prepare();

            sceneController = GameObject.FindObjectOfType<SceneController>();
            sceneController.Container(hero, gameStateMachine, sceneLoader, OnSceneLoaded, levelGenerator,  persistenProgressService.GetStoragesData()); //(() =>
            // {
            //     // Перенеси ссылку на стейт машину и лоадер в SceneController!!
            //     sceneLoader.LoadScene(SceneName.Menu, (() => { gameStateMachine.EnterState<LoadMaiMenuState>(); }));
            //     
            // })); //OnSceneLoaded);
        }

        private void LeaveToMainMenuState() =>
            curtain.ShowCurtain(true, LoadScene);

        private void LoadScene() =>
            sceneLoader.LoadScene(SceneName.Menu, EnterMainMenuState);

        private void EnterMainMenuState() => gameStateMachine.EnterState<LoadMaiMenuState>();

        private void OnSceneLoaded()
        {
            curtain.ShowCurtain(true,
                () =>
                {
                    sceneLoader.LoadScene(SceneName.Menu, (() =>
                    {
                        Debug.Log($"gameStateMachine == null? - {gameStateMachine == null}");
                        gameStateMachine.EnterState<LoadMaiMenuState>();
                    }));
                });
        }

        public void Exit()
        {
            // gameStateMachine.EnterState<LoadMaiMenuState>();
        }
    }
}