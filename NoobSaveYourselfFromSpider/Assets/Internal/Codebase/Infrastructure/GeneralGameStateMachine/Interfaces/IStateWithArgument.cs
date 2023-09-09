// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System.Diagnostics.CodeAnalysis;

namespace Internal.Codebase.Infrastructure.GeneralGameStateMachine.Interfaces
{
    [SuppressMessage("ReSharper", "UnusedTypeParameter")]
    public interface IStateWithArgument<T>
    {
        public void Enter<TArgs>(TArgs args);
    }
}