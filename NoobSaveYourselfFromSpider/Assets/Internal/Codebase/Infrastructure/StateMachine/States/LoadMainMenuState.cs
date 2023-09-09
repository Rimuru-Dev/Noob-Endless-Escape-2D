// **************************************************************** //
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
using Internal.Codebase.Runtime.BiomeShop;
using Internal.Codebase.Runtime.EndlessLevelGenerationSolution.Configs;
using Internal.Codebase.Runtime.MainMenu.HeroSwither.Controller;
using Internal.Codebase.Utilities.Constants;
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
        private BiomeShopView biomeShop;
        private CharacterSwitcher characterSwitcher;

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
            if (uiFactory.MainMenuUIRoot.MenuCanvasView != null)
                uiFactory.MainMenuUIRoot.MenuCanvasView.Dispose();

            characterSwitcher?.Dispose();
        }

        private void PrepareUI()
        {
            uiFactory
                .CreateMainMenuUIRoot()
                .CreateMainMenuBackgraund()
                .CreateMainMenu();

            var uiRoot = uiFactory.MainMenuUIRoot;

            // Setup Biome Shop View
            {
                biomeShop = uiRoot.MenuCanvasView.BiomeShopView;
                biomeShop.Constructor(saveService);
                biomeShop.Prepare();

                biomeShop.PlayBiomeForest.onClick.AddListener(PlayForest);
                biomeShop.PlayBiomWinter.onClick.AddListener(PlayWinter);

                biomeShop.OpenPanel.onClick.AddListener(() => { biomeShop.RootPanel.SetActive(true); });
                biomeShop.CloseWindow.onClick.AddListener(() => { biomeShop.RootPanel.SetActive(false); });
            }

            // Setup Settings Panel
            {
                var settingsView = uiRoot.MenuCanvasView.SettingsView;

                settingsView.GeneralAudioHandler.Constructor(saveService);
                settingsView.GeneralAudioHandler.Prepare();
                settingsView.OpenPanel.onClick.AddListener(() => settingsView.Panel.SetActive(true));
                settingsView.ClosePanel.onClick.AddListener(() => settingsView.Panel.SetActive(false));
            }

            // Adv currency panels
            {
                var currencyShopView = uiRoot.MenuCanvasView.CurrencyShopView;
                var length = currencyShopView.OpenChop.Length;

                for (var i = 0; i < length; i++)
                    currencyShopView.OpenChop[i].onClick.AddListener(() => currencyShopView.Panel.SetActive(true));

                currencyShopView.CloseChop.onClick.AddListener(() => { currencyShopView.Panel.SetActive(false); });
            }

            // Currency
            {
                uiRoot.MenuCanvasView.CurrancFishView.Constructor(saveService);
                uiRoot.MenuCanvasView.CurrancFishView.Prepare();

                uiRoot.MenuCanvasView.CurrancEmeraldView.Constructor(saveService);
                uiRoot.MenuCanvasView.CurrancEmeraldView.Prepare();
            }

            // BestDistanceView
            {
                uiRoot.MenuCanvasView.BestDistanceView.Constuctor(saveService);
                uiRoot.MenuCanvasView.BestDistanceView.Prepare();
            }

            // Buy Currency by AD
            {
                uiRoot.MenuCanvasView.BuyCurrencyShort.Constructor(saveService);
                uiRoot.MenuCanvasView.BuyCurrencyShort.Prepare();

                uiRoot.MenuCanvasView.BuyCurrencyPanel.Constructor(saveService);
                uiRoot.MenuCanvasView.BuyCurrencyPanel.Prepare();
            }

            // Setup CharacterSwitcherView
            {
                characterSwitcher = new CharacterSwitcher(uiRoot.MenuCanvasView.CharacterSwitcherView, saveService);
                characterSwitcher.Prepare();
            }
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