// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using UnityEngine;

namespace Internal.Codebase.Runtime.GameplayScene.Hero
{
    [DisallowMultipleComponent]
    public sealed class HeroDie : MonoBehaviour
    {
        public Action OnDie;

        public void ApplyHeroDie()
        {
            Debug.Log("Hero Die");
            
            OnDie?.Invoke();
        }
    }
}