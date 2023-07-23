// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using Internal.Codebase.Infrastructure.Services.PersistenProgress;
using Internal.Codebase.Runtime.StorageData;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Internal.Codebase.Runtime
{
    public sealed class BuyCurrency : MonoBehaviour
    {
        public Button buyFish;
        public Button buyEmerald;
        public YandexGame yandexGameSDK;

        public int rewardFish = 10;
        public int rewardEmerald = 20;
        private Storage storage;
        private IPersistenProgressService persistenProgressService;

        private void Start()
        {
            buyFish.onClick.AddListener(BuyFish);
            buyEmerald.onClick.AddListener(BuyEmerald);
            yandexGameSDK = FindObjectOfType<YandexGame>(true);
        }

        public void Constructor(Storage storage, IPersistenProgressService persistenProgressService)
        {
            this.storage = storage;
            this.persistenProgressService = persistenProgressService;
        }

        private void OnDestroy()
        {
            buyFish.onClick.RemoveListener(BuyFish);
            buyEmerald.onClick.RemoveListener(BuyEmerald);
        }


        private void OnEnable() =>
            YandexGame.RewardVideoEvent += Rewarded;

        private void OnDisable() =>
            YandexGame.RewardVideoEvent -= Rewarded;

        private void Rewarded(int id)
        {
            if (id == 10)
            {
                storage.FishCurrancy = 1;
                persistenProgressService.Save(storage);
            }

            if (id == 20)
            {
                storage.EmeraldCurrancy = 9;
                persistenProgressService.Save(storage);
            }
        }

        private void ShowAdvButton(int id) =>
            yandexGameSDK._RewardedShow(id);

        private void BuyEmerald()
        {
            ShowAdvButton(20);
        }

        private void BuyFish()
        {
            ShowAdvButton(10);
        }
    }
}