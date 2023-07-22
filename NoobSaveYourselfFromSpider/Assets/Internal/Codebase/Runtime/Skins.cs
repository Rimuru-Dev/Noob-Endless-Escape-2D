// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System.Collections.Generic;
using UnityEngine;

namespace Internal.Codebase.Runtime
{
    [CreateAssetMenu(menuName = "Configs/Create " + nameof(Skins), fileName = nameof(Skins), order = 0)]
    public sealed class Skins : ScriptableObject
    {
       public List<GameplaySkinData> gameplaySkinDatas;
    }
}