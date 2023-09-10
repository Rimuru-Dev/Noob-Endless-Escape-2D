// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: 
//          - Gmail:    rimuru.dev@gmail.com
//          - GitHub:   https://github.com/RimuruDev
//          - LinkedIn: https://www.linkedin.com/in/rimuru/
//          - GitHub Organizations: https://github.com/Rimuru-Dev
//
// **************************************************************** //

using UnityEngine;

namespace Internal.Codebase.Runtime.MainMenu.View
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public sealed class UIRoot : MonoBehaviour
    {
        [field: SerializeField] public MenuCanvasView MenuCanvasView { get; set; }
        [field: SerializeField] public BackgroundCanvasView BackgroundCanvasView { get; set; }
    }
}