// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;

namespace AbyssMoth.Internal.Codebase.Infrastructure.Services.Storage
{
    public interface IStorageService
    {
        public string GetDataPath { get; }
        public void Save(string key, object data, Action<bool> onCallback = null);
        public void Load<TData>(string key, Action<TData> onCallback);
    }
}