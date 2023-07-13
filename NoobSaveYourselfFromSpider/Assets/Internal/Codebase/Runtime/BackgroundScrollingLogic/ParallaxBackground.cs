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
        [SerializeField] private Camera mainCamera;

        private float[] backgroundInitialPositions;

        private void Start()
        {
            mainCamera ??= Camera.main;
            backgroundInitialPositions = new float[backgrounds.Length];

            for (var i = 0; i < backgrounds.Length; i++)
            {
                backgroundInitialPositions[i] = backgrounds[i].position.x;
            }
        }

        // private void Update()
        // {
        //     for (var i = 0; i < backgrounds.Length; i++)
        //     {
        //         var backgroundTargetPositionX =
        //             backgrounds[i].position.x + parallaxSpeed * (i * parallaxScale + 1) * Time.deltaTime;
        //
        //         var backgroundTargetPosition = new Vector3(backgroundTargetPositionX, backgrounds[i].position.y,
        //             backgrounds[i].position.z);
        //
        //         backgrounds[i].position = backgroundTargetPosition;
        //
        //         // Продление фона из префабов
        //         if (backgrounds[i].position.x < backgroundInitialPositions[i])
        //         {
        //             var rightmostBackgroundX = GetRightmostBackgroundX();
        //
        //             backgrounds[i].position = new Vector3(rightmostBackgroundX + backgroundInitialPositions[i],
        //                 backgrounds[i].position.y, backgrounds[i].position.z);
        //         }
        //     }
        // }

        private void Update()
        {
            float screenWidthInWorldPoint = CalculateScreenWidthInWorldPoint();

            Debug.Log(screenWidthInWorldPoint);

            for (int i = 0; i < backgrounds.Length; i++)
            {
                var backgroundTargetPositionX =
                    backgrounds[i].position.x + parallaxSpeed * (i * parallaxScale + 1) * Time.deltaTime;

                var backgroundTargetPosition = new Vector3(backgroundTargetPositionX, backgrounds[i].position.y,
                    backgrounds[i].position.z);

                backgrounds[i].position = backgroundTargetPosition;

                // Проверяем, если фон вышел за пределы экрана справа
                if (backgrounds[i].position.x < screenWidthInWorldPoint)
                {
                    Debug.Log("backgrounds[i].position.x < screenWidthInWorldPoint");

                    var leftmostBackgroundX = GetLeftmostBackgroundX();
                    var backgroundWidth = backgrounds[i].GetComponent<SpriteRenderer>().bounds.size.x;
                    var newBackgroundX = leftmostBackgroundX - backgroundWidth;

                    backgrounds[i].position =
                        new Vector3(newBackgroundX, backgrounds[i].position.y, backgrounds[i].position.z);
                }
            }
        }

        private float CalculateScreenWidthInWorldPoint()
        {
            var screenPoint = new Vector3(Screen.width, 0, mainCamera.transform.position.z);
            var screenWidthInWorldPoint = mainCamera.ScreenToWorldPoint(screenPoint).x;
            return screenWidthInWorldPoint;
        }

        private float GetLeftmostBackgroundX()
        {
            var leftmostX = float.MaxValue;

            for (int i = 0; i < backgrounds.Length; i++)
            {
                if (backgrounds[i].position.x < leftmostX)
                    leftmostX = backgrounds[i].position.x;
            }

            return leftmostX;
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