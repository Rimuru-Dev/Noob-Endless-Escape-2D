// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Internal.Codebase.Runtime;
using Internal.Codebase.Runtime.Curtain;
using Internal.Codebase.Runtime.EndlessLevelGenerationSolution.Configs;
using Internal.Codebase.Runtime.Hero;
using Internal.Codebase.Runtime.MainMenu.New.Configs;

namespace Internal.Codebase.Infrastructure.Services.StaticData
{
    public interface IStaticDataService
    {
        public void Initialize();

        public CurtainConfig ForCurtain();

        // public MainMenuConfig ForMainMenu();
        public HeroConfig ForHero();
        public Skins ForSkins();
        public EndlessLevelGenerationConfig GreenPlains { get; set; }
        public EndlessLevelGenerationConfig SnowyWastelands { get; set; }


        public MainMenuUIConfig ForMainMenuUI();
    }
}