// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Project: "Murders Drones Endless Way" 
//   Contact me: 
//          - Gmail:    rimuru.dev@gmail.com
//          - GitHub:   https://github.com/RimuruDev
//          - LinkedIn: https://www.linkedin.com/in/rimuru/
//          - GitHub Organizations: https://github.com/Rimuru-Dev
//
// **************************************************************** //

using Internal.Codebase.Runtime.MainMenu.New.UI;
using Internal.Codebase.Runtime.MainMenu.New.View;
using UnityEngine;

namespace Internal.Codebase.Runtime.MainMenu.New.Configs
{
    [CreateAssetMenu(fileName = nameof(MainMenuUIConfig), menuName = "Static Data/Main Menu Scene/UI Config",
        order = 0)]
    public sealed class MainMenuUIConfig : ScriptableObject
    {
        [field: SerializeField] public UIRoot UIRoot { get; private set; }
        [field: SerializeField] public BackgroundCanvasView BackgroundCanvasView { get; private set; }
        [field: SerializeField] public MenuCanvasView MenuCanvasView { get; private set; }
    }
}