﻿// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using UnityEngine;

namespace Internal.Codebase.Runtime.Hero
{
    public sealed class HeroDie : MonoBehaviour
    {
        public void ApplyHeroDie()
        {
            Debug.Log("Hero Die");
            Time.timeScale = 0;
        }
    }
}