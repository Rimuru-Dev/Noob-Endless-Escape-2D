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

using Internal.Codebase.Runtime.MainMenu.Background;
using UnityEngine;

namespace Internal.Codebase.Runtime.MainMenu.View
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public sealed class BackgroundCanvasView : MonoBehaviour
    {
        [field: SerializeField] public Canvas Canvas { get; private set; }
        [field: SerializeField] public Camera CameraRenderer { get; private set; }
        [field: SerializeField] public BackgroundScrolling BackgroundScrolling { get; private set; }
    }
}