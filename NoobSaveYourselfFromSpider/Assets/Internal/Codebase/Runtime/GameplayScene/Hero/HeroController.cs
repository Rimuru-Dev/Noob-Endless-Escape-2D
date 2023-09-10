// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Project: "Murders Drones Endless Way" 
//   Contact me: 
//          - Gmail:    rimuru.dev@gmail.com
//          - GitHub:   https://github.com/RimuruDev
//          - LinkedIn: https://www.linkedin.com/in/rimuru/
//          - GitHub Organizations: https://github.com/Rimuru-Dev
//
// **************************************************************** //

using System;
using Internal.Codebase.Runtime.GameplayScene.Hero.Death;
using Internal.Codebase.Runtime.GameplayScene.Hero.View;

namespace Internal.Codebase.Runtime.GameplayScene.Hero
{
    public sealed class HeroController : IDisposable
    {
        private readonly HeroViewController heroView;
        private readonly IHeroDeath heroDeath;

        public HeroController(HeroViewController heroView, IHeroDeath heroDeath)
        {
            this.heroView = heroView;
            this.heroDeath = heroDeath;

            Prepare();
        }

        ~HeroController() =>
            Dispose();

        public IHeroDeath HeroDeath => heroDeath;

        // Pass to the stage exit state of gameplay to be cleared.
        public void Dispose()
        {
            heroView.DeathObserver.OnCollisionEnter2D -= heroDeath.PerformDeath;
            heroDeath.Unsubscribe(OnHeroDeath);
            heroDeath?.Dispose();
        }

        private void Prepare()
        {
            heroView.DeathObserver.OnCollisionEnter2D += heroDeath.PerformDeath;
            heroDeath.Subscribe(OnHeroDeath);
        }

        private void OnHeroDeath() => 
            heroView.JumpController.IsCanJump = false;
    }
}