using System;
using Internal.Codebase.Infrastructure.Services.CloudSave;
using Internal.Codebase.Runtime.SpriteTextNumberCounterLogic;
using Internal.Codebase.Runtime.StorageData;
using UnityEngine;

namespace Internal.Codebase.Runtime.MainMenu.New.Currency
{
    [DisallowMultipleComponent]
    public sealed class CurrancyUIView : MonoBehaviour
    {
        [field: SerializeField] public CurrancyTypeID CurrancyTypeID { get; set; }
        [field: SerializeField] public NumberVisualizer NumberVisualizer { get; private set; }

        private Storage storage;
        private IYandexSaveService yandexSaveService;

        public void Constructor(IYandexSaveService saveService) =>
            yandexSaveService = saveService;

        private void OnDestroy() =>
            UnsubscribeNumberVisualizer();

        public void Prepare()
        {
            storage = yandexSaveService.Load();

            SubscribeNumberVisualizer();

            storage.Refresh();
        }

        private void SubscribeNumberVisualizer()
        {
            switch (CurrancyTypeID)
            {
                case CurrancyTypeID.Emerald:
                    storage.OnEmeraldCurrancyChanged += NumberVisualizer.ShowNumber;
                    break;
                case CurrancyTypeID.Fish:
                    storage.OnFishCurrancyChanged += NumberVisualizer.ShowNumber;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UnsubscribeNumberVisualizer()
        {
            switch (CurrancyTypeID)
            {
                case CurrancyTypeID.Emerald:
                    storage.OnEmeraldCurrancyChanged -= NumberVisualizer.ShowNumber;
                    break;
                case CurrancyTypeID.Fish:
                    storage.OnFishCurrancyChanged -= NumberVisualizer.ShowNumber;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}