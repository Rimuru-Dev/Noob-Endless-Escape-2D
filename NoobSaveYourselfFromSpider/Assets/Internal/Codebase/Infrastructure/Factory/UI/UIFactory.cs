// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using Internal.Codebase.Infrastructure.AssetManagement;
using Internal.Codebase.Infrastructure.Services;
using Internal.Codebase.Infrastructure.Services.ActionUpdater;
using Internal.Codebase.Infrastructure.Services.StaticData;
using Internal.Codebase.Infrastructure.Services.Storage;
using Internal.Codebase.Runtime.Curtain;
using Internal.Codebase.Runtime.MainMenu;
using Internal.Codebase.Runtime.MainMenu.New.Configs;
using Internal.Codebase.Runtime.MainMenu.New.UI;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Infrastructure.Factory.UI
{
    public sealed class UIFactory : IUIFactory
    {
        private readonly IActionUpdaterService actionUpdaterService;
        private readonly IAssetProvider assetProvider;
        private readonly IStaticDataService staticData;
        private readonly IStorageService storageService;

        private Transform mainMenuRoot;

        [Inject]
        public UIFactory(
            IActionUpdaterService actionUpdaterService,
            IAssetProvider assetProvider,
            IStaticDataService staticData,
            IStorageService storageService)
        {
            this.actionUpdaterService = actionUpdaterService;
            this.assetProvider = assetProvider;
            this.staticData = staticData;
            this.storageService = storageService;
        }

        public Transform CreateMainMenuRoot()
        {
            var config = staticData.ForMainMenu();

            mainMenuRoot = assetProvider.Instantiate(config.UIRoot.gameObject).transform;

            return mainMenuRoot;
        }

        public CurtainView CreateCurtain()
        {
            var config = staticData.ForCurtain();
            var instance = assetProvider.Instantiate(config.CurtainView.gameObject);
            var view = instance.GetComponent<CurtainView>();

            view.Constructor(config.AnimationDuration);

            return view;
        }

        public GameObject CreateDynamicCanvas()
        {
            var config = staticData.ForMainMenu();

            var instance = assetProvider.Instantiate(config.DynamicCanvas.gameObject, mainMenuRoot);

            return instance;
        }

        public MainMenuCanvasView CreateMainMenuCanvas()
        {
            var config = staticData.ForMainMenu();

            var view = assetProvider
                .Instantiate(config.MainMenuCanvas.gameObject, mainMenuRoot)
                .GetComponent<MainMenuCanvasView>();

            return view;
        }

        private UIRoot mainMenuUIRoot;
        public UIRoot MainMenuUIRoot
        {
            get
            {
                if (mainMenuUIRoot == null)
                    CreateMainMenuUIRoot();

                return mainMenuUIRoot;
            }
            private set
            {
                if (mainMenuUIRoot == null && value != null)
                    mainMenuUIRoot = value;
            }
        }

        public IUIFactory CreateMainMenuUIRoot()
        {
            var config = staticData.ForMainMenuUI();

            var root = assetProvider.Instantiate(config.UIRoot);

            MainMenuUIRoot = root;

            return this;
        }

        public IUIFactory CreateMainMenuBackgraund()
        {
            var config = staticData.ForMainMenuUI();

            var canvasView = assetProvider.Instantiate(config.BackgroundCanvasView, MainMenuUIRoot.transform);

            MainMenuUIRoot.BackgroundCanvasView = canvasView;

            return this;
        }

        public IUIFactory CreateMainMenu()
        {
            var config = staticData.ForMainMenuUI();

            var canvasView = assetProvider.Instantiate(config.MenuCanvasView, MainMenuUIRoot.transform);

            MainMenuUIRoot.MenuCanvasView = canvasView;

            return this;
        }
    }
}