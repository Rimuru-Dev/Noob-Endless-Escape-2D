// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Zenject;
using Cinemachine;
using Internal.Codebase.Infrastructure.AssetManagement;
using Internal.Codebase.Infrastructure.GeneralGameStateMachine.States;
using Internal.Codebase.Infrastructure.Services.StaticData;
using Internal.Codebase.Runtime.GameplayScene.Hero;
using Internal.Codebase.Runtime.GameplayScene.Hero.Death;
using Internal.Codebase.Runtime.GameplayScene.Hero.View;

namespace Internal.Codebase.Infrastructure.Factory.Hero
{
    public sealed class HeroFactory : IHeroFactory
    {
        private readonly IAssetProvider assetProvider;
        private readonly IStaticDataService staticData;
        public HeroViewController Hero { get; private set; }
        public CinemachineVirtualCamera HeroCamera { get; private set; }
        public HeroController GetHeroController { get; private set; }

        [Inject]
        public HeroFactory(IAssetProvider assetProvider, IStaticDataService staticData)
        {
            this.assetProvider = assetProvider;
            this.staticData = staticData;
        }

        public HeroViewController CreateHero() =>
            Hero = assetProvider.Instantiate(staticData.ForHero().HeroViewControllerPrefab);

        public HeroController CreateHeroController()
        {
            GetHeroController?.Dispose();

            return GetHeroController = new HeroController(Hero, new HeroDeath());
        }

        public void CreateHeroCamera()
        {
            var heroConfig = staticData.ForHero();

            HeroCamera = assetProvider.Instantiate(heroConfig.HeroVirtualCamera);

            HeroCamera.Follow = Hero.transform;
        }

        public void Dispose()
        {
            GetHeroController?.Dispose();
        }
    }
}