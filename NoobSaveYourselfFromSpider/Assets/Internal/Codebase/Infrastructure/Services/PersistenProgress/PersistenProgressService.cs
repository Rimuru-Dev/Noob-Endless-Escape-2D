// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using Internal.Codebase.Runtime.StorageData;
using UnityEngine;
using YG;
using AudioSettings = Internal.Codebase.Runtime.StorageData.AudioSettings;

namespace Internal.Codebase.Infrastructure.Services.StaticData
{
    public sealed class PersistenProgressService : IPersistenProgressService, IDisposable
    {
        private Runtime.StorageData.Storage storage;

        public void Init()
        {
            storage = new Runtime.StorageData.Storage();

            if (YandexGame.SDKEnabled)
                Load();
            else
                Debug.Log("Eroor Loaded");

            YandexGame.GetDataEvent += Load;
        }

        public Runtime.StorageData.Storage GetStoragesData() => storage;

        public void Save(Runtime.StorageData.Storage newStorage)
        {
            if (newStorage != null)
            {
                YandexGame.savesData.storage = newStorage;
                YandexGame.SaveProgress();
            }
        }

        public void Load()
        {
            Debug.Log("<color=yellow>Loaded !!!</color>");

            if (YandexGame.savesData.storage == null)
            {
                Debug.Log("<color=yellow>Load Default</color>");
                var newStorage = new Runtime.StorageData.Storage
                {
                    fishCurrancy = new FishCurrancy(),
                    emeraldCurrancy = new EmeraldCurrancy(),
                    audioSettings = new AudioSettings(),
                    userBioms = new UserBioms(),
                    userSkins = new UserSkins(),
                    userBestDistance = new UserBestDistance()
                };
                YandexGame.savesData.storage = newStorage;
                storage = newStorage;
                YandexGame.SaveProgress();
            }
            else
            {
                Debug.Log("<color=yellow>Load Old Save</color>");
                storage = YandexGame.savesData.storage;
            }
        }

        public void Dispose()
        {
            YandexGame.GetDataEvent -= Load;
        }
    }
}