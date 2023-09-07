// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using Internal.Codebase.Runtime.MainMenu.Animation;
using Internal.Codebase.Runtime.StorageData;
using UnityEngine;
using YG;

namespace Internal.Codebase.Runtime
{
    public sealed class RewardView : MonoBehaviour
    {
        public int emeraldCurrancyRewardCount = 1;

        private Storage storage;

        // private IPersistenProgressService persistenProgressService;
        private bool isCollectRewarded;

        private void Awake()
        {
            if (YandexGame.SDKEnabled)
                Load();

            YandexGame.GetDataEvent += Load;
        }

        public void Save()
        {
            if (storage == null)
            {
                Load();
            }

            YandexGame.savesData.storage.emeraldCurrancy = storage.emeraldCurrancy;
        }

        public void Load()
        {
            storage = YandexGame.savesData.storage;
        }

        public void Constructor(Storage storage)
        {
            //   this.storage = storage;
            //   this.persistenProgressService = persistenProgressService;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.transform.CompareTag("Player"))
                return;

            if (isCollectRewarded)
                return;

            isCollectRewarded = true;
            storage.EmeraldCurrancy = emeraldCurrancyRewardCount;
            // persistenProgressService.Save(storage);

            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            Save();
            YandexGame.GetDataEvent -= Load;
        }
    }
}