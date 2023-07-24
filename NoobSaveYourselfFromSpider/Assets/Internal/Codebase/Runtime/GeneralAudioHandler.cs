// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using Internal.Codebase.Infrastructure.Services.PersistenProgress;
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
    public sealed class GeneralAudioHandler : MonoBehaviour, IFuckingSaveLoad
    {
        private const int Off = 0, On = 1;

        // [Header("Settings")] [SerializeField]
        // private GeneralAudioSettings generalAudioSettings; // TODO: Load in Awake from JSON

        [Header("Sliders")] [SerializeField] private Slider musicSlider;
        // [SerializeField] private Slider audioSlider;

        // private AudioSource[] audioSources;
        private Storage storage;

        private float musicValue;
        private float audioValue;
        private IPersistenProgressService persistenProgressService;

        private void Awake()
        {
            if (YandexGame.SDKEnabled)
                Load();

            YandexGame.GetDataEvent += Load;
        }

        public void Save()
        {
            if (YandexGame.savesData.storage.audioSettings == null)
            {
                Debug.Log("NUll audio");
                Load();
            }

            if (storage == null || storage.audioSettings == null)
            {
                Load();
                Debug.Log("NUll torage.audioSettings");
            }

            if (storage != null)
                YandexGame.savesData.storage.audioSettings = storage.audioSettings;
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

        public void Constructor( /*Storage storage,*/ IPersistenProgressService persistenProgressService)
        {
            // this.storage = storage;
            // this.persistenProgressService = persistenProgressService;
            //
            // musicSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
            // // audioSlider.onValueChanged.AddListener(OnAudioSliderValueChanged);
            //
            // musicSlider.value = this.storage.audioSettings.volume;
            // // audioSlider.value = audioValue;
        }


        private void OnDestroy()
        {
            YandexGame.GetDataEvent -= Load;
            musicSlider.onValueChanged.RemoveListener(OnMusicSliderValueChanged);
            // audioSlider.onValueChanged.RemoveListener(OnAudioSliderValueChanged);
        }

        // private void OnApplicationPause(bool paused) =>
        //     AudioListener.volume = paused ? Off : On;
        //
        // private void OnApplicationFocus(bool focused) =>
        //     AudioListener.volume = focused ? On : Off;

        private void OnMusicSliderValueChanged(float value)
        {
            var valueChanged = Mathf.Clamp(value, 0, 1);
            AudioListener.volume = valueChanged;

            storage.audioSettings.volume = valueChanged;
        }

        private void OnDisable()
        {
            Save();
            // persistenProgressService.Save(storage);
        }

        // private void OnAudioSliderValueChanged(float value)
        // {
        //     foreach (var audioSource in audioSources)
        //         audioSource.volume = Mathf.Clamp(value, 0, 1);
        // }
    }
}