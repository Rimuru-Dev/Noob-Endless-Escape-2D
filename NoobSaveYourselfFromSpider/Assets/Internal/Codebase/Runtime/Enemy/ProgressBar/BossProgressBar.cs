// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Internal.Codebase.Runtime.Enemy.ProgressBar
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public sealed class BossProgressBar : MonoBehaviour
    {
        [field: SerializeField] public GameObject Root { get; private set; }
        [field: SerializeField] public Image Foreground { get; private set; }
        [field: SerializeField] public CanvasGroup CanvasGroup { get; private set; }

        private float maxValue = 100f;
        private float currentValue = 0f;

        private float counter = 0;

        public void StartProgressBar(float time)
        {
            StartCoroutine(ProgressBar(time));
        }

        public IEnumerator ProgressBar(float time)
        {
            SetFull();
            SetMaxValue(time);

            float startTime = Time.time; // сохраняем время начала

            while (Time.time - startTime < time)
            {
                float progress = (Time.time - startTime) / time; // вычисляем текущий прогресс от 0 до 1

                float value =
                    Mathf.Lerp(0f, maxValue, progress); // плавно изменяем значение от 0 до максимального значения

                SetValue(value);

                yield return null;
            }

            counter = 0;
        }

        public void SetMaxValue(float value)
        {
            maxValue = value;
            UpdateFillAmount();
        }

        public void SetValue(float value)
        {
            currentValue = value;
            UpdateFillAmount();
        }

        public void SetFull()
        {
            Foreground.fillAmount = 1;
        }

        public void UpdateFillAmount()
        {
            Foreground.fillAmount = currentValue / maxValue;
        }
    }
}