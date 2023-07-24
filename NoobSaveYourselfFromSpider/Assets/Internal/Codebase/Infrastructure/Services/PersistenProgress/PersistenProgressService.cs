// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using System.Collections.Generic;
using System.Linq;
using Internal.Codebase.Runtime.EndlessLevelGenerationSolution.Configs;
using Internal.Codebase.Runtime.StorageData;
using UnityEngine;
using YG;
using AudioSettings = Internal.Codebase.Runtime.StorageData.AudioSettings;

namespace Internal.Codebase.Infrastructure.Services.PersistenProgress
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
                Debug.Log("Error Loaded");

            YandexGame.GetDataEvent += Load;
        }

        public Runtime.StorageData.Storage GetStoragesData() => storage;

        public void Save(Runtime.StorageData.Storage newStorage)
        {
            if (YandexGame.SDKEnabled)
            {
                Debug.Log("Save Data SDKEnabled = true");
                if (newStorage != null)
                {
                    YandexGame.savesData.storage = newStorage;
                    YandexGame.SaveProgress();
                }
            }

            Debug.Log("Save Data SDKEnabled = false");
        }

        public void Load()
        {
            // Debug.Log("<color=yellow>Loaded !!!</color>");

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

                newStorage.audioSettings.volume = 0.1f;
                // Biome
                // Lol Kek :3 Default biome settings
                {
                    newStorage.userBioms.selectionBiomId = BiomeTypeID.GreenPlains;

                    newStorage.userBioms.biomeData = new List<BiomDatas>();

                    var biom1 = new BiomDatas
                    {
                        id = BiomeTypeID.GreenPlains,
                        isOpen = true
                    };
                    newStorage.userBioms.biomeData.Add(biom1);

                    var biom2 = new BiomDatas
                    {
                        id = BiomeTypeID.SnowyWastelands,
                        isOpen = false
                    };
                    newStorage.userBioms.biomeData.Add(biom2);
                }
                YandexGame.savesData.storage = newStorage;
                storage = newStorage;
                // YandexGame.SaveProgress();
            }
            else
            {
                Debug.Log("<color=yellow>Load Old Save</color>");
                storage = YandexGame.savesData.storage;
                // YandexGame.LoadProgress();
            }
        }

        public void Dispose()
        {
            YandexGame.GetDataEvent -= Load;
        }
    }
}