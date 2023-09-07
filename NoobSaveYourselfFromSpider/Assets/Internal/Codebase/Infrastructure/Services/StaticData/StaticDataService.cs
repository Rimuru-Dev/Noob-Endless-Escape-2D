// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System.Collections.Generic;
using Internal.Codebase.Infrastructure.AssetManagement;
using Internal.Codebase.Infrastructure.Services.Resource;
using Internal.Codebase.Runtime;
using Internal.Codebase.Runtime.Curtain;
using Internal.Codebase.Runtime.EndlessLevelGenerationSolution.Configs;
using Internal.Codebase.Runtime.Hero;
using Internal.Codebase.Runtime.MainMenu.Configs;
using Internal.Codebase.Runtime.MainMenu.New.Configs;
using Zenject;

namespace Internal.Codebase.Infrastructure.Services.StaticData
{
    public sealed class StaticDataService : IStaticDataService
    {
        private readonly IResourceLoaderService resourceLoader;

        private CurtainConfig curtainConfig;
        private MainMenuConfig mainMenuConfig;
        private HeroConfig heroConfig;
        //private EndlessLevelGenerationConfig levelGenerationConfig;
        private Skins skins;

        private EndlessLevelGenerationConfig greenPlains;
        private EndlessLevelGenerationConfig snowyWastelands;

        private MainMenuUIConfig mainMenuUIConfig;
        
        // private Dictionary<BiomeTypeID, EndlessLevelGenerationConfig> biomes;

        [Inject]
        public StaticDataService(IResourceLoaderService resourceLoader) =>
            this.resourceLoader = resourceLoader;

        public void Initialize()
        {
            mainMenuUIConfig = resourceLoader.Load<MainMenuUIConfig>(AssetPath.MainMenuUIConfig);
            
            curtainConfig = resourceLoader.Load<CurtainConfig>(AssetPath.Curtain);
            mainMenuConfig = resourceLoader.Load<MainMenuConfig>(AssetPath.MainMenuConfig);
            heroConfig = resourceLoader.Load<HeroConfig>(AssetPath.HeroConfig);

            greenPlains = resourceLoader.Load<EndlessLevelGenerationConfig>("Biomes/Configs/GreenPlains");
            snowyWastelands = resourceLoader.Load<EndlessLevelGenerationConfig>("Biomes/Configs/SnowyWastelands");
            skins = resourceLoader.Load<Skins>("Skins/Skins");


            // levelGenerationConfig =
            //     resourceLoader.LoadAll<EndlessLevelGenerationConfig>(AssetPath.BiomeConfigs);
        }

        public CurtainConfig ForCurtain() =>
            curtainConfig;

        public MainMenuConfig ForMainMenu() =>
            mainMenuConfig;

        public HeroConfig ForHero() =>
            heroConfig;

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

        public MainMenuUIConfig ForMainMenuUI() => 
            mainMenuUIConfig;

        public Skins ForSkins() => skins;
    }
}