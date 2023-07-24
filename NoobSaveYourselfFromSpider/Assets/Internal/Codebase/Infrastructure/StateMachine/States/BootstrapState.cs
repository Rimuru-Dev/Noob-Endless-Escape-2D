// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System.Collections.Generic;
using DG.Tweening;
using Internal.Codebase.Infrastructure.Services.Curtain;
using Internal.Codebase.Infrastructure.Services.PersistenProgress;
using Internal.Codebase.Infrastructure.Services.SceneLoader;
using Internal.Codebase.Infrastructure.Services.StaticData;
using Internal.Codebase.Infrastructure.Services.Storage;
using Internal.Codebase.Infrastructure.StateMachine.Interfaces;
using Internal.Codebase.Runtime.EndlessLevelGenerationSolution.Configs;
using Internal.Codebase.Runtime.StorageData;
using Internal.Codebase.Utilities.Constants;
using UnityEngine;
using YG;
using Zenject;
using AudioSettings = UnityEngine.AudioSettings;

namespace Internal.Codebase.Infrastructure.StateMachine.States
{
    public sealed class BootstrapState : IStateNext
    {
        private readonly ICurtainService curtain;
        private readonly ISceneLoaderService sceneLoader;
        private readonly IStaticDataService staticData;
        private readonly IStorageService storageService;
        private readonly IPersistenProgressService persistenProgressService;
        private GameStateMachine gameStateMachine;

        [Inject]
        public BootstrapState(
            IStaticDataService staticData,
            ISceneLoaderService sceneLoader,
            ICurtainService curtain,
            IStorageService storageService, IPersistenProgressService persistenProgressService)
        {
            this.staticData = staticData;
            this.sceneLoader = sceneLoader;
            this.curtain = curtain;
            this.storageService = storageService;
            this.persistenProgressService = persistenProgressService;
        }

        public void Init(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            if (YandexGame.SDKEnabled)
                Load();

            YandexGame.GetDataEvent += Load;

            PrepareServices();

            sceneLoader.LoadScene(SceneName.Menu, OnSceneLoaded);
        }

        private void Load()
        {
            // Debug.Log("Bootstrup Loaded Start 1!!");

            if (YandexGame.savesData.storage == null)
            {
                Debug.Log("Bootstrup Loaded !!");
                var newStorage = new Storage
                {
                    fishCurrancy = new FishCurrancy(),
                    emeraldCurrancy = new EmeraldCurrancy(),
                    // audioSettings = new Runtime.StorageData.AudioSettings(),
                    userBioms = new UserBioms(),
                    // userSkins = new UserSkins(),
                    userBestDistance = new UserBestDistance()
                };

                // newStorage.audioSettings.volume = 0.1f;
                // Biome
                // Lol Kek :3 Default biome settings
                {
                    newStorage.userBioms.selectionBiomId = BiomeTypeID.GreenPlains;

                    newStorage.userBioms.biomeData = new List<BiomDatas>();

                    var biom1 = new BiomDatas
                    {
                        id = BiomeTypeID.GreenPlains,
                        isOpen = true
                    };
                    newStorage.userBioms.biomeData.Add(biom1);

                    var biom2 = new BiomDatas
                    {
                        id = BiomeTypeID.SnowyWastelands,
                        isOpen = false
                    };
                    newStorage.userBioms.biomeData.Add(biom2);
                }

                if (YandexGame.savesData == null)
                    Debug.Log("WARNING! YANDEX GAME EQUALS NULL!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

                YandexGame.savesData.storage = newStorage;
            }
            else
            {
                Debug.Log("Bootstrup Loaded FAILURE!!! DATA NOT NULL!!!");
            }
        }

        public void Exit()
        {
        }

        private void PrepareServices()
        {
            // Debug.Log($"YandexGame.SDKEnabled == {YandexGame.SDKEnabled}");
            // Load Game Save Data
            // persistenProgressService.Init();

            // ** Localozation ** //
            // LocalizationManager.Read();

            // ** Tweens ** //
            DOTween.Init();

            // ** Static Data ** //
            staticData.Initialize();

            // ** Curtain ** //
            curtain.Init();
            curtain.ShowCurtain(false);

            // Debug.Log($"YandexGame.SDKEnabled == {YandexGame.SDKEnabled}");
        }

        private void OnSceneLoaded()
        {
            gameStateMachine.EnterState<LoadMaiMenuState>();
        }
    }
}