using System;
using System.Collections.Generic;
using Internal.Codebase.Runtime.GameplayScene.LevelGeneration.Configs;

namespace Internal.Codebase.Runtime.General.StorageData
{
    [Serializable]
    public sealed class UserBioms
    {
        public BiomeTypeID selectionBiomId;
        public List<BiomDatas> biomeData;
    }
}