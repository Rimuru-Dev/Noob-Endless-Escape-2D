// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Internal.Codebase.Runtime.Curtain;
using Internal.Codebase.Runtime.MainMenu.Configs;

namespace Internal.Codebase.Infrastructure.Services.StaticData
{
    public interface IStaticDataService
    {
        public void Initialize();
        public CurtainConfig ForCurtain();
        public MainMenuConfig ForMainMenu();
    }
}