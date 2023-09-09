// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Internal.Codebase.Infrastructure.Services.CloudSave;
using Internal.Codebase.Runtime.General.SpriteTextNumberCounterLogic;
using Internal.Codebase.Runtime.General.StorageData;
using UnityEngine;

namespace Internal.Codebase.Runtime.MainMenu.BestDistance
{
    [DisallowMultipleComponent]
    public sealed class BestDistanceUIView : MonoBehaviour
    {
        [field: SerializeField] public NumberVisualizer NumberVisualizer { get; private set; }

        private Storage storage;
        private IYandexSaveService saveService;

        public void Constuctor(IYandexSaveService yandexSaveService) =>
            saveService = yandexSaveService;

        public void Prepare()
        {
            storage = saveService.Load();
            storage.OnBestDistanceChanged += NumberVisualizer.ShowNumber;
            storage.Refresh();
        }

        private void OnDestroy() =>
            storage.OnBestDistanceChanged -= NumberVisualizer.ShowNumber;
    }
}