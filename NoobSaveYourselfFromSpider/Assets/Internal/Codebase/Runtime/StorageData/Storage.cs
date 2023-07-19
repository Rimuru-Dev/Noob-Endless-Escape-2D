// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using System.Collections.Generic;

namespace Internal.Codebase.Runtime.StorageData
{
    [Serializable]
    public sealed class Currancys
    {
        public int emerald;
        public int fish;
    }

    [Serializable]
    public sealed class UserBestDistance
    {
        public int bestDistance;
    }

    [Serializable]
    public sealed class UserSkins
    {
        public int selectionSkinId;
        public List<SkinData> SkinDatas;

        public sealed class SkinData
        {
            public int ID;
            public bool IsOpen;
        }
    }

    [Serializable]
    public sealed class UserBioms
    {
        public int selectionBiomId;
        public List<BiomDatas> BiomData;

        public sealed class BiomDatas
        {
            public int ID;
            public bool IsOpen;
        }
    }

    [Serializable]
    public sealed class AudioSettings
    {
        public float volume;
    }

    [Serializable]
    public sealed class Storage
    {
        public Currancys currancys;
        public UserBestDistance userBestDistance;
        public UserSkins userSkins;
        public UserBioms userBioms;
        public AudioSettings audioSettings;
    }
}