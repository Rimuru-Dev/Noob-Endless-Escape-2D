// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Internal.Codebase.Infrastructure.Services.CloudSave;
using Internal.Codebase.Runtime.General.StorageData;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Internal.Codebase.Runtime.MainMenu.Currency
{
    public sealed class BuyCurrency : MonoBehaviour
    {
        public Button buyFish;
        public Button buyEmerald;
        public YandexGame yandexGameSDK;

        // TODO: Add Config
        public int rewardFish = 10;
        public int rewardEmerald = 20;

        private Storage storage;
        private IYandexSaveService yandexSaveService;

        public void Constructor(IYandexSaveService saveService) =>
            yandexSaveService = saveService;

        public void Prepare()
        {
            storage = yandexSaveService.Load();

            buyFish.onClick.AddListener(BuyFish);
            buyEmerald.onClick.AddListener(BuyEmerald);
            yandexGameSDK = FindObjectOfType<YandexGame>(true);
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
            if (id == rewardFish)
                storage.FishCurrancy = 1;

            if (id == rewardEmerald)
                storage.EmeraldCurrancy = 9;

            yandexSaveService.Save(storage);
        }

        private void ShowAdvButton(int id) =>
            yandexGameSDK._RewardedShow(id);

        private void BuyEmerald() =>
            ShowAdvButton(rewardEmerald);

        private void BuyFish() =>
            ShowAdvButton(rewardFish);
    }
}