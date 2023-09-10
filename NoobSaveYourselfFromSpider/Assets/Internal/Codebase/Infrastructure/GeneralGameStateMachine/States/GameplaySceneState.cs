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
using Internal.Codebase.Runtime.GameplayScene.Hero;
using Internal.Codebase.Runtime.GameplayScene.Hero.Death;
using Internal.Codebase.Runtime.GameplayScene.LevelGeneration.Handlers;
using Internal.Codebase.Runtime.GameplayScene.Parallax;
using Internal.Codebase.Utilities.Constants;
using UnityEngine;
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
            saveService.Load().Refresh();
        }

        public void Exit() =>
            heroController.HeroDeath.Unsubscribe(HeroDeath);

        private void HideCurtain() =>
            curtain.HideCurtain();

        private HeroController heroController;

        private void PrepareScene()
        {
            uiFactory
                .CreateGameplayUIRoot()
                .CreateGameplayCanvas();

            PrepareLevelGeneration();
            PrepareHero();


            {
                heroController = new HeroController(
                    heroFactory.Hero,
                    new HeroDeath()
                );
            }

            {
                heroController.HeroDeath.Subscribe(HeroDeath);
            }

            void PrepareLevelGeneration()
            {
                levelGenerator = gameFactory.CreateLevelGenerator();

                var startLevelTimer = uiFactory.GameplayUIRoot.GameplayCanvasView.StartLevelTimerView;
                startLevelTimer.Timer.OnTimerOn += LockHero;
                startLevelTimer.Timer.OnTimerOff += ActivateEndlessLevelGeneration;
            }

            void PrepareHero()
            {
                var heroViewController = heroFactory.CreateHero();
                heroFactory.CreateHeroCamera();

                var skinDatas = staticData.ForSkins().gameplaySkinDatas;
                var userSkin = saveService.Load().userSkins;

                heroViewController.HeroSpriteRenderer.sprite =
                    skinDatas.FirstOrDefault(x => x.id == userSkin.selectionSkinId)!.icon;
            }
        }

        private void LockHero()
        {
            heroFactory.Hero.JumpController.IsCanJump = false;
        }

        // TODO: Create observer
        private void HeroDeath()
        {
            Debug.Log("DEATH!");
            levelGenerator.StopEndlessLevelGeneration();

            Object
                .FindObjectOfType<ParallaxBackgroundScrollingList>(true)
                .SetPauseForAllParallaxBackgroundScrolling(pause: true);

            uiFactory.GameplayUIRoot.GameplayCanvasView.ScoreTimerView.ScoreVisualizer.IsPause = true;
        }

        private void StartLevelTimer()
        {
            var startLevelTimer = uiFactory.GameplayUIRoot.GameplayCanvasView.StartLevelTimerView;
            startLevelTimer.RootPanel.gameObject.SetActive(true);
            startLevelTimer.Timer.StartCountdown();

            var canvas = uiFactory.GameplayUIRoot.GameplayCanvasView;

            canvas.MenuPanelView.ResumePauseButton.gameObject.SetActive(false);
            canvas.MenuPanelView.MenuPanelRoot.gameObject.SetActive(false);

            canvas.MenuPanelView.PlayPauseButton.onClick.AddListener(() =>
            {
                canvas.MenuPanelView.PlayPauseButton.gameObject.SetActive(false);
                canvas.MenuPanelView.ResumePauseButton.gameObject.SetActive(true);
                canvas.MenuPanelView.MenuPanelRoot.gameObject.SetActive(true);
            });

            canvas.MenuPanelView.ResumePauseButton.onClick.AddListener(() =>
            {
                canvas.MenuPanelView.PlayPauseButton.gameObject.SetActive(true);
                canvas.MenuPanelView.ResumePauseButton.gameObject.SetActive(false);
                canvas.MenuPanelView.MenuPanelRoot.gameObject.SetActive(false);
            });

            canvas.MenuPanelView.GoMainMenuButton.onClick.AddListener(() =>
            {
                HeroDeath();
                levelGenerator.StartEndlessLevelGeneration();
                OnSceneLoaded();
            });
        }

        private void ActivateEndlessLevelGeneration()
        {
            levelGenerator.StartEndlessLevelGeneration();

            var startLevelTimer = uiFactory.GameplayUIRoot.GameplayCanvasView.StartLevelTimerView;
            uiFactory.GameplayUIRoot.GameplayCanvasView.ScoreTimerView.ScoreVisualizer.StartAutoVisualizeText();
            startLevelTimer.Timer.OnTimerOff -= ActivateEndlessLevelGeneration;
            heroFactory.Hero.JumpController.IsCanJump = true;
            startLevelTimer.RootPanel.gameObject.SetActive(false);
        }

        private void OnSceneLoaded() =>
            curtain.ShowCurtain(true, LoadMainMenu);

        private void LoadMainMenu() =>
            sceneLoader.LoadScene(SceneName.Menu, LoadLoadMainMenuState);

        private void LoadLoadMainMenuState() =>
            gameStateMachine.EnterState<LoadMainMenuState>();
    }
}