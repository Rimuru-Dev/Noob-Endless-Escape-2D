using System;
using System.Collections.Generic;

namespace Internal.Codebase.Runtime.General.StorageData
{
    [Serializable]
    public sealed class UserSkins
    {
        public int selectionSkinId;
        public List<SkinData> SkinDatas;
    }
}