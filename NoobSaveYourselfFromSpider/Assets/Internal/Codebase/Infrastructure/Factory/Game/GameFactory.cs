﻿// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System.Diagnostics.CodeAnalysis;
using Internal.Codebase.Infrastructure.AssetManagement;
using Internal.Codebase.Infrastructure.Services.ActionUpdater;
using Internal.Codebase.Infrastructure.Services.CloudSave;
using Internal.Codebase.Infrastructure.Services.CoroutineRunner;
using Internal.Codebase.Infrastructure.Services.StaticData;
using Internal.Codebase.Runtime.GameplayScene.LevelGeneration.Configs;
using Internal.Codebase.Runtime.GameplayScene.LevelGeneration.Handlers;
using YG;

namespace Internal.Codebase.Infrastructure.Factory.Game
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public sealed class GameFactory : IGameFactory
    {
        private readonly IAssetProvider assetProvider;
        private readonly IStaticDataService staticDataService;
        private readonly IYandexSaveService yandexSaveService;
        private readonly IActionUpdaterService actionUpdaterService;
        private readonly ICoroutineRunner coroutineRunner;
        public EndlessLevelGenerationHandler GetCahaeLevelGeneration { get; private set; }

        public GameFactory(
            IAssetProvider assetProvider,
            IStaticDataService staticDataService,
            IYandexSaveService yandexSaveService,
            IActionUpdaterService actionUpdaterService,
            ICoroutineRunner coroutineRunner)
        {
            this.assetProvider = assetProvider;
            this.staticDataService = staticDataService;
            this.yandexSaveService = yandexSaveService;
            this.actionUpdaterService = actionUpdaterService;
            this.coroutineRunner = coroutineRunner;
        }

        public EndlessLevelGenerationHandler CreateLevelGenerator()
        {
            var config = yandexSaveService.Load();
            var selectionBiom = config.userBioms.selectionBiomId;

            var biom = selectionBiom switch
            {
                BiomeTypeID.GreenPlains => staticDataService.GreenPlains,
                BiomeTypeID.SnowyWastelands => staticDataService.SnowyWastelands,
                _ => staticDataService.GreenPlains
            };

            var levelGenerationHandler = new EndlessLevelGenerationHandler(
                coroutineRunner,
                yandexSaveService,
                actionUpdaterService,
                biom);

            GetCahaeLevelGeneration = levelGenerationHandler;

            levelGenerationHandler.Prepare();

            return levelGenerationHandler;
        }

        public void Dispose() => 
            GetCahaeLevelGeneration?.Dispose();
    }
}