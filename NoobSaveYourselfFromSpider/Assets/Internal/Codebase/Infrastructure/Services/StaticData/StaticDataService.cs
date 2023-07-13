// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Internal.Codebase.Infrastructure.AssetManagement;
using Internal.Codebase.Infrastructure.Services.Resource;
using Internal.Codebase.Runtime.Curtain;
using Zenject;

namespace Internal.Codebase.Infrastructure.Services.StaticData
{
    public sealed class StaticDataService : IStaticDataService
    {
        private readonly IResourceLoaderService resourceLoader;

        private CurtainConfig curtainConfig;

        [Inject]
        public StaticDataService(IResourceLoaderService resourceLoader)
        {
            this.resourceLoader = resourceLoader;
        }

        public void Initialize()
        {
            curtainConfig = resourceLoader.Load<CurtainConfig>(AssetPath.Curtain);
        }

        public CurtainConfig ForCurtain()
        {
            return curtainConfig;
        }
    }
}