// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using UnityEngine;

namespace Internal.Codebase.Infrastructure.Services.Resource
{
    public interface IResourceLoaderService
    {
        public TResource Load<TResource>(string path) where TResource : Object;
        public TResource[] LoadAll<TResource>(string path) where TResource : Object;
    }
}