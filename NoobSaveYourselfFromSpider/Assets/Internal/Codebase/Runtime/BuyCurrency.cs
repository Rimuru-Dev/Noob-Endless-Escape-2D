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

namespace Internal.Codebase.Runtime
{
    public sealed class BuyCurrency : MonoBehaviour, IFuckingSaveLoad
    {
        public Button buyFish;
        public Button buyEmerald;
        public YandexGame yandexGameSDK;

        public int rewardFish = 10;
        public int rewardEmerald = 20;

        private Storage storage;
        // private IPersistenProgressService persistenProgressService;

        private void Awake()
        {
            if (YandexGame.SDKEnabled)
                Load();

            YandexGame.GetDataEvent += Load;
        }

        private void Start()
        {
            buyFish.onClick.AddListener(BuyFish);
            buyEmerald.onClick.AddListener(BuyEmerald);
            yandexGameSDK = FindObjectOfType<YandexGame>(true);
        }

        public void Save()
        {
            YandexGame.savesData.storage.fishCurrancy = storage.fishCurrancy;
            YandexGame.savesData.storage.emeraldCurrancy = storage.emeraldCurrancy;
        }

        public void Load()
        {
            storage = YandexGame.savesData.storage;
        }

        public void Constructor(Storage storage, IPersistenProgressService persistenProgressService)
        {
            // this.storage = storage;
            // this.persistenProgressService = persistenProgressService;
        }

        private void OnDestroy()
        {
            // Save();
            YandexGame.GetDataEvent -= Load;
            buyFish.onClick.RemoveListener(BuyFish);
            buyEmerald.onClick.RemoveListener(BuyEmerald);
        }


        private void OnEnable() =>
            YandexGame.RewardVideoEvent += Rewarded;

        private void OnDisable() =>
            YandexGame.RewardVideoEvent -= Rewarded;

        private void Rewarded(int id)
        {
            if (id == rewardFish)
            {
                AudioListener.volume = storage.audioSettings.volume;
                storage.FishCurrancy = 1;
                // persistenProgressService.Save(storage);
            }

            if (id == rewardEmerald)
            {
                AudioListener.volume = storage.audioSettings.volume;
                storage.EmeraldCurrancy = 9;
                // persistenProgressService.Save(storage);
            }

            Save();
            // persistenProgressService.Save(storage);
        }

        private void ShowAdvButton(int id)
        {
            AudioListener.volume = 0;
            yandexGameSDK._RewardedShow(id);
        }

        private void BuyEmerald()
        {
            ShowAdvButton(rewardEmerald);
        }

        private void BuyFish()
        {
            ShowAdvButton(rewardFish);
        }
    }
}