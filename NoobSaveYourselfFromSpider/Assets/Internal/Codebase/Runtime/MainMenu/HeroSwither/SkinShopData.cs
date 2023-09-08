using System;
using UnityEngine;

namespace Internal.Codebase.Runtime.MainMenu.HeroSwither
{
    [Serializable]
    public sealed class SkinShopData
    {
        public int id;

        [NaughtyAttributes.ShowAssetPreview(256, 256)]
        public Sprite icon;

        public CurrancyTypeID priceType;
        public int price;
        public bool isOpen;
    }
}