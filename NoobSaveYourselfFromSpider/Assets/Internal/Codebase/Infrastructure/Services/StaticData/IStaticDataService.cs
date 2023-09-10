// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using Internal.Codebase.Runtime.GameplayScene.Hero.Configs;
using Internal.Codebase.Runtime.GameplayScene.LevelGeneration.Configs;
using Internal.Codebase.Runtime.GameplayScene.UI.Configs;
using Internal.Codebase.Runtime.General.Curtain;
using Internal.Codebase.Runtime.General.StorageData;
using Internal.Codebase.Runtime.MainMenu.Configs;

namespace Internal.Codebase.Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IDisposable
    {
        public void Initialize();
        public CurtainConfig ForCurtain();
        public HeroConfig ForHero();
        public Skins ForSkins();
        public EndlessLevelGenerationConfig GreenPlains { get; set; }
        public EndlessLevelGenerationConfig SnowyWastelands { get; set; }
        public MainMenuUIConfig ForMainMenuUI();
        public GameplaySceneUIConfig ForGameplaySceneUI();
    }
}