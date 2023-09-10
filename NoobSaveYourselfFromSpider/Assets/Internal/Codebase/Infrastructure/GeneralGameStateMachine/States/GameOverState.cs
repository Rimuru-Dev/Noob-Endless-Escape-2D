// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Internal.Codebase.Infrastructure.Factory.Game;
using Internal.Codebase.Infrastructure.Factory.Hero;
using Internal.Codebase.Infrastructure.Factory.UI;
using Internal.Codebase.Infrastructure.GeneralGameStateMachine.Interfaces;
using Internal.Codebase.Infrastructure.GeneralGameStateMachine.StateMachine;
using Internal.Codebase.Infrastructure.Services.CloudSave;
using Internal.Codebase.Infrastructure.Services.Curtain;
using Internal.Codebase.Runtime.GameplayScene.Parallax;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Infrastructure.GeneralGameStateMachine.States
{
    public sealed class GameOverState : IStateNext
    {
        private readonly ICurtainService curtain;
        private readonly IUIFactory uiFactory;
        private readonly IGameFactory gameFactory;
        private readonly IHeroFactory heroFactory;
        private readonly IYandexSaveService saveService;
        private GameStateMachine gameStateMachine;

        [Inject]
        public GameOverState(
            ICurtainService curtain,
            IUIFactory uiFactory,
            IHeroFactory heroFactory,
            IGameFactory gameFactory,
            IYandexSaveService saveService)
        {
            this.curtain = curtain;
            this.uiFactory = uiFactory;
            this.heroFactory = heroFactory;
            this.saveService = saveService;
            this.gameFactory = gameFactory;
        }

        public void Init(GameStateMachine stateMachine) =>
            gameStateMachine = stateMachine;

        public void Enter()
        {
            // 1. Stop level generation
            // 2. Stop Parallax
            // 3. Lock Input
            // 4. Show Death Popup

            gameFactory.GetCahaeLevelGeneration.StopEndlessLevelGeneration();

            // TODO: Get ParallaxBackgroundScrollingList in uiFactory
            Object
                .FindObjectOfType<ParallaxBackgroundScrollingList>(true)
                .SetPauseForAllParallaxBackgroundScrolling(pause: true);

            uiFactory.GameplayUIRoot.GameplayCanvasView.ScoreTimerView.ScoreVisualizer.IsPause = true;
        }

        public void Exit()
        {
            var menuPanel = uiFactory.GameplayUIRoot.GameplayCanvasView.MenuPanelView;
            menuPanel.PlayPauseButton.onClick.RemoveAllListeners();
            menuPanel.ResumePauseButton.onClick.RemoveAllListeners();
            menuPanel.GoMainMenuButton.onClick.RemoveAllListeners();

            heroFactory.GetHeroController.HeroDeath.UnsubscribeAll();
        }
    }
}