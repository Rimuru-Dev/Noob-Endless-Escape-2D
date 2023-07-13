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
        public float parallaxScale;
        public float parallaxSpeed;

        private float[] backgroundInitialPositions;

        private void Start()
        {
            backgroundInitialPositions = new float[backgrounds.Length];
            
            for (var i = 0; i < backgrounds.Length; i++)
            {
                backgroundInitialPositions[i] = backgrounds[i].position.x;
            }
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

                // Продление фона из префабов
                if (backgrounds[i].position.x < backgroundInitialPositions[i])
                {
                    var rightmostBackgroundX = GetRightmostBackgroundX();

                    backgrounds[i].position = new Vector3(rightmostBackgroundX + backgroundInitialPositions[i],
                        backgrounds[i].position.y, backgrounds[i].position.z);
                }
            }
        }

        /// <summary>
        /// This method returns the `x` coordinate of the rightmost background from the 'backgrounds' array.
        /// </summary>
        /// <code>
        ///     private float GetRightmostBackgroundX() => 
        ///         backgrounds
        ///         .Select(background => background.position.x)
        ///         .Prepend(float.MinValue)
        ///        .Max();
        /// </code>
        /// <returns>rightmostX</returns>
        private float GetRightmostBackgroundX()
        {
            var rightmostX = float.MinValue;

            for (var i = 0; i < backgrounds.Length; i++)
            {
                if (backgrounds[i].position.x > rightmostX)
                    rightmostX = backgrounds[i].position.x;
            }

            return rightmostX;
        }
    }
}