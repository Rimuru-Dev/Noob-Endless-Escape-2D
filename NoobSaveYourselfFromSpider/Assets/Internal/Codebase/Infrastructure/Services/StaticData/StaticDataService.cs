// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using Internal.Codebase.Infrastructure.AssetManagement;
using Internal.Codebase.Infrastructure.Services.Resource;
using Internal.Codebase.Runtime.Curtain;
using Internal.Codebase.Runtime.MainMenu.Configs;
using Internal.Codebase.Runtime.StorageData;
using UnityEngine;
using YG;
using Zenject;
using AudioSettings = Internal.Codebase.Runtime.StorageData.AudioSettings;

namespace Internal.Codebase.Infrastructure.Services.StaticData
{
    public sealed class StaticDataService : IStaticDataService
    {
        private readonly IResourceLoaderService resourceLoader;

        private CurtainConfig curtainConfig;
        private MainMenuConfig mainMenuConfig;

        [Inject]
        public StaticDataService(IResourceLoaderService resourceLoader)
        {
            this.resourceLoader = resourceLoader;
        }

        public void Initialize()
        {
            curtainConfig = resourceLoader.Load<CurtainConfig>(AssetPath.Curtain);
            mainMenuConfig = resourceLoader.Load<MainMenuConfig>(AssetPath.MainMenuConfig);
        }

        public CurtainConfig ForCurtain()
        {
            return curtainConfig;
        }

        public MainMenuConfig ForMainMenu()
        {
            return mainMenuConfig;
        }
    }

    public sealed class PersistenProgressService : IPersistenProgressService, IDisposable
    {
        private Runtime.StorageData.Storage storage;

        public void Init()
        {
            storage = new Runtime.StorageData.Storage();

            if (YandexGame.SDKEnabled)
                Load();
            else
                Debug.Log("Eroor Loaded");

            YandexGame.GetDataEvent += Load;
        }

        public Runtime.StorageData.Storage GetStoragesData() => storage;

        public void Save(Runtime.StorageData.Storage newStorage)
        {
            if (newStorage != null)
                YandexGame.savesData.storage = newStorage;
        }

        public void Load()
        {
            Debug.Log("<color=yellow>Loaded !!!</color>");

            if (YandexGame.savesData.storage == null)
            {
                Debug.Log("<color=yellow>Load Default</color>");
                var newStorage = new Runtime.StorageData.Storage
                {
                    currancys = new Currancys(),
                    audioSettings = new AudioSettings(),
                    userBioms = new UserBioms(),
                    userSkins = new UserSkins(),
                    userBestDistance = new UserBestDistance()
                };
                newStorage.currancys.emerald = 150;
                newStorage.currancys.fish = 15;

                newStorage.userBestDistance.bestDistance = 35;

                YandexGame.savesData.storage = newStorage;
                storage = newStorage;
            }
            else
            {
                Debug.Log("<color=yellow>Load Old Save</color>");
                storage = YandexGame.savesData.storage;
            }
        }

        public void Dispose()
        {
            YandexGame.GetDataEvent -= Load;
        }
    }

    public interface IPersistenProgressService
    {
        public void Init();
        public Runtime.StorageData.Storage GetStoragesData();
        public void Save(Runtime.StorageData.Storage storage);
        public void Load();
    }
}