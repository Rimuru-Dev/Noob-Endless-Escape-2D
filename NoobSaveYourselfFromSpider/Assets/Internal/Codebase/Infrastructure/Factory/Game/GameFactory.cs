// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System.Diagnostics.CodeAnalysis;
using Internal.Codebase.Infrastructure.AssetManagement;
using Internal.Codebase.Infrastructure.Services.StaticData;
using Internal.Codebase.Runtime.EndlessLevelGenerationSolution.Handlers;

namespace Internal.Codebase.Infrastructure.Factory
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
            var levelGenerationHandler = assetProvider.Instantiate<EndlessLevelGenerationHandler>(AssetPath.LevelGeneratorHandler);

            levelGenerationHandler.Constructor(staticDataService.ForEndlessLevelGenerationConfig());

            return levelGenerationHandler;
        }
    }
}