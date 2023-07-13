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

        private float[] backgroundInitialPositions;

        private void Start()
        {
            backgroundInitialPositions = new float[backgrounds.Length];

            for (var i = 0; i < backgrounds.Length; i++)
            {
                backgroundInitialPositions[i] = backgrounds[i].position.x;
                var backgroundWidth = backgrounds[i].GetComponent<SpriteRenderer>().bounds.size.x;
                backgroundInitialPositions[i] -= backgroundWidth * i;
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
                if (backgrounds[i].position.x < -backgroundInitialPositions[i])
                {
                    var rightmostBackgroundX = GetRightmostBackgroundX();

                    backgrounds[i].position = new Vector3(rightmostBackgroundX + backgroundInitialPositions[i],
                        backgrounds[i].position.y, backgrounds[i].position.z);
                }
            }
        }


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