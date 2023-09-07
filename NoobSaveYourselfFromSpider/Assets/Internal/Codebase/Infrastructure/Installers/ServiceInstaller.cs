// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System.Diagnostics.CodeAnalysis;
using Internal.Codebase.Infrastructure.AssetManagement;
using Internal.Codebase.Infrastructure.Services;
using Internal.Codebase.Infrastructure.Services.ActionUpdater;
using Internal.Codebase.Infrastructure.Services.CloudSave;
using Internal.Codebase.Infrastructure.Services.CoroutineRunner;
using Internal.Codebase.Infrastructure.Services.Curtain;
using Internal.Codebase.Infrastructure.Services.PersistenProgress;
using Internal.Codebase.Infrastructure.Services.Resource;
using Internal.Codebase.Infrastructure.Services.SceneLoader;
using Internal.Codebase.Infrastructure.Services.StaticData;
using Internal.Codebase.Infrastructure.Services.Storage;
using UnityEngine;
using YG;
using Zenject;

namespace Internal.Codebase.Infrastructure.Installers
{
    [SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
    public sealed class ServiceInstaller : MonoInstaller, ICoroutineRunner
    {
        [SerializeField] private YandexGame yandexGame;

        public override void InstallBindings() =>
            BindServices();

        private void BindServices()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle();
            Container.Bind<ICurtainService>().To<CurtainService>().AsSingle();
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<ISceneLoaderService>().To<SceneLoaderService>().AsSingle();
            Container.Bind<IStorageService>().To<JsonToFileStorageService>().AsSingle();
            Container.Bind<IPersistenProgressService>().To<PersistenProgressService>().AsSingle();
            Container.Bind<IResourceLoaderService>().To<ResourceLoaderServiceService>().AsSingle();

            Container.Bind<IActionUpdaterService>().To<ActionUpdaterService>().AsSingle();

            BindYandexGamesCloudSaveService();
        }

        private void BindYandexGamesCloudSaveService()
        {
            Container.Bind<YandexGame>().FromInstance(yandexGame).AsSingle();

            Container
                .Bind<IYandexGamesCloudSaveService>()
                .To<YandexGamesCloudSaveService>()
                .AsSingle()
                .WithArguments(yandexGame);
        }
    }
}