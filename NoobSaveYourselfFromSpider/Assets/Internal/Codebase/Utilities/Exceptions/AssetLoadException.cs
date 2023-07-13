// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;

namespace Internal.Codebase.Utilities.Exceptions
{
    public sealed class AssetLoadException : Exception
    {
        public AssetLoadException()
        {
        }

        public AssetLoadException(string path) : base(path)
        {
        }

        public AssetLoadException(string path, Exception inner) : base(path, inner)
        {
        }
    }
}