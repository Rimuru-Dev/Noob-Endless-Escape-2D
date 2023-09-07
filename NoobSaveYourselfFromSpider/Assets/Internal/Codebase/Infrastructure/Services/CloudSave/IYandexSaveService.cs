// ReSharper disable CommentTypo
// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: 
//          - Gmail:    rimuru.dev@gmail.com
//          - GitHub:   https://github.com/RimuruDev
//          - LinkedIn: https://www.linkedin.com/in/rimuru/
//          - GitHub Organizations: https://github.com/Rimuru-Dev
//
// **************************************************************** //

using System;
using Zenject;

namespace Internal.Codebase.Infrastructure.Services.CloudSave
{
    public interface IYandexSaveService : IDisposable, ITickable
    {
        public void Init();
        public void Save();
        public void Save(Runtime.StorageData.Storage savedStorage);
        public Runtime.StorageData.Storage Load();
    }
}