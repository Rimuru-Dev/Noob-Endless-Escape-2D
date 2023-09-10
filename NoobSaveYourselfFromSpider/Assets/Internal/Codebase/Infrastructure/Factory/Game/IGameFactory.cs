// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Internal.Codebase.Runtime.GameplayScene.LevelGeneration.Handlers;

namespace Internal.Codebase.Infrastructure.Factory.Game
{
    public interface IGameFactory
    {
        public EndlessLevelGenerationHandler CreateLevelGenerator();
    }
}