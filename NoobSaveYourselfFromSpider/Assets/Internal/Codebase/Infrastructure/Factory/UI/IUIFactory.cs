// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using AbyssMoth.Internal.Codebase.Runtime.Curtain;
using UnityEngine;

namespace AbyssMoth.Internal.Codebase.Infrastructure.Factory.UI
{
    public interface IUIFactory
    {
        public Transform CreateRoot();
        public CurtainView CreateCurtain();
    }
}