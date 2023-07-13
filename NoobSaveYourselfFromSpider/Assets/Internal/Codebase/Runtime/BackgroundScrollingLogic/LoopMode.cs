// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//   Gist: https://gist.github.com/RimuruDev/bd6ce71565e49d8cdefc5631e8d6ecf9
//
// **************************************************************** //

using System;

namespace Internal.Codebase.Runtime.BackgroundScrollingLogic
{
    [Serializable]
    public enum LoopMode
    {
        Update = 0,
        FixedUpdate = 1,
        LateUpdate = 2,
    }
}