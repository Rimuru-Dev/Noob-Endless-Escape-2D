// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using Internal.Codebase.Infrastructure.Services.CloudSave;
using Internal.Codebase.Runtime.MainMenu.Animation;
using Internal.Codebase.Runtime.StorageData;
using UnityEngine;
using UnityEngine.UI;
using YG;
using AudioSettings = Internal.Codebase.Runtime.StorageData.AudioSettings;

namespace Internal.Codebase.Runtime
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public sealed class GeneralAudioHandler : MonoBehaviour
    {
        private const int Off = 0;

        [SerializeField] private Slider musicSlider;

        private Storage storage;
        private float musicValue;
        private float audioValue;
        private IYandexSaveService yandexSaveService;

        public void Constructor(IYandexSaveService saveService)
        {
            yandexSaveService = saveService;
            storage = yandexSaveService.Load();
        }

        public void Save()
        {
            YandexGame.savesData.storage.audioSettings = storage.audioSettings;
            yandexSaveService.Save();
        }

        public void Load()
        {
            if (YandexGame.savesData.storage.audioSettings == null)
            {
                YandexGame.savesData.storage.audioSettings = new AudioSettings();
                YandexGame.savesData.storage.audioSettings.volume = 0.15f;
                storage = YandexGame.savesData.storage;
            }
            else
                storage = YandexGame.savesData.storage;

            musicSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
            musicSlider.value = storage.audioSettings.volume;
        }


        private void OnDestroy()
        {
            YandexGame.GetDataEvent -= Load;
            musicSlider.onValueChanged.RemoveListener(OnMusicSliderValueChanged);
            // audioSlider.onValueChanged.RemoveListener(OnAudioSliderValueChanged);
        }


        private void OnMusicSliderValueChanged(float value)
        {
            var valueChanged = Mathf.Clamp(value, 0, 1);
            AudioListener.volume = valueChanged;

            storage.audioSettings.volume = valueChanged;
        }

        private void OnDisable()
        {
            Save();
        }
    }
}