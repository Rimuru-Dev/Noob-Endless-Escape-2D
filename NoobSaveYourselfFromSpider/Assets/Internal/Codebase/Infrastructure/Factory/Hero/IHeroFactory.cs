// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Internal.Codebase.Runtime.GameplayScene.Hero;
using Internal.Codebase.Runtime.GameplayScene.Hero.Controller;
using UnityEngine;

namespace Internal.Codebase.Infrastructure.Factory.Hero
{
    public interface IHeroFactory
    {
        public GameObject Hero { get; }
        public HeroViewController CreateHero();
        public void CreateHeroCamera();
    }
}