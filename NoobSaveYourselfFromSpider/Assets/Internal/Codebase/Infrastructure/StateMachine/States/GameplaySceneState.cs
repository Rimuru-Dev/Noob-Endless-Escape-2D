// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using Cinemachine;
using Internal.Codebase.Infrastructure.AssetManagement;
using Internal.Codebase.Infrastructure.Factory;
using Internal.Codebase.Infrastructure.Factory.Hero;
using Internal.Codebase.Infrastructure.Factory.UI;
using Internal.Codebase.Infrastructure.Services.Curtain;
using Internal.Codebase.Infrastructure.Services.SceneLoader;
using Internal.Codebase.Infrastructure.Services.StaticData;
using Internal.Codebase.Infrastructure.StateMachine.Interfaces;
using Internal.Codebase.Runtime.EndlessLevelGenerationSolution.Handlers;
using Internal.Codebase.Runtime.Hero;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Internal.Codebase.Infrastructure.StateMachine.States
{
    public sealed class GameplaySceneState : IStateNext
    {
        private readonly ICurtainService curtain;
        private readonly ISceneLoaderService sceneLoader;
        private readonly IStaticDataService staticData;
        private readonly IUIFactory uiFactory;
        private readonly IHeroFactory heroFactory;
        private readonly IGameFactory gameFactory;
        private IGameStateMachine gameStateMachine;

        [Inject]
        public GameplaySceneState(
            ICurtainService curtain,
            ISceneLoaderService sceneLoader,
            IStaticDataService staticData,
            IUIFactory uiFactory,
            IHeroFactory heroFactory,
            IGameFactory gameFactory)
        {
            this.curtain = curtain;
            this.sceneLoader = sceneLoader;
            this.staticData = staticData;
            this.uiFactory = uiFactory;
            this.heroFactory = heroFactory;
            this.gameFactory = gameFactory;
        }

        public void Init(IGameStateMachine gameStateMachine) =>
            this.gameStateMachine = gameStateMachine;

        public void Enter()
        {
            PrepareScene();

            // *** Hide Curtain *** //
            var config = staticData.ForCurtain();
            curtain.HideCurtain(config.HideDelay);
        }

        private void PrepareScene()
        {
            var hero = heroFactory.CreateHero();
            heroFactory.CreateHeroCamera();
            hero.transform.position = new Vector3(0, 5f, 0);

            var levelGenerator = gameFactory.CreateLevelGenerator();
            levelGenerator.Prepare();

            // var spawnPoint = levelGenerator.GetComponentInChildren<HeroSpawnPoint>();
            // hero.transform.position = spawnPoint != null 
            //     ? spawnPoint.transform.position 
            //     : new Vector3(0, 5f, 0); // default position
        }

        public void Exit()
        {
        }
    }
}