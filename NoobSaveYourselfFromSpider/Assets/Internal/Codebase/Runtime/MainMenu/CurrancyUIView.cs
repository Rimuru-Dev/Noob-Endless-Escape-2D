using System;
using Internal.Codebase.Runtime.SpriteTextNumberCounterLogic;
using Internal.Codebase.Runtime.StorageData;
using UnityEngine;

namespace Internal.Codebase.Runtime.MainMenu
{
    [Serializable]
    public enum CurrancyTypeID
    {
        Emerald = 0,
        Fish = 1,
    }

    public sealed class CurrancyUIView : MonoBehaviour
    {
        [field: SerializeField] public CurrancyTypeID CurrancyTypeID { get; set; }
        [field: SerializeField] public NumberVisualizer NumberVisualizer { get; private set; }

        private Storage storage;

        public void Initialize(Storage storage)
        {
            this.storage = storage;

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
        }
    }
}