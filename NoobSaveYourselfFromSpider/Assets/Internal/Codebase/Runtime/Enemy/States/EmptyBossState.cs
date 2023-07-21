using System;
using Internal.Codebase.Runtime.Enemy.Configs;
using Internal.Codebase.Runtime.Enemy.ProgressBar;
using UnityEngine;

namespace Internal.Codebase.Runtime.Enemy
{
    public sealed class EmptyBossState : State
    {
        private readonly GameObject gameObject;
        private readonly BossProgressBar bossProgressBar;
        private readonly EnemyStateMachine enemyStateMachine;
        private readonly BossConfig bossConfig;

        public EmptyBossState(GameObject gameObject, BossProgressBar bossProgressBar,
            EnemyStateMachine enemyStateMachine, BossConfig bossConfig) : base(gameObject)
        {
            this.gameObject = gameObject;
            this.bossProgressBar = bossProgressBar;
            this.enemyStateMachine = enemyStateMachine;
            this.bossConfig = bossConfig;
        }

        public override Type Tick() => null;
    }
}