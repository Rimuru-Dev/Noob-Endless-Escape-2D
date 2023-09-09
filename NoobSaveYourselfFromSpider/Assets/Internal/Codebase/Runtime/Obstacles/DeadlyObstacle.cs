// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Internal.Codebase.Runtime.Hero;
using Internal.Codebase.Utilities.Constants;
using UnityEngine;

namespace Internal.Codebase.Runtime.Obstacles
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class DeadlyObstacle : MonoBehaviour
    {
        [field: SerializeField] public DeadlyObstacleTypeID DeadlyObstacleTypeID { get; private set; }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.transform.CompareTag(Tag.Player))
                return;

            var die = other.transform.GetComponent<HeroDie>();

            if (die != null)
                die.ApplyHeroDie();
        }
    }
}