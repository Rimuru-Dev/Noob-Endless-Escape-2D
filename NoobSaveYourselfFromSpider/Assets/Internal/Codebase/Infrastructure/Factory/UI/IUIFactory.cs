// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Internal.Codebase.Runtime.General.Curtain;
using Internal.Codebase.Runtime.MainMenu.UI;

namespace Internal.Codebase.Infrastructure.Factory.UI
{
    public interface IUIFactory
    {
        public CurtainView CreateCurtain();
        public UIRoot MainMenuUIRoot { get; }
        public IUIFactory CreateMainMenuUIRoot();
        public IUIFactory CreateMainMenuBackgraund();
        public IUIFactory CreateMainMenu();
    }
}