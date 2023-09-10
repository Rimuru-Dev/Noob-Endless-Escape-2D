using System;
using UnityEngine;
using UnityEngine.UI;

namespace Internal.Codebase.Runtime.GameplayScene.UI.View
{
    [DisallowMultipleComponent]
    public sealed class DeathPanelView : MonoBehaviour
    {
        [field: SerializeField] public Transform RootPanel { get; private set; }
        [field: SerializeField] public Button PlayMainMenuPanel { get; private set; }
        [field: SerializeField] public Button RebirthForAdvButton { get; private set; }
    }
}