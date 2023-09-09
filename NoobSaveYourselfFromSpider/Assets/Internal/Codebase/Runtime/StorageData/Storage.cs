// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using UnityEngine;

namespace Internal.Codebase.Runtime.StorageData
{
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
            if (emeraldCurrancy != null)
                OnEmeraldCurrancyChanged?.Invoke(emeraldCurrancy.emeralds);

            if (fishCurrancy != null)
                OnFishCurrancyChanged?.Invoke(fishCurrancy.fishs);

            if (userBestDistance != null)
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