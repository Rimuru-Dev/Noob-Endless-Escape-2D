// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

namespace Internal.Codebase.Infrastructure.AssetManagement
{
    public readonly struct AssetPath
    {
        // General - Static Data
        public const string Curtain = "StaticData/Curtain/Config/CurtainConfig";

        // Gameplay Scene - Static Data
        public const string GameplaySceneUIConfig = "StaticData/GameplayScene/UI/Configs/GameplaySceneUIConfig";
        public const string HeroConfig = "StaticData/GameplayScene/Hero/Configs/HeroConfig";
        public const string GreenPlains = "StaticData/GameplayScene/Biomes/Configs/GreenPlains";
        public const string SnowyWastelands = "StaticData/GameplayScene/Biomes/Configs/SnowyWastelands";

        // Main Menu Scene - Static Data
        public const string Skins = "StaticData/MainMenuScene/Skins/Skins";
        public const string MainMenuUIConfig = "StaticData/MainMenuScene/Configs/MainMenuUIConfig";
        public const string LevelGeneratorHandler = "StaticData/GameplayScene/Biomes/Prefabs/LevelGenerator";
    }
}