// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using UnityEngine;
using UnityEngine.UI;

namespace Internal.Codebase.Runtime.MainMenu
{
    public sealed class MainMenuCanvasView : MonoBehaviour
    {
        [field: SerializeField] public CurrancyUIView Emerald { get; private set; }
        [field: SerializeField] public CurrancyUIView Fish { get; private set; }
        [field: SerializeField] public SettingsUIView Settings { get; private set; }
        [field: SerializeField] public BestDistanceUIView BestDistance { get; private set; }
        
        [field: SerializeField] public Button PlayButton { get; private set; }
    }
}