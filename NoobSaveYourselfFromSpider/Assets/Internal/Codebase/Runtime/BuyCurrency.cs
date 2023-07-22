// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using UnityEngine;
using UnityEngine.UI;

namespace Internal.Codebase.Runtime
{
    public sealed class BuyCurrency : MonoBehaviour
    {
        public Button buyFish;
        public Button buyEmerald;

        private void Start()
        {
            buyFish.onClick.AddListener(BuyFish);
            buyEmerald.onClick.AddListener(BuyEmerald);
        }

        private void OnDestroy()
        {
            buyFish.onClick.RemoveListener(BuyFish);
            buyEmerald.onClick.RemoveListener(BuyEmerald);
        }

        private void BuyEmerald()
        {
        }

        private void BuyFish()
        {
        }
    }
}