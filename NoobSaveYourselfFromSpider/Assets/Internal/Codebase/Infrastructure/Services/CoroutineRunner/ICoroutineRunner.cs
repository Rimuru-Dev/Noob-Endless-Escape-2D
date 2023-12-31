﻿// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System.Collections;
using UnityEngine;

namespace Internal.Codebase.Infrastructure.Services.CoroutineRunner
{
    public interface ICoroutineRunner
    {
        public Coroutine StartCoroutine(IEnumerator coroutine);

        public void StopAllCoroutines();

        public void StopCoroutine(Coroutine coroutine);
    }
}