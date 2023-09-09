using System;
using Internal.Codebase.Runtime.GameplayScene.LevelGeneration.Configs;

namespace Internal.Codebase.Runtime.General.StorageData
{
    [Serializable]
    public sealed class BiomDatas
    {
        public BiomeTypeID id;
        public bool isOpen;
    }
}