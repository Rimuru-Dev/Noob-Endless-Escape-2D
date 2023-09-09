// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Cinemachine;
using Internal.Codebase.Infrastructure.AssetManagement;
using Internal.Codebase.Infrastructure.Services.StaticData;
using Internal.Codebase.Runtime.GameplayScene.Hero;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Infrastructure.Factory.Hero
{
    public sealed class HeroFactory : IHeroFactory
    {
        private readonly IAssetProvider assetProvider;
        private readonly IStaticDataService staticData;

        public GameObject Hero { get; private set; }

        [Inject]
        public HeroFactory(IAssetProvider assetProvider, IStaticDataService staticData)
        {
            this.assetProvider = assetProvider;
            this.staticData = staticData;
        }

        public HeroViewController CreateHero()
        {
            var heroConfig = staticData.ForHero();

            // TODO: Added override T Instantiate<T>()
            var hero = assetProvider.Instantiate(heroConfig.HeroViewControllerPrefab.gameObject).GetComponent<HeroViewController>();

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