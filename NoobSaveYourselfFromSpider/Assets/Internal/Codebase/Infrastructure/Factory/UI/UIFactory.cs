// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Zenject;
using Internal.Codebase.Runtime.General.Curtain;
using Internal.Codebase.Infrastructure.AssetManagement;
using Internal.Codebase.Infrastructure.Services.StaticData;
using UIRoot = Internal.Codebase.Runtime.MainMenu.View.UIRoot;

namespace Internal.Codebase.Infrastructure.Factory.UI
{
    public sealed class UIFactory : IUIFactory
    {
        private readonly IAssetProvider assetProvider;
        private readonly IStaticDataService staticData;

        private UIRoot mainMenuUIRoot;
        private Runtime.GameplayScene.UI.View.UIRoot gameplayUIRoot;

        [Inject]
        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticData)
        {
            this.assetProvider = assetProvider;
            this.staticData = staticData;
        }

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

        public Runtime.GameplayScene.UI.View.UIRoot GameplayUIRoot
        {
            get
            {
                if (gameplayUIRoot == null)
                    CreateGameplayUIRoot();

                return gameplayUIRoot;
            }
            private set
            {
                if (gameplayUIRoot == null && value != null)
                    gameplayUIRoot = value;
            }
        }

        public CurtainView CreateCurtain()
        {
            var config = staticData.ForCurtain();
            var instance = assetProvider.Instantiate(config.CurtainView.gameObject);
            var view = instance.GetComponent<CurtainView>();

            view.Constructor(config.AnimationDuration);

            return view;
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

        public IUIFactory CreateGameplayUIRoot()
        {
            var config = staticData.ForGameplaySceneUI();

            var root = assetProvider.Instantiate(config.UIRoot);

            GameplayUIRoot = root;

            return this;
        }

        public IUIFactory CreateGameplayCanvas()
        {
            var config = staticData.ForGameplaySceneUI();

            var gameplayCanvas = assetProvider.Instantiate(config.GameplayCanvas, GameplayUIRoot.transform);

            GameplayUIRoot.GameplayCanvasView = gameplayCanvas;

            return this;
        }
    }
}