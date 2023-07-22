// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using UnityEngine;

namespace Internal.Codebase.Infrastructure.Factory.Hero
{
    public interface IHeroFactory
    {
        public GameObject Hero { get; }
        public Runtime.Hero.HeroViewController CreateHero();
        public void CreateHeroCamera();
    }
}