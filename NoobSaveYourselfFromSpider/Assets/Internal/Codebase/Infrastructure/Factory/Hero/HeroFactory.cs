// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using Cinemachine;
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

        public GameObject Hero { get; private set; }

        [Inject]
        public HeroFactory(IAssetProvider assetProvider, IStaticDataService staticData, IStorageService storageService)
        {
            this.assetProvider = assetProvider;
            this.staticData = staticData;
            this.storageService = storageService;
        }

        public Runtime.Hero.HeroViewController CreateHero()
        {
            var heroConfig = staticData.ForHero();

            // TODO: Added override T Instantiate<T>()
            var hero = assetProvider.Instantiate(heroConfig.HeroViewControllerPrefab.gameObject).GetComponent<Runtime.Hero.HeroViewController>();

            Hero = hero.gameObject;

            return hero;
        }

        public void CreateHeroCamera()
        {
            var heroConfig = staticData.ForHero();

            var heroCanera = assetProvider
                .Instantiate(heroConfig.HeroVirtualCamera.gameObject)
                .GetComponent<CinemachineVirtualCamera>();

            heroCanera.Follow = Hero.transform;
        }
    }
}