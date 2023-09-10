// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using Internal.Codebase.Runtime.GameplayScene.Hero.View;

namespace Internal.Codebase.Infrastructure.Factory.Hero
{
    public interface IHeroFactory : IDisposable
    {
        public HeroViewController Hero { get; }
        public HeroViewController CreateHero();
        public void CreateHeroCamera();
    }
}