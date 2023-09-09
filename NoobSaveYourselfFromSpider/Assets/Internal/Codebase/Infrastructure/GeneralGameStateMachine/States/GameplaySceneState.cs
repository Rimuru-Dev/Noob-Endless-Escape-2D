// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System.Linq;
using Internal.Codebase.Infrastructure.Factory.Game;
using Internal.Codebase.Infrastructure.Factory.Hero;
using Internal.Codebase.Infrastructure.GeneralGameStateMachine.Interfaces;
using Internal.Codebase.Infrastructure.GeneralGameStateMachine.StateMachine;
using Internal.Codebase.Infrastructure.Services.CloudSave;
using Internal.Codebase.Infrastructure.Services.Curtain;
using Internal.Codebase.Infrastructure.Services.SceneLoader;
using Internal.Codebase.Infrastructure.Services.StaticData;
using Internal.Codebase.Runtime.GameplayScene.LevelController;
using Internal.Codebase.Utilities.Constants;
using UnityEngine;
using YG;
using Zenject;

namespace Internal.Codebase.Infrastructure.GeneralGameStateMachine.States
{
    public sealed class GameplaySceneState : IStateNext
    {
        private readonly ICurtainService curtain;
        private readonly ISceneLoaderService sceneLoader;
        private readonly IStaticDataService staticData;
        private readonly IHeroFactory heroFactory;
        private readonly IGameFactory gameFactory;
        private readonly IYandexSaveService saveService;
        private GameStateMachine gameStateMachine;
        private SceneController sceneController;

        [Inject]
        public GameplaySceneState(
            ICurtainService curtain,
            ISceneLoaderService sceneLoader,
            IStaticDataService staticData,
            IHeroFactory heroFactory,
            IGameFactory gameFactory,
            IYandexSaveService saveService
        )
        {
            this.curtain = curtain;
            this.sceneLoader = sceneLoader;
            this.staticData = staticData;
            this.heroFactory = heroFactory;
            this.gameFactory = gameFactory;
            this.saveService = saveService;
        }

        public void Init(GameStateMachine stateMachine) =>
            gameStateMachine = stateMachine;

        public void Enter()
        {
            PrepareScene();
            HideCurtain();
        }

        public void Exit()
        {
        }

        private void HideCurtain()
        {
            var config = staticData.ForCurtain();
            curtain.HideCurtain(config.HideDelay);
        }

        private void PrepareScene()
        {
            var hero = heroFactory.CreateHero();
            heroFactory.CreateHeroCamera();
            hero.transform.position = new Vector3(0, 7f, 0);

            var skinDatas = staticData.ForSkins().gameplaySkinDatas;
            var userSkin = YandexGame.savesData.storage.userSkins;

            hero.HeroSpriteRenderer.sprite = skinDatas.FirstOrDefault(x => x.id == userSkin.selectionSkinId)!.icon;

            var levelGenerator = gameFactory.CreateLevelGenerator();
            levelGenerator.Prepare();

            sceneController = Object.FindObjectOfType<SceneController>();

            sceneController.Container(
                hero,
                OnSceneLoaded,
                levelGenerator,
                saveService);
        }

        private void OnSceneLoaded()
        {
            curtain.ShowCurtain(true,
                () =>
                {
                    sceneLoader.LoadScene(SceneName.Menu, (() =>
                    {
                        Debug.Log($"stateMachine == null? - {gameStateMachine == null}");
                        if (gameStateMachine != null)
                            gameStateMachine.EnterState<LoadMainMenuState>();
                        else
                        {
                            Debug.Log(
                                $"Failure loaded LoadMainMenuState - stateMachine == null? - {gameStateMachine == null}");
                        }
                    }));
                });
        }
    }
}