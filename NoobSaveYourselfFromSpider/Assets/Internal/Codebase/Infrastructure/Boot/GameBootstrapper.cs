// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Internal.Codebase.Infrastructure.Services.PersistenProgress;
using Internal.Codebase.Infrastructure.Services.StaticData;
using Internal.Codebase.Infrastructure.StateMachine;
using Internal.Codebase.Infrastructure.StateMachine.States;
using Internal.Codebase.Runtime.MainMenu.Animation;
using UnityEngine;
using YG;
using Zenject;

namespace Internal.Codebase.Infrastructure.Boot
{
    [SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
    public sealed class GameBootstrapper : MonoBehaviour
    {
        private GameStateMachine gameStateMachine;

        [Inject] public IPersistenProgressService PersistenProgressService;
        [Inject] public IStaticDataService StaticDataService;

        public static GameBootstrapper Instance;

        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            StartCoroutine(AutoSave());
        }

        private IEnumerator AutoSave()
        {
            while (true)
            {
                yield return new WaitForSeconds(5f);

                var f = GameObject.FindObjectsOfType<GameObject>(true);

                foreach (var c in f.Where(x => x != null))
                {
                    var s = c.GetComponent<IFuckingSaveLoad>();
                    if (s != null)
                    {
                        // Debug.Log(c.name);
                        s?.Save();
                    }
                }

                YandexGame.SaveProgress();
            }
        }

        private void OnApplicationQuit()
        {
            var f = GameObject.FindObjectsOfType<GameObject>(true);

            foreach (var c in f.Where(x => x != null))
            {
                var s = c.GetComponent<IFuckingSaveLoad>();
                if (s != null)
                {
                    Debug.Log(c.name);
                    s?.Save();
                }
            }

            YandexGame.SaveProgress();
        }

        private void OnDestroy()
        {
            OnApplicationQuit();
        }

        [Inject]
        public void Constructor(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        private void Initialize()
        {
            if (Exist())
            {
                Destroy(gameObject);
                return;
            }

            ApplyDontDestroyOnLoad();

            EnterBootstrapState();
        }

        private bool Exist()
        {
            var bootstrupper = FindObjectOfType<GameBootstrapper>();

            return bootstrupper is not null && bootstrupper != this;
        }

        private void ApplyDontDestroyOnLoad()
        {
            transform.SetParent(null);
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void EnterBootstrapState()
        {
            gameStateMachine.Init();
            gameStateMachine.EnterState<BootstrapState>();
        }
    }
}