// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using Internal.Codebase.Infrastructure.Services.PersistenProgress;
using Internal.Codebase.Infrastructure.Services.StaticData;
using Internal.Codebase.Runtime.EndlessLevelGenerationSolution.Configs;
using Internal.Codebase.Runtime.MainMenu;
using Internal.Codebase.Runtime.MainMenu.Animation;
using Internal.Codebase.Runtime.SpriteTextNumberCounterLogic;
using Internal.Codebase.Runtime.StorageData;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Internal.Codebase.Runtime.BiomeShop
{
    public sealed class BiomeShopView : MonoBehaviour, IFuckingSaveLoad
    {
        private const int BiomePrice = 5;
        private const CurrancyTypeID BiomeCurrancyTypeID = CurrancyTypeID.Fish;

        private Storage storage;
        // private IStaticDataService staticData;
        // private IPersistenProgressService persistenProgressService;

        [field: SerializeField] public Button PlayBiomeForest { get; private set; }
        [field: SerializeField] public Button PlayBiomWinter { get; private set; }

        [field: SerializeField] public GameObject LookIcon { get; private set; }

        [field: SerializeField] public Button BuyBiomWinter { get; private set; }
        [field: SerializeField] public NumberVisualizer NumberVisualizer { get; private set; }
        [field: SerializeField] public GameObject RootPanel { get; private set; }
        [field: SerializeField] public Button CloseWindow { get; private set; }

        private void Awake()
        {
            if (YandexGame.SDKEnabled)
                Load();

            YandexGame.GetDataEvent += Load;
        }

        public void Save()
        {
            YandexGame.savesData.storage.userBioms = storage.userBioms;
            YandexGame.savesData.storage.fishCurrancy = storage.fishCurrancy;
        }

        public void Load()
        {
            storage = YandexGame.savesData.storage;
            var bioms = storage.userBioms;

            if (bioms.biomeData[0].isOpen)
            {
            }
            else
                Debug.Log("ERRIRRRRR");

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
                //  NumberVisualizer.ShowNumber(BiomePrice);
            }
        }

        public void Initialize(Storage storage)
        {
            this.storage = storage;

            var bioms = storage.userBioms;

            if (bioms.biomeData[0].isOpen)
            {
            }
            else
                Debug.Log("ERRIRRRRR");

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
                //  NumberVisualizer.ShowNumber(BiomePrice);
            }

//             foreach (var biomsData in bioms.biomeData)
//             {
//                 if (biomsData.isOpen)
//                 {
//                     PlayBiomWinter.gameObject.SetActive(true);
//                     {
//                         // subscribe and selec biome
//                         PlayBiomWinter.onClick.AddListener(PlayWinter);
//                     }
//
//                     BuyBiomWinter.gameObject.SetActive(false);
//                     LookIcon.gameObject.SetActive(false);
//                     NumberVisualizer.gameObject.SetActive(false);
//                 }
//                 else
//                 {
//                     PlayBiomWinter.gameObject.SetActive(false);
//
//                     BuyBiomWinter.gameObject.SetActive(true);
//                     BuyBiomWinter.onClick.AddListener(Buy);
//                     LookIcon.gameObject.SetActive(true);
//                     NumberVisualizer.gameObject.SetActive(false);
// //                    NumberVisualizer.ShowNumber(BiomePrice);
//                 }
//             }
        }

        private void PlayWinter()
        {
            storage.userBioms.selectionBiomId = BiomeTypeID.SnowyWastelands;
        }

        private void PlayForest()
        {
            storage.userBioms.selectionBiomId = BiomeTypeID.GreenPlains;
        }

        private void Buy()
        {
            Debug.Log("Buy");
            if (storage.fishCurrancy.fishs >= BiomePrice)
            {
                storage.FishCurrancy = -BiomePrice;

                storage.userBioms.biomeData[1].isOpen = true;


                PlayBiomWinter.gameObject.SetActive(true);
                {
                    // subscribe and selec biome
                    storage.userBioms.selectionBiomId = BiomeTypeID.SnowyWastelands;
                    PlayBiomWinter.onClick.AddListener(PlayWinter);
                }

                BuyBiomWinter.gameObject.SetActive(false);
                LookIcon.gameObject.SetActive(false);
                NumberVisualizer.gameObject.SetActive(false);

                Save();
                // persistenProgressService.Save(storage);
            }
        }

        public void Constructor(IStaticDataService staticData, IPersistenProgressService persistenProgressService)
        {
            // this.staticData = staticData;
            // this.persistenProgressService = persistenProgressService;
        }

        private void OnDestroy()
        {
            BuyBiomWinter.onClick.RemoveAllListeners();
            PlayBiomeForest.onClick.RemoveAllListeners();
            PlayBiomWinter.onClick.RemoveAllListeners();
        }
    }
}