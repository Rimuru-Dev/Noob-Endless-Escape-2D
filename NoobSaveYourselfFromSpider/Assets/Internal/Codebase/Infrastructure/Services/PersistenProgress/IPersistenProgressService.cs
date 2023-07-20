// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

namespace Internal.Codebase.Infrastructure.Services.StaticData
{
    public interface IPersistenProgressService
    {
        public void Init();
        public Runtime.StorageData.Storage GetStoragesData();
        public void Save(Runtime.StorageData.Storage storage);
        public void Load();
    }
}