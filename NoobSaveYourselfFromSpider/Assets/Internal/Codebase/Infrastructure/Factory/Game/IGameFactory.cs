// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Internal.Codebase.Runtime.EndlessLevelGenerationSolution.Handlers;

namespace Internal.Codebase.Infrastructure.Factory
{
    public interface IGameFactory
    {
        public EndlessLevelGenerationHandler CreateLevelGenerator();
    }
}