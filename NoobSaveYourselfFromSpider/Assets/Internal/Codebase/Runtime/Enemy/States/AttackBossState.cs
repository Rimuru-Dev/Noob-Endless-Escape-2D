using System;
using System.Collections;
using Internal.Codebase.Infrastructure.Services.CoroutineRunner;
using Internal.Codebase.Runtime.Enemy.Base;
using Internal.Codebase.Runtime.Enemy.Configs;
using Internal.Codebase.Runtime.Enemy.ProgressBar;
using UnityEngine;
using Object = UnityEngine.Object;
using State = Internal.Codebase.Runtime.Enemy.Base.State;

namespace Internal.Codebase.Runtime.Enemy.States
{
    public sealed class AttackBossState : State
    {
        private readonly GameObject gameObject;
        private readonly EnemyStateMachine enemyStateMachine;
        private readonly BossConfig bossConfig;
        private readonly ICoroutineRunner coroutineRunner;
        private readonly BossProgressBar progressBar;
        private Hero.HeroViewController cahedHeroViewController;

        public AttackBossState(GameObject gameObject, EnemyStateMachine enemyStateMachine, BossConfig bossConfig,
            ICoroutineRunner coroutineRunner, BossProgressBar progressBar) : base(gameObject)
        {
            this.gameObject = gameObject;
            this.enemyStateMachine = enemyStateMachine;
            this.bossConfig = bossConfig;
            this.coroutineRunner = coroutineRunner;
            this.progressBar = progressBar;
        }

        public override void OnStateEnter()
        {
            coroutineRunner.StartCoroutine(Attack());

            if (cahedHeroViewController == null)
                cahedHeroViewController = Object.FindObjectOfType<Hero.HeroViewController>();
        }

        private float timeAttack = 0;

        private IEnumerator Attack()
        {
            while (timeAttack < bossConfig.AttackTime)
            {
                Debug.Log($"{timeAttack} < {bossConfig.AttackTime}");
                timeAttack += Time.deltaTime;
                // Создание пули и задание ей скорости и направления
                GameObject bullet = GameObject.Instantiate(bossConfig.Bullet, gameObject.transform.position,
                    Quaternion.identity);
                // Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
                // ..bullet.transform.Translate(gameObject.transform.right *(bossConfig.AttackSpeed * Time.deltaTime));
                //  bulletRigidbody.velocity = gameObject.transform.right * bossConfig.AttackSpeed;
                progressBar.SetValue(timeAttack);
                yield return new WaitForSeconds(bossConfig.AttackCooldown); // Ожидание 3 секунды между выстрелами
                timeAttack += bossConfig.AttackCooldown;
                progressBar.SetValue(timeAttack);
            }

            timeAttack = 0;
            enemyStateMachine.SwitchState(typeof(GoingBackState)); // end attack
        }

        private void SyncPositionY()
        {
            if (cahedHeroViewController != null)
            {
                Vector3 targetPosition = gameObject.transform.position;
                targetPosition.y = cahedHeroViewController.transform.position.y;
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, targetPosition,
                    Time.deltaTime * bossConfig.SyncSpeed);
            }
        }

        public override Type Tick()
        {
            Debug.Log("AttackBossState");

            SyncPositionY();

            return null;
        }
    }
}