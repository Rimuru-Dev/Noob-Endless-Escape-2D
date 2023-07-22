// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Internal.Codebase.Runtime.Hero;
using UnityEngine;

namespace Internal.Codebase.Runtime.Obstacles
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class DeadlyObstacle : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.transform.CompareTag("Player"))
                return;

            other.transform
                .GetComponent<HeroDie>()
                .ApplyHeroDie();
        }
    }
}