// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System.Diagnostics.CodeAnalysis;
using AbyssMoth.Internal.Codebase.Infrastructure.AssetManagement;
using AbyssMoth.Internal.Codebase.Infrastructure.Factory.Hero;
using AbyssMoth.Internal.Codebase.Infrastructure.Factory.UI;
using AbyssMoth.Internal.Codebase.Infrastructure.Services.CoroutineRunner;
using AbyssMoth.Internal.Codebase.Infrastructure.Services.Curtain;
using AbyssMoth.Internal.Codebase.Infrastructure.Services.Resource;
using AbyssMoth.Internal.Codebase.Infrastructure.Services.SceneLoader;
using AbyssMoth.Internal.Codebase.Infrastructure.Services.StaticData;
using AbyssMoth.Internal.Codebase.Infrastructure.Services.Storage;
using Zenject;

namespace AbyssMoth.Internal.Codebase.Infrastructure.Installers
{
    [SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
    public sealed class ServiceInstaller : MonoInstaller, ICoroutineRunner
    {
        public override void InstallBindings()
        {
            BindServices();
            BindFactory();
        }

        private void BindServices()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle();
            Container.Bind<ICurtainService>().To<CurtainService>().AsSingle();
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<ISceneLoaderService>().To<SceneLoaderService>().AsSingle();
            Container.Bind<IStorageService>().To<JsonToFileStorageService>().AsSingle();
            Container.Bind<IResourceLoaderService>().To<ResourceLoaderServiceService>().AsSingle();
        }

        private void BindFactory()
        {
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
            Container.Bind<IHeroFactory>().To<HeroFactory>().AsSingle();
        }
    }
}