// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Internal.Codebase.Infrastructure.Services.PersistenProgress;
using Internal.Codebase.Runtime.StorageData;
using UnityEngine;

namespace Internal.Codebase.Runtime
{
    public sealed class RewardView : MonoBehaviour
    {
        public int emeraldCurrancyRewardCount = 1;

        private Storage storage;
        private IPersistenProgressService persistenProgressService;
        private bool isCollectRewarded;

        public void Constructor(Storage storage, IPersistenProgressService persistenProgressService)
        {
            this.storage = storage;
            this.persistenProgressService = persistenProgressService;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.transform.CompareTag("Player"))
                return;

            if (isCollectRewarded)
                return;

            isCollectRewarded = true;
            storage.EmeraldCurrancy = emeraldCurrancyRewardCount;
            persistenProgressService.Save(storage);

            Destroy(gameObject);
        }
    }
}