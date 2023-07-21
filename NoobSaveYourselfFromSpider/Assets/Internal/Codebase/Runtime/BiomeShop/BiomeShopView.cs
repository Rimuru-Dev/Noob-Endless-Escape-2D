// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Internal.Codebase.Runtime.SpriteTextNumberCounterLogic;
using UnityEngine;
using UnityEngine.UI;

namespace Internal.Codebase.Runtime.BiomeShop
{
    public sealed class BiomeShopView : MonoBehaviour
    {
        [field: SerializeField] public Button PlayBiomeForest { get; private set; }
        [field: SerializeField] public Button PlayBiomWinter { get; private set; }

        [field: SerializeField] public GameObject LookIcon { get; private set; }

        [field: SerializeField] public Button BuyBiomWinter { get; private set; }
        [field: SerializeField] public NumberVisualizer NumberVisualizer { get; private set; }
        [field: SerializeField] public GameObject RootPanel { get; private set; }
        [field: SerializeField] public Button CloseWindow { get; private set; }
    }
}