// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //


using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Internal.Codebase.Infrastructure.AssetManagement
{
    [SuppressMessage("ReSharper", "MethodOverloadWithOptionalParameter")]
    public interface IAssetProvider
    {
        public GameObject Instantiate(string path);
        public GameObject Instantiate(string path, Vector3 at);
        public GameObject Instantiate(string path, Transform parent = null);
        public GameObject Instantiate(GameObject prefab, Transform parent = null);
        public GameObject Instantiate(GameObject prefab, Vector3 at, Quaternion rotation);
    }
}