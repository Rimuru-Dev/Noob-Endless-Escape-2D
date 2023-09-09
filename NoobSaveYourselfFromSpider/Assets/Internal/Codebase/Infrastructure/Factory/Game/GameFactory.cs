// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System.Diagnostics.CodeAnalysis;
using Internal.Codebase.Infrastructure.AssetManagement;
using Internal.Codebase.Infrastructure.Services.ActionUpdater;
using Internal.Codebase.Infrastructure.Services.CloudSave;
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

        public GameFactory(
            IAssetProvider assetProvider,
            IStaticDataService staticDataService,
            IYandexSaveService yandexSaveService,
            IActionUpdaterService actionUpdaterService)
        {
            this.assetProvider = assetProvider;
            this.staticDataService = staticDataService;
            this.yandexSaveService = yandexSaveService;
            this.actionUpdaterService = actionUpdaterService;
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

            levelGenerationHandler.Constructor(yandexSaveService, actionUpdaterService, biom);
            levelGenerationHandler.Perform();

            return levelGenerationHandler;
        }
    }
}