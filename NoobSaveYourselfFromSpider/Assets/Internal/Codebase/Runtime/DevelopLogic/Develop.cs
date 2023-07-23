// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Internal.Codebase.Infrastructure.Services.PersistenProgress;
using UnityEngine;
using YG;
using Zenject;

namespace Internal.Codebase.Runtime.DevelopLogic
{
    public sealed class Develop : MonoBehaviour
    {
#if UNITY_EDITOR
        [Inject] private IPersistenProgressService persistenProgressService;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
                YandexGame.SaveProgress();

            if (Input.GetKeyDown(KeyCode.R))
                YandexGame.ResetSaveProgress();

            if (Input.GetKeyDown(KeyCode.T))
            {
                var data = persistenProgressService.GetStoragesData();

                data.EmeraldCurrancy = 50;
                data.FishCurrancy = 45;
                data.BestDistance = 10;
                //     YandexGame.SaveProgress();
            }
        }
#endif
    }
}