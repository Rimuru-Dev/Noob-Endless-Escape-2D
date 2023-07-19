// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using UnityEngine;

namespace Internal.Codebase.Runtime.MainMenu.Configs
{
    [CreateAssetMenu(menuName = "Configs/Create " + nameof(MainMenuConfig), fileName = nameof(MainMenuConfig), order = 0)]
    public sealed class MainMenuConfig : ScriptableObject
    {
        [field: SerializeField] public Transform UIRoot { get; private set; }
        [field: SerializeField] public Transform DynamicCanvas { get; private set; }
        [field: SerializeField] public MainMenuCanvasView MainMenuCanvas { get; private set; }
    }
}