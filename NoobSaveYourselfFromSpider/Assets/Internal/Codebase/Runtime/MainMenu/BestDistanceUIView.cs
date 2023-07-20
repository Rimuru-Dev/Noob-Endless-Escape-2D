// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using Internal.Codebase.Runtime.SpriteTextNumberCounterLogic;
using Internal.Codebase.Runtime.StorageData;
using UnityEngine;

namespace Internal.Codebase.Runtime.MainMenu
{
    public sealed class BestDistanceUIView : MonoBehaviour
    {
        [field: SerializeField] public NumberVisualizer NumberVisualizer { get; private set; }

        private Storage storage;

        public void Initialize(Storage storage)
        {
            this.storage = storage;

            storage.OnBestDistanceChanged += NumberVisualizer.ShowNumber;
        }

        private void OnDestroy() =>
            storage.OnBestDistanceChanged -= NumberVisualizer.ShowNumber;
    }
}