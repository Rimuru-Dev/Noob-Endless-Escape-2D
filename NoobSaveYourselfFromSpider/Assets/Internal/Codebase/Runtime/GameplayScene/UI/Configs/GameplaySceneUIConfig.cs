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

using Internal.Codebase.Runtime.GameplayScene.UI.View;
using UnityEngine;

namespace Internal.Codebase.Runtime.GameplayScene.UI.Configs
{
    [CreateAssetMenu(fileName = nameof(GameplaySceneUIConfig), menuName = "Static Data/Gameplay Scene/UI Config", order = 0)]
    public sealed class GameplaySceneUIConfig : ScriptableObject
    {
        [field: SerializeField] public UIRoot UIRoot { get; private set; }
        [field: SerializeField] public GameplayCanvasView GameplayCanvas { get; private set; }
    }
}