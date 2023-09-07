﻿// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System.Linq;
using Internal.Codebase.Infrastructure.Factory.Game;
using Internal.Codebase.Infrastructure.Factory.Hero;
using Internal.Codebase.Infrastructure.Factory.UI;
using Internal.Codebase.Infrastructure.Services.Curtain;
using Internal.Codebase.Infrastructure.Services.SceneLoader;
using Internal.Codebase.Infrastructure.Services.StaticData;
using Internal.Codebase.Infrastructure.StateMachine.Interfaces;
using Internal.Codebase.Runtime.GameplayScene;
using Internal.Codebase.Utilities.Constants;
using UnityEngine;
using YG;
using Zenject;

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
        private GameStateMachine gameStateMachine;
        private SceneController sceneController;

        [Inject]
        public GameplaySceneState(
            ICurtainService curtain,
            ISceneLoaderService sceneLoader,
            IStaticDataService staticData,
            IUIFactory uiFactory,
            IHeroFactory heroFactory,
            IGameFactory gameFactory
        )
        {
            this.curtain = curtain;
            this.sceneLoader = sceneLoader;
            this.staticData = staticData;
            this.uiFactory = uiFactory;
            this.heroFactory = heroFactory;
            this.gameFactory = gameFactory;
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

            sceneController = GameObject.FindObjectOfType<SceneController>();
            sceneController.Container(hero, gameStateMachine, sceneLoader, OnSceneLoaded, levelGenerator,
                YandexGame.savesData.storage);
        }

        private void LeaveToMainMenuState() =>
            curtain.ShowCurtain(true, LoadScene);

        private void LoadScene() =>
            sceneLoader.LoadScene(SceneName.Menu, EnterMainMenuState);

        private void EnterMainMenuState() => gameStateMachine.EnterState<LoadMainMenuState>();

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