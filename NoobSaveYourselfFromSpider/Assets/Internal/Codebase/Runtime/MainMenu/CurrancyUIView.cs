using System;
using Internal.Codebase.Runtime.MainMenu.Animation;
using Internal.Codebase.Runtime.SpriteTextNumberCounterLogic;
using Internal.Codebase.Runtime.StorageData;
using UnityEngine;
using YG;

namespace Internal.Codebase.Runtime.MainMenu
{
    [Serializable]
    public enum CurrancyTypeID
    {
        Emerald = 0,
        Fish = 1,
    }

    public sealed class CurrancyUIView : MonoBehaviour, IFuckingSaveLoad
    {
        [field: SerializeField] public CurrancyTypeID CurrancyTypeID { get; set; }
        [field: SerializeField] public NumberVisualizer NumberVisualizer { get; private set; }

        private Storage storage;

        private void Awake()
        {
            if (YandexGame.SDKEnabled)
                Load();

            YandexGame.GetDataEvent += Load;
        }

        public void Save()
        {
            YandexGame.savesData.storage.fishCurrancy = storage.fishCurrancy;
            YandexGame.savesData.storage.emeraldCurrancy = storage.emeraldCurrancy;
        }

        public void Load()
        {
            if (YandexGame.savesData.storage.emeraldCurrancy == null ||
                YandexGame.savesData.storage.fishCurrancy == null)
            {
                YandexGame.savesData.storage.emeraldCurrancy = new EmeraldCurrancy();
                YandexGame.savesData.storage.fishCurrancy = new FishCurrancy();
                YandexGame.savesData.storage.emeraldCurrancy.emeralds = 0;
                YandexGame.savesData.storage.fishCurrancy.fishs = 0;
                storage = YandexGame.savesData.storage;
            }
            else
                storage = YandexGame.savesData.storage;


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

            storage.Refresh();
        }

        public void Initialize(Storage storage)
        {
            // this.storage = storage;
            //
            // switch (CurrancyTypeID)
            // {
            //     case CurrancyTypeID.Emerald:
            //         storage.OnEmeraldCurrancyChanged += NumberVisualizer.ShowNumber;
            //         break;
            //     case CurrancyTypeID.Fish:
            //         storage.OnFishCurrancyChanged += NumberVisualizer.ShowNumber;
            //         break;
            //     default:
            //         throw new ArgumentOutOfRangeException();
            // }
        }

        private void OnDestroy()
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

            YandexGame.GetDataEvent -= Load;
        }
    }
}