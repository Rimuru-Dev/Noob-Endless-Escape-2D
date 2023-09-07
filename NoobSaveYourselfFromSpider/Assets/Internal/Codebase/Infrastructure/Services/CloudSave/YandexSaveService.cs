// ReSharper disable CommentTypo
// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: 
//          - Gmail:    rimuru.dev@gmail.com
//          - GitHub:   https://github.com/RimuruDev
//          - LinkedIn: https://www.linkedin.com/in/rimuru/
//          - GitHub Organizations: https://github.com/Rimuru-Dev
//
// **************************************************************** //

using YG;
using System;
using System.Collections.Generic;
using Internal.Codebase.Runtime.EndlessLevelGenerationSolution.Configs;
using Internal.Codebase.Runtime.StorageData;

namespace Internal.Codebase.Infrastructure.Services.CloudSave
{
    public sealed class YandexSaveService : IYandexSaveService
    {
        private readonly YandexGame yandexGameSDK;
        private Runtime.StorageData.Storage storage;

        public YandexSaveService(YandexGame yandexGameSDK) =>
            this.yandexGameSDK = yandexGameSDK;

        public void Init() =>
            YandexGame.GetDataEvent += InitializeLoad;

        public void Save(Runtime.StorageData.Storage savedStorage)
        {
            if (savedStorage == null)
                throw new ArgumentNullException(nameof(savedStorage));

            YandexGame.savesData.storage = savedStorage;
            YandexGame.SaveProgress();
        }

        public Runtime.StorageData.Storage Load() =>
            YandexGame.savesData.storage == null
                ? LoadDefaultStorage()
                : LoadUserStorage();

        public void Dispose() =>
            YandexGame.GetDataEvent -= InitializeLoad;

        private void InitializeLoad() =>
            Load();

        private Runtime.StorageData.Storage LoadDefaultStorage()
        {
            var newStorage = new Runtime.StorageData.Storage
            {
                fishCurrancy = new FishCurrancy(),
                emeraldCurrancy = new EmeraldCurrancy(),
                userBioms = new UserBioms(),
                userBestDistance = new UserBestDistance()
            };

            // Biome
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

            YandexGame.savesData ??= new SavesYG();
            YandexGame.savesData.storage = newStorage;

            storage = YandexGame.savesData.storage;

            return storage;
        }

        private Runtime.StorageData.Storage LoadUserStorage()
        {
            storage = YandexGame.savesData.storage;

            return storage;
        }
    }
}