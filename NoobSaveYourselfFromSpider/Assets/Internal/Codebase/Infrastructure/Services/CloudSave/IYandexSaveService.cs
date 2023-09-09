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
using Internal.Codebase.Runtime.General.StorageData;

namespace Internal.Codebase.Infrastructure.Services.CloudSave
{
    public interface IYandexSaveService : IDisposable
    {
        public void Init();
        public void Save(Storage savedStorage);
        public Storage Load();
    }
}