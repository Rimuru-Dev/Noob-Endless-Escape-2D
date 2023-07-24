// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using System.Collections.Generic;
using System.Linq;
using Internal.Codebase.Infrastructure.Services.Curtain;
using Internal.Codebase.Infrastructure.Services.PersistenProgress;
using Internal.Codebase.Infrastructure.Services.SceneLoader;
using Internal.Codebase.Infrastructure.Services.StaticData;
using Internal.Codebase.Infrastructure.StateMachine;
using Internal.Codebase.Runtime.EndlessLevelGenerationSolution.Configs;
using Internal.Codebase.Runtime.EndlessLevelGenerationSolution.Handlers;
using Internal.Codebase.Runtime.EndlessLevelGenerationSolution.PrefabHelper;
using Internal.Codebase.Runtime.Hero;
using Internal.Codebase.Runtime.MainMenu.Animation;
using Internal.Codebase.Runtime.Obstacles;
using Internal.Codebase.Runtime.SpriteTextNumberCounterLogic;
using Internal.Codebase.Runtime.StorageData;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using YG;
using Zenject;

namespace Internal.Codebase.Runtime.GameplayScene
{
    public sealed class SceneController : MonoBehaviour, IFuckingSaveLoad
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

        [Inject] private IPersistenProgressService persistenProgressService;
        [Inject] private IStaticDataService staticDataService;
        [Inject] private ICurtainService curtainService;
        private GameStateMachine gameStateMachine;
        private Hero.HeroViewController heroViewController;
        private ISceneLoaderService sceneLoader;
        public List<DeadlyObstacle> obstacles = new();
        private EndlessLevelGenerationHandler endlessLevelGenerationHandler;

        public void Container(
            HeroViewController heroViewController,
            GameStateMachine gameStateMachine,
            ISceneLoaderService sceneLoader,
            Action action,
            EndlessLevelGenerationHandler endlessLevelGenerationHandler,
            Storage storage)
        {
            // this.storage = storage;
            this.endlessLevelGenerationHandler = endlessLevelGenerationHandler;
            this.gameStateMachine = gameStateMachine;
            this.heroViewController = heroViewController;
            this.heroViewController.heroDie.OnDie += Die;
            this.sceneLoader = sceneLoader;


            advTimer.OnTimerOn += StartTimer;
            advTimer.OnTimerOff += EndTimer;
            goMainMenuButton.onClick.AddListener(() =>
            {
                Time.timeScale = 1;
                //sceneLoader.LoadScene(SceneName.Menu, () => { gameStateMachine.EnterState<LoadMaiMenuState>(); });
                endlessLevelGenerationHandler.Pause = true;
                heroViewController.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                heroViewController.jumpController.IsCanJump = false;
                action?.Invoke();
            });

            rebirthButton.onClick.AddListener(Reb);

            yandexGameSDK = FindObjectOfType<YandexGame>(true);

            startPause.onClick.AddListener(() => { Time.timeScale = 0; });
            stopPause.onClick.AddListener(() => { Time.timeScale = 1; });
            home.onClick.AddListener(() =>
            {
                Time.timeScale = 1;
                //sceneLoader.LoadScene(SceneName.Menu, () => { gameStateMachine.EnterState<LoadMaiMenuState>(); });
                endlessLevelGenerationHandler.Pause = true;
                heroViewController.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                heroViewController.jumpController.IsCanJump = false;
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

        private void Awake()
        {
            if (YandexGame.SDKEnabled)
                Load();

            YandexGame.GetDataEvent += Load;
        }

        public void Save()
        {
            YandexGame.savesData.storage = storage;
        }

        public void Load()
        {
            storage = YandexGame.savesData.storage;
        }

        private void OnEnable() =>
            YandexGame.RewardVideoEvent += Rewarded;

        public int rebirdthID = 50;
        private Storage storage;

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

        private void Reb()
        {
            ShowAdvButton(50);
        }

        public void Die()
        {
            home.gameObject.SetActive(false);
            stopPause.gameObject.SetActive(false);
            startPause.gameObject.SetActive(false);

            popup.SetActive(true);
            heroViewController.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            heroViewController.HeroSpriteRenderer.color = Color.red;
            numberVisualizer.IsPause = true;
            heroViewController.jumpController.IsCanJump = false;

            var storage = YandexGame.savesData.storage;
            var bestDistance = storage.BestDistance;
            var currentDistance = numberVisualizer.currentNumber;

            if (currentDistance > bestDistance)
            {
                storage.BestDistance = currentDistance;

                YandexGame.savesData.storage = storage;
                // persistenProgressService.Save(storage);
            }


            obstacles = new List<DeadlyObstacle>();
            foreach (var prefab in FindObjectsOfType<DeadlyObstacle>(true))
            {
                if (prefab.DeadlyObstacleTypeID == DeadlyObstacleTypeID.Trap)
                    obstacles.Add(prefab);
            }

            Time.timeScale = 0;
        }

        public void Rebirth()
        {
            foreach (var obstacle in obstacles.Where(obstacle => obstacle != null))
            {
                Debug.Log(obstacle.transform.parent.gameObject.name);
                obstacle.transform.parent.gameObject.SetActive(false);
            }

            obstacles.Clear();
            heroViewController.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            heroViewController.HeroSpriteRenderer.color = Color.white;
            heroViewController.jumpController.IsCanJump = true;
            numberVisualizer.IsPause = false;

            heroViewController.transform.position = new Vector3(0, 6f, 0);

            // home.gameObject.SetActive(true);
            // stopPause.gameObject.SetActive(true);
            startPause.gameObject.SetActive(true);
        }
#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKey(key: KeyCode.P))
                Rebirth();
        }
#endif
        private void OnDestroy()
        {
            advTimer.OnTimerOn -= StartTimer;
            advTimer.OnTimerOff -= EndTimer;
            YandexGame.GetDataEvent -= Load;
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