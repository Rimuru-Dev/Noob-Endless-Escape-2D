﻿// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System.Diagnostics.CodeAnalysis;
using Internal.Codebase.Infrastructure.AssetManagement;
using Internal.Codebase.Infrastructure.Services.StaticData;
using Internal.Codebase.Runtime.EndlessLevelGenerationSolution.Configs;
using Internal.Codebase.Runtime.EndlessLevelGenerationSolution.Handlers;
using YG;

namespace Internal.Codebase.Infrastructure.Factory.Game
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public sealed class GameFactory : IGameFactory
    {
        private readonly IAssetProvider assetProvider;
        private readonly IStaticDataService staticDataService;

        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticDataService)
        {
            this.assetProvider = assetProvider;
            this.staticDataService = staticDataService;
        }

        public EndlessLevelGenerationHandler CreateLevelGenerator()
        {
            var levelGenerationHandler =
                assetProvider.Instantiate<EndlessLevelGenerationHandler>(AssetPath.LevelGeneratorHandler);

            var config = YandexGame.savesData.storage;

            var selectionBiom = config.userBioms.selectionBiomId;

            var biom = selectionBiom switch
            {
                BiomeTypeID.GreenPlains => staticDataService.GreenPlains,
                BiomeTypeID.SnowyWastelands => staticDataService.SnowyWastelands,
                _ => staticDataService.GreenPlains
            };

            levelGenerationHandler.Constructor(biom, YandexGame.savesData.storage);

            return levelGenerationHandler;
        }
    }
}