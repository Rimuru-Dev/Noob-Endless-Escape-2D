// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System.Diagnostics.CodeAnalysis;
using Internal.Codebase.Infrastructure.StateMachine;
using Internal.Codebase.Infrastructure.StateMachine.States;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Infrastructure.Boot
{
    [SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
    public sealed class GameBootstrapper : MonoBehaviour
    {
        private GameStateMachine gameStateMachine;

        private void Awake()
        {
            Initialize();
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

            DontDestroyOnLoad(gameObject);
        }

        private void EnterBootstrapState()
        {
            gameStateMachine.Init();
            gameStateMachine.EnterState<BootstrapState>();
        }
    }
}