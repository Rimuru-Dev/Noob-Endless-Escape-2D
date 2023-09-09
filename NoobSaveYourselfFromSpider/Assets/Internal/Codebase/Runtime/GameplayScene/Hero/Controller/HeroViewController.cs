// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Internal.Codebase.Runtime.GameplayScene.Hero.Death;
using Internal.Codebase.Runtime.GameplayScene.Hero.Input;
using UnityEngine;

namespace Internal.Codebase.Runtime.GameplayScene.Hero.Controller
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public sealed class HeroViewController : MonoBehaviour
    {
        [field: SerializeField] public SpriteRenderer HeroSpriteRenderer { get; private set; }

        [SerializeField] public JumpController jumpController;
        public HeroDie heroDie;
    }
}