// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System.Linq;
using Internal.Codebase.Infrastructure.Factory.Game;
using Internal.Codebase.Infrastructure.Factory.Hero;
using Internal.Codebase.Infrastructure.Factory.UI;
using Internal.Codebase.Infrastructure.GeneralGameStateMachine.Interfaces;
using Internal.Codebase.Infrastructure.GeneralGameStateMachine.StateMachine;
using Internal.Codebase.Infrastructure.Services.CloudSave;
using Internal.Codebase.Infrastructure.Services.Curtain;
using Internal.Codebase.Infrastructure.Services.SceneLoader;
using Internal.Codebase.Infrastructure.Services.StaticData;
using Internal.Codebase.Runtime.GameplayScene.Hero.View;
using Internal.Codebase.Runtime.GameplayScene.LevelController;
using Internal.Codebase.Runtime.GameplayScene.LevelGeneration.Handlers;
using Internal.Codebase.Runtime.GameplayScene.Timer;
using Internal.Codebase.Runtime.General.SpriteTextNumberCounterLogic;
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
        private readonly IUIFactory uiFactory;
        private GameStateMachine gameStateMachine;
        private SceneController sceneController;
        private EndlessLevelGenerationHandler levelGenerator;

        [Inject]
        public GameplaySceneState(
            ICurtainService curtain,
            ISceneLoaderService sceneLoader,
            IStaticDataService staticData,
            IHeroFactory heroFactory,
            IGameFactory gameFactory,
            IYandexSaveService saveService,
            IUIFactory uiFactory
        )
        {
            this.curtain = curtain;
            this.sceneLoader = sceneLoader;
            this.staticData = staticData;
            this.heroFactory = heroFactory;
            this.gameFactory = gameFactory;
            this.saveService = saveService;
            this.uiFactory = uiFactory;
        }

        public void Init(GameStateMachine stateMachine) =>
            gameStateMachine = stateMachine;

        public void Enter()
        {
            PrepareScene();
            HideCurtain();
            StartLevelTimer();
        }

        public void Exit()
        {
        }

        private void HideCurtain() =>
            curtain.HideCurtain();

        private void PrepareScene()
        {
            uiFactory
                .CreateGameplayUIRoot()
                .CreateGameplayCanvas();
            
            PrepareLevelGeneration();
            var hero = PrepareHero();

            sceneController = Object.FindObjectOfType<SceneController>();

            sceneController.Container(
                hero,
                OnSceneLoaded,
                levelGenerator,
                saveService);

            void PrepareLevelGeneration()
            {
                levelGenerator = gameFactory.CreateLevelGenerator();

                var startLevelTimer = uiFactory.GameplayUIRoot.GameplayCanvasView.StartLevelTimerView;
                startLevelTimer.Timer.OnTimerOff += ActivateEndlessLevelGeneration;
            }

            HeroViewController PrepareHero()
            {
                var heroViewController = heroFactory.CreateHero();
                heroFactory.CreateHeroCamera();

                var skinDatas = staticData.ForSkins().gameplaySkinDatas;
                var userSkin = saveService.Load().userSkins;

                heroViewController.HeroSpriteRenderer.sprite =
                    skinDatas.FirstOrDefault(x => x.id == userSkin.selectionSkinId)!.icon;

                return heroViewController;
            }
        }

        private void StartLevelTimer()
        {
            var startLevelTimer = uiFactory.GameplayUIRoot.GameplayCanvasView.StartLevelTimerView;
            startLevelTimer.Timer.StartCountdown();
        }

        private void ActivateEndlessLevelGeneration()
        {
            levelGenerator.StartEndlessLevelGeneration();

            var startLevelTimer = uiFactory.GameplayUIRoot.GameplayCanvasView.StartLevelTimerView;
            startLevelTimer.TimeToPlayVisualizer.StartAutoVisualizeText();
            startLevelTimer.Timer.OnTimerOff -= ActivateEndlessLevelGeneration;
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
                            Debug.Log(
                                $"Failure loaded LoadMainMenuState - stateMachine == null? - {gameStateMachine == null}");
                    }));
                });
        }
    }
}