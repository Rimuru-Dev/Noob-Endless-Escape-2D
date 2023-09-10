// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Internal.Codebase.Runtime.MainMenu.Animation
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public sealed class ImageClickAnimation : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private float scaleMultiplier = 1.32f;
        [SerializeField] private float animationDuration = 0.3f;
        [SerializeField] private int animationRepeat = 4;

        private bool isAnimating;
        private Vector3 originalScale;

        private void Start() =>
            originalScale = transform.localScale;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (isAnimating)
                return;

            isAnimating = true;

            var targetScale = transform.localScale * scaleMultiplier;

            transform
                .DOScale(targetScale, animationDuration)
                .SetEase(Ease.OutQuad)
                .SetLoops(animationRepeat, LoopType.Yoyo)
                .OnComplete(() =>
                {
                    transform
                        .DOScale(originalScale, animationDuration)
                        .SetEase(Ease.OutElastic)
                        .OnComplete(() => isAnimating = false);
                });
        }
    }
}