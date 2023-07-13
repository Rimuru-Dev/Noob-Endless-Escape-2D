// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using Internal.Codebase.Infrastructure.AssetManagement;
using Internal.Codebase.Infrastructure.Services.StaticData;
using Internal.Codebase.Infrastructure.Services.Storage;
using Internal.Codebase.Runtime.Curtain;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Infrastructure.Factory.UI
{
    public sealed class UIFactory : IUIFactory
    {
        private readonly IAssetProvider assetProvider;
        private readonly IStaticDataService staticData;
        private readonly IStorageService storageService;

        private Transform root;

        [Inject]
        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticData,
            IStorageService storageService)
        {
            this.assetProvider = assetProvider;
            this.staticData = staticData;
            this.storageService = storageService;
        }

        public Transform CreateRoot()
        {
            throw new NotImplementedException("UI Root");
        }

        public CurtainView CreateCurtain()
        {
            var config = staticData.ForCurtain();
            var instance = assetProvider.Instantiate(config.CurtainView.gameObject, root);
            var view = instance.GetComponent<CurtainView>();

            view.Constructor(config.AnimationDuration);

            return view;
        }
    }
}