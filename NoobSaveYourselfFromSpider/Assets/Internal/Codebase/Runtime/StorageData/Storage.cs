﻿// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Internal.Codebase.Runtime.StorageData
{
    [Serializable]
    public sealed class EmeraldCurrancy
    {
        public int emeralds;
    }

    [Serializable]
    public sealed class FishCurrancy
    {
        public int fishs;
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
        public FishCurrancy fishCurrancy;
        public EmeraldCurrancy emeraldCurrancy;

        public UserBestDistance userBestDistance;
        public UserSkins userSkins;
        public UserBioms userBioms;
        public AudioSettings audioSettings;

        [NonSerialized] public Action<int> OnFishCurrancyChanged;
        [NonSerialized] public Action<int> OnEmeraldCurrancyChanged;
        [NonSerialized] public Action<int> OnBestDistanceChanged;

        public void Refresh()
        {
            OnEmeraldCurrancyChanged?.Invoke(emeraldCurrancy.emeralds);
            OnFishCurrancyChanged?.Invoke(fishCurrancy.fishs);
            OnBestDistanceChanged?.Invoke(userBestDistance.bestDistance);
        }

        public int FishCurrancy
        {
            get => fishCurrancy.fishs;

            set
            {
                fishCurrancy.fishs += Mathf.Clamp(value, 0, int.MaxValue);
                OnFishCurrancyChanged?.Invoke(fishCurrancy.fishs);
            }
        }

        public int EmeraldCurrancy
        {
            get => emeraldCurrancy.emeralds;

            set
            {
                emeraldCurrancy.emeralds += Mathf.Clamp(value, 0, int.MaxValue);
                OnEmeraldCurrancyChanged?.Invoke(emeraldCurrancy.emeralds);
            }
        }

        public int BestDistance
        {
            get => userBestDistance.bestDistance;

            set
            {
                userBestDistance.bestDistance += Mathf.Clamp(value, 0, int.MaxValue);
                OnBestDistanceChanged?.Invoke(userBestDistance.bestDistance);
            }
        }
    }
}