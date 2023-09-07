// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Internal.Codebase.Runtime.Curtain;
using Internal.Codebase.Runtime.MainMenu;
using Internal.Codebase.Runtime.MainMenu.New.UI;
using UnityEngine;

namespace Internal.Codebase.Infrastructure.Factory.UI
{
    public interface IUIFactory
    {
        public Transform CreateMainMenuRoot();
        public CurtainView CreateCurtain();
        
        public GameObject CreateDynamicCanvas();
        public MainMenuCanvasView CreateMainMenuCanvas();

        public UIRoot MainMenuUIRoot { get; }
        public IUIFactory CreateMainMenuUIRoot();
        public IUIFactory CreateMainMenuBackgraund();
        public IUIFactory CreateMainMenu();
    }
}