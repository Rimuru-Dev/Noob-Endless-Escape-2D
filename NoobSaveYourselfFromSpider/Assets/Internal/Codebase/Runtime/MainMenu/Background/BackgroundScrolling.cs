// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Internal.Codebase.Runtime.GameplayScene.Parallax;
using UnityEngine;
using UnityEngine.UI;

namespace Internal.Codebase.Runtime.MainMenu.Background
{
    [SelectionBase]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RawImage))]
    public sealed class BackgroundScrolling : MonoBehaviour
    {
        [SerializeField] private LoopMode loopMode = LoopMode.Update;
        [SerializeField] private Vector2 scrollPositionXY;
        private RawImage rawImage;
        private bool isPause;

        private void Start() =>
            rawImage = GetComponent<RawImage>();

        private void Update()
        {
            if (loopMode != LoopMode.Update || isPause)
                return;

            SetNewRawImageUV(Time.deltaTime);
        }

        public void SetBackgroundScrollingPause(bool pause) =>
            isPause = pause;

        private void SetNewRawImageUV(float delta) =>
            rawImage.uvRect = CalculateRectPosition(delta);

        private Rect CalculateRectPosition(float delta)
        {
            var offset = new Vector2(scrollPositionXY.x, scrollPositionXY.y);
            var position = rawImage.uvRect.position + offset * delta;
            var size = rawImage.uvRect.size;

            return new Rect(position, size);
        }
    }
}