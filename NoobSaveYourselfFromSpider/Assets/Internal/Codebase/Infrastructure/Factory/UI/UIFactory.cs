// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using Internal.Codebase.Infrastructure.AssetManagement;
using Internal.Codebase.Infrastructure.Services.StaticData;
using Internal.Codebase.Infrastructure.Services.Storage;
using Internal.Codebase.Runtime.Curtain;
using Internal.Codebase.Runtime.MainMenu;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Infrastructure.Factory.UI
{
    public sealed class UIFactory : IUIFactory
    {
        private readonly IAssetProvider assetProvider;
        private readonly IStaticDataService staticData;
        private readonly IStorageService storageService;

        private Transform mainMenuRoot;

        [Inject]
        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticData,
            IStorageService storageService)
        {
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
    }
}