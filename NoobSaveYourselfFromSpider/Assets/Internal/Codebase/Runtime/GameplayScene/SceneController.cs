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
using Internal.Codebase.Runtime.EndlessLevelGenerationSolution.Handlers;
using Internal.Codebase.Runtime.EndlessLevelGenerationSolution.PrefabHelper;
using Internal.Codebase.Runtime.Hero;
using Internal.Codebase.Runtime.Obstacles;
using Internal.Codebase.Runtime.SpriteTextNumberCounterLogic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using YG;
using Zenject;

namespace Internal.Codebase.Runtime.GameplayScene
{
    public sealed class SceneController : MonoBehaviour
    {
        public NumberVisualizer numberVisualizer;
        public GameTimer advTimer;

        // Popup
        public GameObject popup;
        public Button goMainMenuButton;
        public Button rebirthButton;


        [Inject] private IPersistenProgressService persistenProgressService;
        [Inject] private IStaticDataService staticDataService;
        [Inject] private ICurtainService curtainService;
        private GameStateMachine gameStateMachine;
        private Hero.HeroViewController heroViewController;
        private ISceneLoaderService sceneLoader;
        public List<DeadlyObstacle> obstacles = new();
        private EndlessLevelGenerationHandler endlessLevelGenerationHandler;

        public void Container(HeroViewController heroViewController,
            GameStateMachine gameStateMachine,
            ISceneLoaderService sceneLoader, Action action, EndlessLevelGenerationHandler endlessLevelGenerationHandler)
        {
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

        private void OnEnable() =>
            YandexGame.RewardVideoEvent += Rewarded;

        public int rebirdthID = 50;

        private void Rewarded(int id)
        {
            if (id == 50)
            {
                endlessLevelGenerationHandler.Pause = true;
                Time.timeScale = 1;
                popup.SetActive(false);
                advTimer.StartCountdown();
            }
        }

        private void ShowAdvButton(int id) =>
            yandexGameSDK._RewardedShow(id);

        private void OnDisable() =>
            YandexGame.RewardVideoEvent -= Rewarded;

        private void Reb()
        {
            ShowAdvButton(50);
        }

        public void Die()
        {
            popup.SetActive(true);
            heroViewController.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            heroViewController.HeroSpriteRenderer.color = Color.red;
            numberVisualizer.IsPause = true;
            heroViewController.jumpController.IsCanJump = false;

            var storage = persistenProgressService.GetStoragesData();
            var bestDistance = storage.BestDistance;
            var currentDistance = numberVisualizer.currentNumber;

            if (currentDistance > bestDistance)
            {
                storage.BestDistance = currentDistance;

                persistenProgressService.Save(storage);
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

            heroViewController.transform.position = new Vector3(0, 5f, 0);
        }

        private void Update()
        {
            if (Input.GetKey(key: KeyCode.P))
                Rebirth();
        }

        private void OnDestroy()
        {
            advTimer.OnTimerOn -= StartTimer;
            advTimer.OnTimerOff -= EndTimer;

            goMainMenuButton.onClick.RemoveAllListeners();
            rebirthButton.onClick.RemoveAllListeners();

            if (heroViewController != null)
                heroViewController.heroDie.OnDie -= Die;
        }
    }
}