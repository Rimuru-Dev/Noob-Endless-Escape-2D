using System;
using System.Collections;
using Internal.Codebase.Infrastructure.Services.CoroutineRunner;
using Internal.Codebase.Runtime.Enemy.Base;
using Internal.Codebase.Runtime.Enemy.Configs;
using Internal.Codebase.Runtime.Enemy.ProgressBar;
using UnityEngine;

namespace Internal.Codebase.Runtime.Enemy.States
{
    public sealed class ShowBossState : State
    {
        private readonly GameObject gameObject;
        private readonly BossProgressBar bossProgressBar;
        private readonly EnemyStateMachine enemyStateMachine;
        private readonly ICoroutineRunner coroutineRunner;
        private readonly BossConfig bossConfig;

        private Vector3 targetPosition;

        public ShowBossState(GameObject gameObject,
            BossProgressBar bossProgressBar,
            EnemyStateMachine enemyStateMachine,
            ICoroutineRunner coroutineRunner, BossConfig bossConfig) : base(gameObject)
        {
            this.gameObject = gameObject;
            this.bossProgressBar = bossProgressBar;
            this.enemyStateMachine = enemyStateMachine;
            this.coroutineRunner = coroutineRunner;
            this.bossConfig = bossConfig;

            if (this.coroutineRunner == null)
                Debug.Log("NULLLLLLLLLLLLLL");
        }

        public override Type Tick()
        {
            Debug.Log("ShowBossState");

            return null;
        }

        public override void OnStateEnter()
        {
            targetPosition = bossConfig.AppearancePosition;
            coroutineRunner.StartCoroutine(Appearance());

            bossProgressBar.CanvasGroup.alpha = 0;
            bossProgressBar.Root.SetActive(true);
         //   bossProgressBar.SetFull();
            coroutineRunner.StartCoroutine(bossProgressBar.ProgressBar(bossConfig.LifeTime + bossConfig.AttackTime));
           // bossProgressBar.SetMaxValue(bossConfig.LifeTime + bossConfig.AttackTime);
            coroutineRunner.StartCoroutine(ShowProgressBar());
        }

        private IEnumerator ShowProgressBar()
        {
            while (bossProgressBar.CanvasGroup.alpha < 1)
            {
                bossProgressBar.CanvasGroup.alpha += 0.07f;

                yield return new WaitForSeconds(0.1f);
            }
        }

        private IEnumerator Appearance()
        {
            while (Vector3.Distance(gameObject.transform.position, targetPosition) > 0.01f)
            {
                gameObject.transform.position =
                    Vector3.MoveTowards(
                        gameObject.transform.position,
                        targetPosition,
                        bossConfig.MoveSpeed * Time.deltaTime);

              //  bossProgressBar.SetValue(Time.deltaTime);
                
                yield return null;
            }

            enemyStateMachine.SwitchState(typeof(AttackBossState));
        }
    }
}