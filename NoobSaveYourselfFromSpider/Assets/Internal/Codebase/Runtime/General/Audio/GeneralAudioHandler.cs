﻿// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Internal.Codebase.Infrastructure.Services.CloudSave;
using Internal.Codebase.Runtime.General.StorageData;
using UnityEngine;
using UnityEngine.UI;

namespace Internal.Codebase.Runtime.General.Audio
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public sealed class GeneralAudioHandler : MonoBehaviour
    {
        private const int Min = 0, Max = 1;

        [SerializeField] private Slider musicSlider;

        private Storage storage;
        private IYandexSaveService yandexSaveService;

        public void Constructor(IYandexSaveService saveService) =>
            yandexSaveService = saveService;

        private void OnDisable() =>
            Save();

        private void OnDestroy() =>
            musicSlider.onValueChanged.RemoveListener(OnMusicSliderValueChanged);

        public void Prepare()
        {
            storage = yandexSaveService.Load();
            musicSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
            musicSlider.value = storage.audioSettings.volume;
        }

        private void Save() =>
            yandexSaveService.Save(storage);

        private void OnMusicSliderValueChanged(float value)
        {
            var valueChanged = Mathf.Clamp(value, Min, Max);

            AudioListener.volume = valueChanged;

            storage.audioSettings.volume = valueChanged;
        }
    }
}