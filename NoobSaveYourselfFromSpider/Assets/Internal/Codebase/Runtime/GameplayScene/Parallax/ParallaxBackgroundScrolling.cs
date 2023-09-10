// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//   Gist: https://gist.github.com/RimuruDev/bd6ce71565e49d8cdefc5631e8d6ecf9
//
// **************************************************************** //

using System;
using UnityEngine;

namespace Internal.Codebase.Runtime.GameplayScene.Parallax
{
    /// <summary>
    /// Add a background to the scene. You add components such as:
    ///  - Rigidbody2D,
    ///  - BoxCollider2D,
    ///  - SpriteRenderer to which you pass the background.
    /// Next, add this script, and adjust the scrolling speed of the background.
    /// Install two such backgrounds on the stage one after the other, and start the game.
    /// As a result, you will get an endless scrolling of the background.
    /// <code></code>
    /// Rigidbody's settings are as follows: Body Type - Kinematic.
    /// </summary>
    [SelectionBase]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public sealed class ParallaxBackgroundScrolling : MonoBehaviour
    {
        // If you need more backgrounds in length than two pieces, increase this value so that the queue works correctly!
        private const float RepositionOffsetX = 2f;

        [SerializeField] private LoopMode loopMode = LoopMode.Update;
        [SerializeField] private float scrollSpeed = -3f;

        private BoxCollider2D boxCollider2D;
        private new Rigidbody2D rigidbody2D;
        private float with;
        private bool isPause;

        private void Start()
        {
            boxCollider2D = GetComponent<BoxCollider2D>();
            with = boxCollider2D.size.x;

            rigidbody2D = GetComponent<Rigidbody2D>();
            rigidbody2D.velocity = new Vector2(scrollSpeed, 0);
        }

        private void FixedUpdate()
        {
            if (CheckPause())
                return;

            if (loopMode != LoopMode.FixedUpdate)
                return;

            if (CanReposition())
                Reposition();
        }

        private void Update()
        {
            if (CheckPause())
                return;

            if (loopMode != LoopMode.Update)
                return;

            if (CanReposition())
                Reposition();
        }

        private void LateUpdate()
        {
            if (CheckPause())
                return;

            if (loopMode != LoopMode.LateUpdate)
                return;

            if (CanReposition())
                Reposition();
        }

        [System.Diagnostics.Conditional("DEBUG")]
        private void OnValidate()
        {
            var rigidbody2DComponent = GetComponent<Rigidbody2D>();

            if (rigidbody2DComponent.bodyType != RigidbodyType2D.Kinematic)
                rigidbody2DComponent.bodyType = RigidbodyType2D.Kinematic;
        }

        public void SetPause(bool pause) =>
            isPause = pause;

        private bool CanReposition() =>
            transform.position.x <= -with;

        private void Reposition()
        {
            var vector2 = new Vector3(with * RepositionOffsetX, 0, 0);
            transform.position += vector2;
        }

        private bool CheckPause()
        {
            if (!isPause)
                return false;

            rigidbody2D.velocity = Vector2.zero;

            return true;
        }
    }
}