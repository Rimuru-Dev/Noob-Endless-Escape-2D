// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;

namespace AbyssMoth.Internal.Codebase.Infrastructure.Services.Curtain
{
    public interface ICurtainService
    {
        public void Init();
        public void ShowCurtain(bool isAnimated = true, Action callback = null);
        public void HideCurtain(bool isAnimated = true, Action callback = null);
        public void HideCurtain(float startDelay, Action callback = null);
    }
}