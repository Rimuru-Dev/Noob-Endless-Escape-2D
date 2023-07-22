// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using System.Collections.Generic;
using Internal.Codebase.Runtime.EndlessLevelGenerationSolution.Configs;
using UnityEngine;
using UnityEngine.Serialization;

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
    }

    [Serializable]
    public sealed class SkinData
    {
        public int ID;
        public bool IsOpen;
    }

    [Serializable]
    public sealed class UserBioms
    {
        public BiomeTypeID selectionBiomId;
        public List<BiomDatas> biomeData;
    }

    [Serializable]
    public sealed class BiomDatas
    {
        public BiomeTypeID id;
        public bool isOpen;
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
                fishCurrancy.fishs += value;
                fishCurrancy.fishs = Mathf.Clamp(fishCurrancy.fishs, 0, int.MaxValue);
                OnFishCurrancyChanged?.Invoke(fishCurrancy.fishs);
            }
        }

        public int EmeraldCurrancy
        {
            get => emeraldCurrancy.emeralds;

            set
            {
                emeraldCurrancy.emeralds += value;
                emeraldCurrancy.emeralds = Mathf.Clamp(emeraldCurrancy.emeralds, 0, int.MaxValue);
                OnEmeraldCurrancyChanged?.Invoke(emeraldCurrancy.emeralds);
            }
        }

        public int BestDistance
        {
            get => userBestDistance.bestDistance;

            set
            {
                userBestDistance.bestDistance = value;
                userBestDistance.bestDistance = Mathf.Clamp(userBestDistance.bestDistance, 0, int.MaxValue);
                OnBestDistanceChanged?.Invoke(userBestDistance.bestDistance);
            }
        }
    }
}