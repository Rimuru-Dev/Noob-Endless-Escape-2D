// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System.Diagnostics.CodeAnalysis;

namespace AbyssMoth.Internal.Codebase.Infrastructure.StateMachine.Interfaces
{
    public interface IStateWithArgument<T>
    {
        public void Enter<TArgs>(TArgs args);
    }
}