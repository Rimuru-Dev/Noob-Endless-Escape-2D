// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using Internal.Codebase.Infrastructure.Factory.UI;
using Internal.Codebase.Runtime.General.Curtain;
using Zenject;

namespace Internal.Codebase.Infrastructure.Services.Curtain
{
    public sealed class CurtainService : ICurtainService
    {
        private readonly IUIFactory uiFactory;
        private CurtainView curtainView;

        [Inject]
        public CurtainService(IUIFactory uiFactory)
        {
            this.uiFactory = uiFactory;
        }

        public void Init()
        {
            curtainView = uiFactory.CreateCurtain();
        }

        public void ShowCurtain(bool isAnimated = true, Action callback = null)
        {
            curtainView.ShowCurtain(isAnimated, callback);
        }

        public void HideCurtain(bool isAnimated = true, Action callback = null)
        {
            curtainView.HideCurtain(isAnimated, callback);
        }

        public void HideCurtain(float startDelay, Action callback = null)
        {
            curtainView.HideCurtain(startDelay, callback);
        }
    }
}