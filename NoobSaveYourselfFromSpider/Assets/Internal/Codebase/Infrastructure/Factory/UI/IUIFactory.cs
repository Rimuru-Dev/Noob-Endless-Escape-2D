// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Internal.Codebase.Runtime.Curtain;
using UnityEngine;

namespace Internal.Codebase.Infrastructure.Factory.UI
{
    public interface IUIFactory
    {
        public Transform CreateRoot();
        public CurtainView CreateCurtain();
    }
}