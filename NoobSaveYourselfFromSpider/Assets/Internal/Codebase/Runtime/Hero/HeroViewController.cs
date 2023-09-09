// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using UnityEngine;

namespace Internal.Codebase.Runtime.Hero
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