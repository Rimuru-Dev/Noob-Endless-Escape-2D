// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using UnityEngine;

namespace Internal.Codebase.Runtime.BackgroundScrollingLogic
{
    [DisallowMultipleComponent]
    public sealed class ParallaxBackground : MonoBehaviour
    {
        public Transform[] backgrounds;
        public float parallaxScale = 0;
        public float parallaxSpeed = 1;

        private float[] backgroundWidths;
        private float rightmostBackgroundX;

        private void Start()
        {
            backgroundWidths = new float[backgrounds.Length];

            for (var i = 0; i < backgrounds.Length; i++)
            {
                backgroundWidths[i] = GetSpriteWidth(backgrounds[i]);
            }

            rightmostBackgroundX = GetRightmostBackgroundX();
        }

        private void Update()
        {
            for (var i = 0; i < backgrounds.Length; i++)
            {
                var backgroundTargetPositionX =
                    backgrounds[i].position.x + parallaxSpeed * (i * parallaxScale + 1) * Time.deltaTime;
                var backgroundTargetPosition = new Vector3(backgroundTargetPositionX, backgrounds[i].position.y,
                    backgrounds[i].position.z);
                backgrounds[i].position = backgroundTargetPosition;

                if (backgrounds[i].position.x + backgroundWidths[i] < rightmostBackgroundX)
                {
                    MoveBackgroundToEnd(i);
                }
            }

            rightmostBackgroundX = GetRightmostBackgroundX();
        }

        private float GetSpriteWidth(Transform spriteTransform)
        {
            var spriteRenderer = spriteTransform.GetComponent<SpriteRenderer>();
            var spriteBounds = spriteRenderer.bounds;
            return spriteBounds.size.x;
        }

        private float GetRightmostBackgroundX()
        {
            var rightmostX = float.MinValue;

            for (var i = 0; i < backgrounds.Length; i++)
            {
                var backgroundX = backgrounds[i].position.x + backgroundWidths[i] / 2;
                if (backgroundX > rightmostX)
                    rightmostX = backgroundX;
            }

            return rightmostX;
        }

        private void MoveBackgroundToEnd(int backgroundIndex)
        {
            var leftmostBackgroundX = GetLeftmostBackgroundX();
            var newBackgroundX = rightmostBackgroundX + backgroundWidths[backgroundIndex];
            var backgroundPosition = backgrounds[backgroundIndex].position;
            backgroundPosition.x = leftmostBackgroundX + (newBackgroundX - rightmostBackgroundX) / 2;
            backgrounds[backgroundIndex].position = backgroundPosition;
        }

        private float GetLeftmostBackgroundX()
        {
            var leftmostX = float.MaxValue;

            for (var i = 0; i < backgrounds.Length; i++)
            {
                var backgroundX = backgrounds[i].position.x - backgroundWidths[i] / 2;
                if (backgroundX < leftmostX)
                    leftmostX = backgroundX;
            }

            return leftmostX;
        }
    }
}