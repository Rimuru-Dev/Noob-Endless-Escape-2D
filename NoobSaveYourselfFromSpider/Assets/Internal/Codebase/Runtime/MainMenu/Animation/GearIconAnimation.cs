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
    public sealed class GearIconAnimation : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private float animationDuration = 0.5f;
        [SerializeField] private float rotationAngle = -180f;

        private bool isAnimating;
        private RectTransform gearIconRectTransform;
        private Quaternion startRotation;

        private void Start()
        {
            gearIconRectTransform = GetComponent<RectTransform>();
            startRotation = gearIconRectTransform.localRotation;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (isAnimating)
                return;

            isAnimating = true;

            if (gearIconRectTransform.localRotation == startRotation)
            {
                gearIconRectTransform
                    .DOLocalRotate(new Vector3(0f, rotationAngle, 0), animationDuration, RotateMode.FastBeyond360)
                    .SetEase(Ease.OutElastic)
                    .OnComplete(RotateBack);
            }
            else
            {
                gearIconRectTransform
                    .DOLocalRotate(startRotation.eulerAngles, animationDuration)
                    .SetEase(Ease.OutQuart)
                    .OnComplete(() => isAnimating = false);
            }
        }

        private void RotateBack()
        {
            gearIconRectTransform
                .DOLocalRotate(startRotation.eulerAngles, animationDuration)
                .SetEase(Ease.OutQuart)
                .OnComplete(() => isAnimating = false);
        }
    }
}