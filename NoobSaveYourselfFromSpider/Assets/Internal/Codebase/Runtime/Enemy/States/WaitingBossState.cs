using System;
using Internal.Codebase.Runtime.Enemy.Configs;
using Internal.Codebase.Runtime.Enemy.ProgressBar;
using UnityEngine;

namespace Internal.Codebase.Runtime.Enemy
{
    public sealed class WaitingBossState : State
    {
        private readonly GameObject gameObject;
        private readonly EnemyStateMachine enemyStateMachine;
        private readonly BossConfig bossConfig;
        private readonly BossProgressBar bossProgressBar;
        private float currentTimer = 0f;

        public WaitingBossState(GameObject gameObject, EnemyStateMachine enemyStateMachine, BossConfig bossConfig,
            BossProgressBar bossProgressBar) :
            base(gameObject)
        {
            this.gameObject = gameObject;
            this.enemyStateMachine = enemyStateMachine;
            this.bossConfig = bossConfig;
            this.bossProgressBar = bossProgressBar;
        }

        public override void OnStateEnter()
        {
            currentTimer = 0;
           // bossProgressBar.SetFull();
            //  bossProgressBar.SetMaxValue(bossConfig.LifeTime + bossConfig.AttackTime);
            gameObject.transform.position = bossConfig.InitialPosition;
        }

        public override void OnStateExit() =>
            currentTimer = 0;

        public override Type Tick()
        {
            currentTimer += Time.deltaTime;

            //  Debug.Log(currentTimer);

            if (ReadyToBossAppearanceState())
                enemyStateMachine.SwitchState(typeof(ShowBossState));

            return null;
        }

        private bool ReadyToBossAppearanceState() =>
            currentTimer >= bossConfig.WaitTime;
    }
}