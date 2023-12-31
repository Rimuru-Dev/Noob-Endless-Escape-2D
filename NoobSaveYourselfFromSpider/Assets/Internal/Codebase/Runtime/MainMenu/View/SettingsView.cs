﻿using Internal.Codebase.Runtime.General.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace Internal.Codebase.Runtime.MainMenu.View
{
    public sealed class SettingsView : MonoBehaviour
    {
        [field: SerializeField] public GameObject Panel { get; private set; }
        [field: SerializeField] public Button OpenPanel { get; private set; }
        [field: SerializeField] public Button ClosePanel { get; private set; }
        [field: SerializeField] public GeneralAudioHandler GeneralAudioHandler { get; private set; }
    }
}