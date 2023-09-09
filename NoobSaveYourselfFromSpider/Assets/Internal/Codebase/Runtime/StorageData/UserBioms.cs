using System;
using System.Collections.Generic;
using Internal.Codebase.Runtime.EndlessLevelGenerationSolution.Configs;

namespace Internal.Codebase.Runtime.StorageData
{
    [Serializable]
    public sealed class UserBioms
    {
        public BiomeTypeID selectionBiomId;
        public List<BiomDatas> biomeData;
    }
}