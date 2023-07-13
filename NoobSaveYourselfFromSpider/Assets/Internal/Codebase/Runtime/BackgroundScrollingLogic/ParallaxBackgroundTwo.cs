// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using UnityEngine;

namespace Internal.Codebase.Runtime.BackgroundScrollingLogic
{
    public sealed class ParallaxBackgroundTwo : MonoBehaviour
    {
        public Transform[] backgrounds;
        public float parallaxScale = 0;
        public float parallaxSpeed = 1;

        private float[] backgroundWidths;
        private float backgroundOffset;

        private void Start()
        {
            backgroundWidths = new float[backgrounds.Length];
            backgroundOffset = backgrounds[0].position.x;

            for (int i = 0; i < backgrounds.Length; i++)
            {
                SpriteRenderer spriteRenderer = backgrounds[i].GetComponent<SpriteRenderer>();
                backgroundWidths[i] = spriteRenderer.bounds.size.x;
            }
        }

        private void Update()
        {
            for (int i = 0; i < backgrounds.Length; i++)
            {
                float backgroundTargetPositionX =
                    backgrounds[i].position.x - parallaxSpeed * (parallaxScale/* + 1*/) * Time.deltaTime;

                if (backgroundTargetPositionX < backgroundOffset - backgroundWidths[i])
                {
                    backgroundTargetPositionX += backgroundWidths[i] * backgrounds.Length;
                }

                backgrounds[i].position = new Vector3(backgroundTargetPositionX, backgrounds[i].position.y,
                    backgrounds[i].position.z);
            }
        }
    }
}