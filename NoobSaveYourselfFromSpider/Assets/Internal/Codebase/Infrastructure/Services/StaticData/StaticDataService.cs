// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using Internal.Codebase.Infrastructure.AssetManagement;
using Internal.Codebase.Infrastructure.Services.Resource;
using Internal.Codebase.Runtime;
using Internal.Codebase.Runtime.GameplayScene.Hero;
using Internal.Codebase.Runtime.GameplayScene.Hero.Configs;
using Internal.Codebase.Runtime.GameplayScene.LevelGeneration.Configs;
using Internal.Codebase.Runtime.General.Curtain;
using Internal.Codebase.Runtime.General.StorageData;
using Internal.Codebase.Runtime.MainMenu;
using Internal.Codebase.Runtime.MainMenu.Configs;
using Zenject;

namespace Internal.Codebase.Infrastructure.Services.StaticData
{
    public sealed class StaticDataService : IStaticDataService, IDisposable
    {
        private readonly IResourceLoaderService resourceLoader;

        private Skins skins;
        private HeroConfig heroConfig;
        private CurtainConfig curtainConfig;
        private MainMenuUIConfig mainMenuUIConfig;
        private EndlessLevelGenerationConfig greenPlains;
        private EndlessLevelGenerationConfig snowyWastelands;

        [Inject]
        public StaticDataService(IResourceLoaderService resourceLoader) =>
            this.resourceLoader = resourceLoader;

        public void Initialize()
        {
            mainMenuUIConfig = resourceLoader.Load<MainMenuUIConfig>(AssetPath.MainMenuUIConfig);
            curtainConfig = resourceLoader.Load<CurtainConfig>(AssetPath.Curtain);
            heroConfig = resourceLoader.Load<HeroConfig>(AssetPath.HeroConfig);
            greenPlains = resourceLoader.Load<EndlessLevelGenerationConfig>(AssetPath.GreenPlains);
            snowyWastelands = resourceLoader.Load<EndlessLevelGenerationConfig>(AssetPath.SnowyWastelands);
            skins = resourceLoader.Load<Skins>(AssetPath.Skins);
        }

        public void Dispose()
        {
            skins = null;
            heroConfig = null;
            curtainConfig = null;
            mainMenuUIConfig = null;
            GreenPlains = null;
            SnowyWastelands = null;
        }

        public EndlessLevelGenerationConfig GreenPlains
        {
            get => greenPlains;
            set => greenPlains = value;
        }

        public EndlessLevelGenerationConfig SnowyWastelands
        {
            get => snowyWastelands;
            set => snowyWastelands = value;
        }


        public CurtainConfig ForCurtain() =>
            curtainConfig;

        public HeroConfig ForHero() =>
            heroConfig;

        public MainMenuUIConfig ForMainMenuUI() =>
            mainMenuUIConfig;

        public Skins ForSkins() =>
            skins;
    }
}