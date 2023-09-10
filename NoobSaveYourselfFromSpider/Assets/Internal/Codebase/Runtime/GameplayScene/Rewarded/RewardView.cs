// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Internal.Codebase.Infrastructure.Services.CloudSave;
using Internal.Codebase.Runtime.General.StorageData;
using Internal.Codebase.Utilities.Constants;
using UnityEngine;

namespace Internal.Codebase.Runtime.GameplayScene.Rewarded
{
    public sealed class RewardView : MonoBehaviour
    {
        [SerializeField] private int emeraldCurrancyRewardCount = 1;

        private Storage storage;
        private bool isCollectRewarded;
        private IYandexSaveService saveService;

        public void Constructor(IYandexSaveService yandexSaveService) =>
            saveService = yandexSaveService;

        public void Prepare() =>
            storage = saveService.Load();

        private void OnDestroy() =>
            saveService.Save(storage);

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (IsForbiddenToCollect(other))
                return;

            isCollectRewarded = true;
            storage.EmeraldCurrancy = emeraldCurrancyRewardCount;

            Destroy(gameObject);
        }

        private bool IsForbiddenToCollect(Collision2D other) =>
            !other.transform.CompareTag(Tag.Player) || isCollectRewarded;
    }
}