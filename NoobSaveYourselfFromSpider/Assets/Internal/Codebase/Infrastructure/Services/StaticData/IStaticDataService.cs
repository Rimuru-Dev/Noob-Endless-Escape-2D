// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using AbyssMoth.Internal.Codebase.Runtime.Curtain;

namespace AbyssMoth.Internal.Codebase.Infrastructure.Services.StaticData
{
    public interface IStaticDataService
    {
        public void Initialize();
        public CurtainConfig ForCurtain();
    }
}