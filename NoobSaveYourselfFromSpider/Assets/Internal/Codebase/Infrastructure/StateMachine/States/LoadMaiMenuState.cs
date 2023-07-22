// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Internal.Codebase.Infrastructure.Factory.UI;
using Internal.Codebase.Infrastructure.Services.Curtain;
using Internal.Codebase.Infrastructure.Services.PersistenProgress;
using Internal.Codebase.Infrastructure.Services.SceneLoader;
using Internal.Codebase.Infrastructure.Services.StaticData;
using Internal.Codebase.Infrastructure.StateMachine.Interfaces;
using Internal.Codebase.Runtime;
using Internal.Codebase.Runtime.BiomeShop;
using Internal.Codebase.Runtime.EndlessLevelGenerationSolution.Configs;
using Internal.Codebase.Runtime.MainMenu;
using Internal.Codebase.Runtime.MainMenu.Animation;
using Internal.Codebase.Utilities.Constants;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Internal.Codebase.Infrastructure.StateMachine.States
{
    public sealed class LoadMaiMenuState : IStateNext
    {
        private readonly ICurtainService curtain;
        private readonly ISceneLoaderService sceneLoader;
        private readonly IPersistenProgressService persistenProgressService;
        private readonly IStaticDataService staticData;
        private readonly IUIFactory uiFactory;
        private IGameStateMachine gameStateMachine;

        private MainMenuCanvasView mainMenu;
        private BiomeShopView biomeShop;

        [Inject]
        public LoadMaiMenuState(
            IStaticDataService staticData,
            ICurtainService curtain,
            IUIFactory uiFactory,
            ISceneLoaderService sceneLoader,
            IPersistenProgressService persistenProgressService)
        {
            this.sceneLoader = sceneLoader;
            this.persistenProgressService = persistenProgressService;
            this.staticData = staticData;
            this.curtain = curtain;
            this.uiFactory = uiFactory;
        }

        public void Init(IGameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            PrepareUI();

            // *** Hide Curtain *** //
            var config = staticData.ForCurtain();
            curtain.HideCurtain(config.HideDelay);
        }

        private void PrepareUI()
        {
            uiFactory.CreateMainMenuRoot();
            uiFactory.CreateDynamicCanvas();

            mainMenu = uiFactory.CreateMainMenuCanvas();
            biomeShop = mainMenu.BiomeShopView;

            var storage = persistenProgressService.GetStoragesData();

            mainMenu.Emerald.Initialize(storage);
            mainMenu.Fish.Initialize(storage);
            mainMenu.BestDistance.Initialize(storage);


            mainMenu.BuyCurrencyView.Constructor(storage, persistenProgressService);
            mainMenu.BuyCurrencyViewShortPanel.Constructor(storage, persistenProgressService);

            storage.Refresh();

            //   mainMenu.PlayButton.onClick.AddListener(OnSceneLoaded);

            // Biom ! Transition in Biomhandler.cs
            {
                // var bioms = persistenProgressService.GetStoragesData().userBioms;
                //
                // foreach (var biomsData in bioms.biomeData)
                // {
                //     if (biomsData.isOpen)
                //     {
                //     }
                //     else
                //     {
                //         // Вынеси в отдельный контроллер и канфиги кешируй в Boot ! А не уже в гей-мплей сцене
                //         biomeShop.BuyBiomWinter.gameObject.SetActive(false);
                //         biomeShop.LookIcon.gameObject.SetActive(true);
                //         // biomeShop.NumberVisualizer.gameObject.SetActive(false);
                //     }
                // }
            }
            mainMenu.gameObject
                .GetComponent<CharacterSwitcher>()
                .Initialize(storage, persistenProgressService);

            biomeShop.Constructor(staticData, persistenProgressService);
            biomeShop.Initialize(storage);

            biomeShop.PlayBiomeForest.onClick.AddListener(PlayForest);
            biomeShop.PlayBiomWinter.onClick.AddListener(PlayWinter);

            GameObject.FindObjectOfType<GeneralAudioHandler>(true)?.Constructor(storage, persistenProgressService);
        }

        private void PlayWinter()
        {
            persistenProgressService.GetStoragesData().userBioms.selectionBiomId = BiomeTypeID.SnowyWastelands;
            OnSceneLoaded();
            // mainMenu.
        }

        private void PlayForest()
        {
            persistenProgressService.GetStoragesData().userBioms.selectionBiomId = BiomeTypeID.GreenPlains;
            OnSceneLoaded();
        }


        public void Exit()
        {
            biomeShop.PlayBiomeForest.onClick.RemoveListener(PlayForest);
            biomeShop.PlayBiomWinter.onClick.RemoveListener(PlayWinter);
            mainMenu.PlayButton.onClick.RemoveListener(OnSceneLoaded);
            mainMenu = null;
            biomeShop = null;
        }

        private void OnSceneLoaded()
        {
            curtain.ShowCurtain(true, () =>
            {
                sceneLoader.LoadScene(SceneName.Gameplay,
                    () => { gameStateMachine.EnterState<GameplaySceneState>(); });
            });
        }
    }
}