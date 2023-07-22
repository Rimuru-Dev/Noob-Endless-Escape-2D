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
using Internal.Codebase.Infrastructure.Services.StaticData;
using Internal.Codebase.Runtime.Obstacles;
using Internal.Codebase.Runtime.SpriteTextNumberCounterLogic;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Runtime.GameplayScene
{
    public sealed class SceneController : MonoBehaviour
    {
        public NumberVisualizer numberVisualizer;

        [Inject] private IPersistenProgressService persistenProgressService;
        [Inject] private IStaticDataService staticDataService;
        [Inject] private ICurtainService curtainService;

        private Hero.HeroViewController heroViewController;

        public List<DeadlyObstacle> obstacles = new();

        public void Container(Hero.HeroViewController heroViewController)
        {
            this.heroViewController = heroViewController;
            this.heroViewController.heroDie.OnDie += Die;
        }

        public void Die()
        {
            heroViewController.HeroSpriteRenderer.color = Color.red;
            numberVisualizer.IsPause = true;
            heroViewController.jumpController.IsCanJump = false;
            Time.timeScale = 0;
            var storage = persistenProgressService.GetStoragesData();
            var bestDistance = storage.BestDistance;
            var currentDistance = numberVisualizer.currentNumber;

            if (currentDistance > bestDistance)
            {
                storage.BestDistance = currentDistance;

                persistenProgressService.Save(storage);
            }

            obstacles = new List<DeadlyObstacle>();
            foreach (var deadlyObstacle in FindObjectsOfType<DeadlyObstacle>(true))
                obstacles.Add(deadlyObstacle);
        }

        public void Rebirth()
        {
            foreach (var obstacle in obstacles.Where(obstacle => obstacle != null))
                obstacle.gameObject.SetActive(false);

            obstacles.Clear();
            
            heroViewController.HeroSpriteRenderer.color = Color.white;
            heroViewController.jumpController.IsCanJump = true;
            numberVisualizer.IsPause = false;

            heroViewController.transform.position = new Vector3(0, 5f, 0);

            Time.timeScale = 1;
        }

        private void Update()
        {
            if (Input.GetKey(key: KeyCode.P))
                Rebirth();
        }

        private void OnDestroy()
        {
            this.heroViewController.heroDie.OnDie -= Die;
        }
    }
}