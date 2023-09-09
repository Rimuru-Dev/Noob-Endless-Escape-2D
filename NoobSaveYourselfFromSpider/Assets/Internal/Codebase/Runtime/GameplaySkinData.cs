﻿using System;
using UnityEngine;

namespace Internal.Codebase.Runtime
{
    [Serializable]
    public sealed class GameplaySkinData
    {
        public int id;

        [NaughtyAttributes.ShowAssetPreview()]
        public Sprite icon;
    }
}