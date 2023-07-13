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
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Infrastructure.Factory.Hero
{
    public sealed class HeroFactory : IHeroFactory
    {
        private readonly IAssetProvider assetProvider;
        private readonly IStaticDataService staticData;
        private readonly IStorageService storageService;

        public GameObject Hero { get; }

        [Inject]
        public HeroFactory(IAssetProvider assetProvider, IStaticDataService staticData, IStorageService storageService)
        {
            this.assetProvider = assetProvider;
            this.staticData = staticData;
            this.storageService = storageService;
        }

        public GameObject CreateHero()
        {
            throw new NotImplementedException("Hero");
        }
    }
}