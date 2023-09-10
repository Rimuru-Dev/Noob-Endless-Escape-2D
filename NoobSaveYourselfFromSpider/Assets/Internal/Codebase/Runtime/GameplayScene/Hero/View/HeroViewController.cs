// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Internal.Codebase.Runtime.GameplayScene.Hero.Input;
using Internal.Codebase.Runtime.General.TriggerObservers;
using UnityEngine;

namespace Internal.Codebase.Runtime.GameplayScene.Hero.View
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public sealed class HeroViewController : MonoBehaviour
    {
        [field: SerializeField] public Transform HeroRoot { get; private set; }
        [field: SerializeField] public SpriteRenderer HeroSpriteRenderer { get; private set; }
        [field: SerializeField] public JumpController JumpController { get; private set; }
        // [field: SerializeField] public HeroDie HeroDie { get; private set; }
        [field: SerializeField] public TriggerObserver2D DeathObserver { get; private set; }
    }
}