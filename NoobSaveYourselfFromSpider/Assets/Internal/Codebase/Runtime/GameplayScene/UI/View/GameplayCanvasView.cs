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

namespace Internal.Codebase.Runtime.GameplayScene.UI.View
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public sealed class GameplayCanvasView : MonoBehaviour
    {
        [field: SerializeField] public MenuPanelView MenuPanelView { get; private set; }
        [field: SerializeField] public AdvertismentTimerView AdvTimerView { get; private set; }
        [field: SerializeField] public StartLevelTimerView StartLevelTimerView { get; private set; }
        [field: SerializeField] public DeathPanelView DeathPanelView { get; private set; }
        [field: SerializeField] public ScoreTimerView ScoreTimerView { get; private set; }
    }
}