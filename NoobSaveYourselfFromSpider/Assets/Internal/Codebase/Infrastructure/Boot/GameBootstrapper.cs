// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Zenject;
using UnityEngine;
using System.Diagnostics.CodeAnalysis;
using Internal.Codebase.Infrastructure.StateMachine;
using Internal.Codebase.Infrastructure.StateMachine.States;
using Internal.Codebase.Infrastructure.Services.ActionUpdater;

namespace Internal.Codebase.Infrastructure.Boot
{
    [SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
    public sealed class GameBootstrapper : MonoBehaviour
    {
        private GameStateMachine gameStateMachine;
        private IActionUpdaterService actionUpdaterService;

        [Inject]
        public void Constructor(GameStateMachine stateMachine, IActionUpdaterService actionUpdater)
        {
            gameStateMachine = stateMachine;
            actionUpdaterService = actionUpdater;
        }

        private void Awake() =>
            Initialize();

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

        private void FixedUpdate() =>
            actionUpdaterService.FixedUpdate();

        private void Update() =>
            actionUpdaterService.Update();

        private void LateUpdate() =>
            actionUpdaterService.LateUpdate();
    }
}