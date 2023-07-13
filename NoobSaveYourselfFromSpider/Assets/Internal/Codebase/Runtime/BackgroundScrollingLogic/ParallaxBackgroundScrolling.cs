// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using UnityEngine;

namespace Internal.Codebase.Runtime.BackgroundScrollingLogic
{
    [SelectionBase]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public sealed class ParallaxBackgroundScrolling : MonoBehaviour
    {
        private const float RepositionOffsetX = 2f;

        [SerializeField] private float scrollSpeed = -3f;

        private BoxCollider2D boxCollider2D;
        private new Rigidbody2D rigidbody2D;
        private float with;

        private void Start()
        {
            boxCollider2D = GetComponent<BoxCollider2D>();
            with = boxCollider2D.size.x;

            rigidbody2D = GetComponent<Rigidbody2D>();
            rigidbody2D.velocity = new Vector2(scrollSpeed, 0);
        }

        private void Update()
        {
            if (transform.position.x <= -with)
                Reposition();
        }

        private void Reposition()
        {
            var vector2 = new Vector3(with * RepositionOffsetX, 0, 0);
            var newPositiom = transform.position + vector2;

            transform.position = newPositiom;
        }
    }
}