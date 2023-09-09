// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using System.Collections.Generic;
using System.Linq;
using Internal.Codebase.Infrastructure.Services.CloudSave;
using Internal.Codebase.Infrastructure.Services.StaticData;
using Internal.Codebase.Runtime.GameplayScene.Hero;
using Internal.Codebase.Runtime.GameplayScene.LevelGeneration.Configs;
using Internal.Codebase.Runtime.GameplayScene.LevelGeneration.Handlers;
using Internal.Codebase.Runtime.GameplayScene.Obstacles;
using Internal.Codebase.Runtime.GameplayScene.Timer;
using Internal.Codebase.Runtime.General.SpriteTextNumberCounterLogic;
using Internal.Codebase.Runtime.General.StorageData;
using UnityEngine;
using UnityEngine.UI;
using YG;
using Zenject;

namespace Internal.Codebase.Runtime.GameplayScene.LevelController
{
    public sealed class SceneController : MonoBehaviour
    {
        public Button startPause;
        public Button stopPause;
        public Button home;

        public NumberVisualizer numberVisualizer;
        public GameTimer advTimer;

        // Popup
        public GameObject popup;
        public Button goMainMenuButton;
        public Button rebirthButton;
        public SpriteRenderer[] parallax;

        [Inject] private IStaticDataService staticDataService;
        private HeroViewController heroViewController;
        public List<DeadlyObstacle> obstacles = new();
        private EndlessLevelGenerationHandler endlessLevelGenerationHandler;

        public void Container(
            HeroViewController heroView,
            Action action,
            EndlessLevelGenerationHandler levelGeneration,
            IYandexSaveService yandexSaveService)
        {
            saveService = yandexSaveService;

            endlessLevelGenerationHandler = levelGeneration;
            heroViewController = heroView;
            heroViewController.heroDie.OnDie += Die;

            Load();

            advTimer.OnTimerOn += StartTimer;
            advTimer.OnTimerOff += EndTimer;
            goMainMenuButton.onClick.AddListener(() =>
            {
                Time.timeScale = 1;
                //sceneLoaderService.LoadScene(SceneName.Menu, () => { stateMachine.EnterState<LoadMainMenuState>(); });
                levelGeneration.Pause = true;
                heroView.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                heroView.jumpController.IsCanJump = false;
                action?.Invoke();
            });

            rebirthButton.onClick.AddListener(Reb);

            yandexGameSDK = FindObjectOfType<YandexGame>(true);

            startPause.onClick.AddListener(() => { Time.timeScale = 0; });
            stopPause.onClick.AddListener(() => { Time.timeScale = 1; });
            home.onClick.AddListener(() =>
            {
                Time.timeScale = 1;
                //sceneLoaderService.LoadScene(SceneName.Menu, () => { stateMachine.EnterState<LoadMainMenuState>(); });
                levelGeneration.Pause = true;
                heroView.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                heroView.jumpController.IsCanJump = false;
                action?.Invoke();
            });

            // set parallax
            {
                var config = YandexGame.savesData.storage;

                var selectionBiom = config.userBioms.selectionBiomId;

                var biom = selectionBiom switch
                {
                    BiomeTypeID.GreenPlains => staticDataService.GreenPlains,
                    BiomeTypeID.SnowyWastelands => staticDataService.SnowyWastelands,
                    _ => staticDataService.GreenPlains
                };

                foreach (var sriteRenderer in parallax)
                    sriteRenderer.sprite = biom.background;
            }
        }

        private void StartTimer()
        {
        }

        private void EndTimer()
        {
            endlessLevelGenerationHandler.Pause = false;
            Rebirth();
        }

        public YandexGame yandexGameSDK;

        private void Save() =>
            saveService.Save(storage);

        private void Load() =>
            storage = saveService.Load();

        private void OnEnable() =>
            YandexGame.RewardVideoEvent += Rewarded;

        public int rebirdthID = 50;
        private Storage storage;
        private IYandexSaveService saveService;

        private void Rewarded(int id)
        {
            if (id == 50)
            {
                AudioListener.volume = storage.audioSettings.volume;
                endlessLevelGenerationHandler.Pause = true;
                Time.timeScale = 1;
                popup.SetActive(false);
                advTimer.StartCountdown();
            }
        }

        private void ShowAdvButton(int id)
        {
            AudioListener.volume = 0;
            yandexGameSDK._RewardedShow(id);
        }

        private void OnDisable() =>
            YandexGame.RewardVideoEvent -= Rewarded;

        private void Reb() =>
            ShowAdvButton(50);

        private void Die()
        {
            home.gameObject.SetActive(false);
            stopPause.gameObject.SetActive(false);
            startPause.gameObject.SetActive(false);

            popup.SetActive(true);
            heroViewController.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            heroViewController.HeroSpriteRenderer.color = Color.red;
            numberVisualizer.IsPause = true;
            heroViewController.jumpController.IsCanJump = false;

            var bestDistance = storage.BestDistance;
            var currentDistance = numberVisualizer.currentNumber;

            if (currentDistance > bestDistance)
            {
                storage.BestDistance = currentDistance;

                YandexGame.savesData.storage = storage;
            }


            obstacles = new List<DeadlyObstacle>();
            foreach (var prefab in FindObjectsOfType<DeadlyObstacle>(true))
            {
                if (prefab.DeadlyObstacleTypeID == DeadlyObstacleTypeID.Trap)
                    obstacles.Add(prefab);
            }

            Time.timeScale = 0;
        }

        private void Rebirth()
        {
            foreach (var obstacle in obstacles.Where(obstacle => obstacle != null))
                obstacle.transform.parent.gameObject.SetActive(false);

            obstacles.Clear();
            heroViewController.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            heroViewController.HeroSpriteRenderer.color = Color.white;
            heroViewController.jumpController.IsCanJump = true;
            numberVisualizer.IsPause = false;

            heroViewController.transform.position = new Vector3(0, 6f, 0);

            startPause.gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            Save();

            advTimer.OnTimerOn -= StartTimer;
            advTimer.OnTimerOff -= EndTimer;
            goMainMenuButton.onClick.RemoveAllListeners();
            rebirthButton.onClick.RemoveAllListeners();

            if (heroViewController != null)
                heroViewController.heroDie.OnDie -= Die;

            startPause.onClick.RemoveAllListeners();
            stopPause.onClick.RemoveAllListeners();
            home.onClick.RemoveAllListeners();
        }
    }
}