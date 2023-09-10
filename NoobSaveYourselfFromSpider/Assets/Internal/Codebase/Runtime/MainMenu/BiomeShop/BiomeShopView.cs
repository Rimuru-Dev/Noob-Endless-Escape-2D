// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Internal.Codebase.Infrastructure.Services.CloudSave;
using Internal.Codebase.Runtime.GameplayScene.LevelGeneration.Configs;
using Internal.Codebase.Runtime.General.SpriteTextNumberCounterLogic;
using Internal.Codebase.Runtime.General.StorageData;
using UnityEngine;
using UnityEngine.UI;

namespace Internal.Codebase.Runtime.MainMenu.BiomeShop
{
    [DisallowMultipleComponent]
    public sealed class BiomeShopView : MonoBehaviour
    {
        private const int BiomePrice = 5; // TODO: Add SO Config

        private Storage storage;
        private IYandexSaveService saveService;

        [field: SerializeField] public Button PlayBiomeForest { get; private set; }
        [field: SerializeField] public Button PlayBiomWinter { get; private set; }
        [field: SerializeField] public GameObject LookIcon { get; private set; }
        [field: SerializeField] public Button BuyBiomWinter { get; private set; }
        [field: SerializeField] public NumberVisualizer NumberVisualizer { get; private set; }
        [field: SerializeField] public GameObject RootPanel { get; private set; }
        [field: SerializeField] public Button CloseWindow { get; private set; }
        [field: SerializeField] public Button OpenPanel { get; private set; }

        public void Constructor(IYandexSaveService yandexSaveService) =>
            saveService = yandexSaveService;

        private void OnDestroy()
        {
            BuyBiomWinter.onClick.RemoveAllListeners();
            PlayBiomeForest.onClick.RemoveAllListeners();
            PlayBiomWinter.onClick.RemoveAllListeners();
        }

        public void Prepare()
        {
            storage = saveService.Load();

            UpdateForestBiom();
        }

        private void Save() =>
            saveService.Save(storage);

        private void UpdateForestBiom()
        {
            var bioms = storage.userBioms;
            if (bioms.biomeData[1].isOpen)
            {
                BuyBiomWinter.gameObject.SetActive(false);
                LookIcon.gameObject.SetActive(false);
                NumberVisualizer.gameObject.SetActive(false);
            }
            else
            {
                PlayBiomWinter.gameObject.SetActive(false);

                BuyBiomWinter.gameObject.SetActive(true);
                BuyBiomWinter.onClick.AddListener(Buy);
                LookIcon.gameObject.SetActive(true);
                NumberVisualizer.gameObject.SetActive(true);
            }
        }

        private void PlayWinter() =>
            storage.userBioms.selectionBiomId = BiomeTypeID.SnowyWastelands;

        private void PlayForest() =>
            storage.userBioms.selectionBiomId = BiomeTypeID.GreenPlains;

        private void Buy()
        {
            if (storage.fishCurrancy.fishs < BiomePrice)
                return;

            storage.FishCurrancy = -BiomePrice;
            storage.userBioms.biomeData[1].isOpen = true;

            PlayBiomWinter.gameObject.SetActive(true);
            {
                storage.userBioms.selectionBiomId = BiomeTypeID.SnowyWastelands;
                PlayBiomWinter.onClick.AddListener(PlayWinter);
            }

            BuyBiomWinter.gameObject.SetActive(false);
            LookIcon.gameObject.SetActive(false);
            NumberVisualizer.gameObject.SetActive(false);

            Save();
        }
    }
}