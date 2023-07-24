// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using Internal.Codebase.Runtime.MainMenu.Animation;
using Internal.Codebase.Runtime.SpriteTextNumberCounterLogic;
using Internal.Codebase.Runtime.StorageData;
using UnityEngine;
using YG;

namespace Internal.Codebase.Runtime.MainMenu
{
    public sealed class BestDistanceUIView : MonoBehaviour, IFuckingSaveLoad
    {
        [field: SerializeField] public NumberVisualizer NumberVisualizer { get; private set; }

        private Storage storage;

        private void Awake()
        {
            if (YandexGame.SDKEnabled)
                Load();

            YandexGame.GetDataEvent += Load;
        }

        public void Save()
        {
            YandexGame.savesData.storage.BestDistance = storage.BestDistance;
        }

        public void Load()
        {
            storage = YandexGame.savesData.storage;
            storage.OnBestDistanceChanged += NumberVisualizer.ShowNumber;
            storage.Refresh();
        }

        public void Initialize(Storage storage)
        {
            //     this.storage = storage;

            //   storage.OnBestDistanceChanged += NumberVisualizer.ShowNumber;
        }

        private void OnDestroy()
        {
            YandexGame.GetDataEvent -= Load;
            storage.OnBestDistanceChanged -= NumberVisualizer.ShowNumber;
        }
    }
}