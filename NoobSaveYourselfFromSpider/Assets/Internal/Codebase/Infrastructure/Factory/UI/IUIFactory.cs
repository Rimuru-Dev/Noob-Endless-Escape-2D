// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Internal.Codebase.Runtime.General.Curtain;
using Internal.Codebase.Runtime.MainMenu.View;

namespace Internal.Codebase.Infrastructure.Factory.UI
{
    public interface IUIFactory
    {
        public UIRoot MainMenuUIRoot { get; }
        public CurtainView CreateCurtain();
        public IUIFactory CreateMainMenuUIRoot();
        public IUIFactory CreateMainMenuBackgraund();
        public IUIFactory CreateMainMenu();

        public Runtime.GameplayScene.UI.View.UIRoot GameplayUIRoot { get; }
        public IUIFactory CreateGameplayUIRoot();
        public IUIFactory CreateGameplayCanvas();
    }
}