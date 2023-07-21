using System;
using System.Collections;
using Internal.Codebase.Infrastructure.Services.CoroutineRunner;
using Internal.Codebase.Runtime.Enemy.Configs;
using Internal.Codebase.Runtime.Enemy.ProgressBar;
using UnityEngine;

namespace Internal.Codebase.Runtime.Enemy
{
    [Serializable]
    public sealed class GoingBackState : State
    {
        private readonly GameObject bossView;
        private readonly EnemyStateMachine stateMachine;
        private readonly BossConfig bossConfig;
        private readonly ICoroutineRunner coroutineRunner;
        private readonly BossProgressBar progressBar;

        public GoingBackState(GameObject bossView, EnemyStateMachine stateMachine, BossConfig bossConfig,
            ICoroutineRunner coroutineRunner, BossProgressBar progressBar) :
            base(bossView)
        {
            this.bossView = bossView;
            this.stateMachine = stateMachine;
            this.bossConfig = bossConfig;
            this.coroutineRunner = coroutineRunner;
            this.progressBar = progressBar;
        }

        public override void OnStateEnter()
        {
            coroutineRunner.StartCoroutine(FlyOutAnimation());
            coroutineRunner.StartCoroutine(ShowProgressBar());
        }

        private IEnumerator ShowProgressBar()
        {
            progressBar.SetFull();
            while (progressBar.CanvasGroup.alpha > 0)
            {
                progressBar.CanvasGroup.alpha -= 0.2f;

                yield return new WaitForSeconds(0.1f);
            }
        }


        private IEnumerator FlyOutAnimation()
        {
            var flyTimeLeft = bossConfig.FlyOutDuration;
            var flyStartPosition = bossView.transform.position;

            while (flyTimeLeft > 0f)
            {
                var t = 1f - flyTimeLeft / bossConfig.FlyOutDuration;

                bossView.transform.position =
                    Vector3.Lerp(flyStartPosition, flyStartPosition + Vector3.right * bossConfig.FlyOutDistance, t);

                flyTimeLeft -= Time.deltaTime;

                yield return null;
            }

            stateMachine.SwitchState(typeof(WaitingBossState));
        }

        public override Type Tick()
        {
            Debug.Log("Go back");
            return null;
        }
    }
}