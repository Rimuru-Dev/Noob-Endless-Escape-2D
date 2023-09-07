using System;
using System.Collections.Generic;
using Internal.Codebase.Infrastructure.Services.CoroutineRunner;
using Internal.Codebase.Infrastructure.Services.StaticData;
using Internal.Codebase.Runtime.Enemy.Base;
using Internal.Codebase.Runtime.Enemy.Configs;
using Internal.Codebase.Runtime.Enemy.ProgressBar;
using Internal.Codebase.Runtime.Enemy.States;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Runtime.Enemy.View
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public sealed class EnemyStateMachineView : MonoBehaviour
    {
        [SerializeField] private BossProgressBar progressBar;
        [SerializeField] private BossConfig bossConfig;

        private EnemyStateMachine stateMachine;
        private Dictionary<Type, State> states;

        private IStaticDataService staticDataService;
        private ICoroutineRunner coroutineRunner;

        [Inject]
        public void Constructor(
            IStaticDataService staticDataService,
            ICoroutineRunner coroutineRunner)
        {
            this.staticDataService = staticDataService;
            this.coroutineRunner = coroutineRunner;
        }

        public void Start()
        {
            stateMachine = new EnemyStateMachine();

            var bossView = gameObject;
            
            states = new Dictionary<Type, State>
            {
                // TODO: bossConfig move in StaticDataService
                { typeof(WaitingBossState), new WaitingBossState(bossView, stateMachine,bossConfig, progressBar) },
                { typeof(ShowBossState), new ShowBossState(bossView, progressBar, stateMachine, coroutineRunner,bossConfig) },
                { typeof(HideBossState), new HideBossState(bossView, progressBar, stateMachine,bossConfig) },
                { typeof(AttackBossState), new AttackBossState(bossView, stateMachine,bossConfig, coroutineRunner, progressBar) },
                { typeof(GoingBackState), new GoingBackState (bossView, stateMachine,bossConfig, coroutineRunner, progressBar) },
                { typeof(EmptyBossState), new EmptyBossState (bossView,progressBar,stateMachine,bossConfig ) },
            };

            stateMachine.SetStatus(states);
            stateMachine.SetInitialState(typeof(WaitingBossState));
            stateMachine.SwitchState(typeof(WaitingBossState));
        }

        private void Update() =>
            stateMachine.OnUpdate();
    }
}