// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using UnityEngine;

namespace Internal.Codebase.Runtime.SpriteTextNumberCounterLogic
{
    public sealed class DigitCounter : MonoBehaviour
    {
        public Sprite[] digitSprites;

        private int currentValue;
        private Transform[] digitContainers;
        private SpriteRenderer[] digitRenderers;

        private void Start()
        {
            // Получаем контейнеры и рендереры для каждой цифры
            digitContainers = GetComponentsInChildren<Transform>();
            digitRenderers = new SpriteRenderer[digitContainers.Length - 1]; // Исключаем родительский контейнер
            for (int i = 1; i < digitContainers.Length; i++)
            {
                digitRenderers[i - 1] = digitContainers[i].GetComponent<SpriteRenderer>();
            }

            // Устанавливаем начальное значение счетчика
            SetCounter(0);
        }

        public void SetCounter(int value)
        {
            currentValue = Mathf.Clamp(value, 0, int.MaxValue); // Ограничиваем значение в пределах int

            // Преобразуем целое число в массив цифр
            int[] digits = GetDigitsArray(currentValue);

            // Устанавливаем спрайты для каждой цифры
            for (int i = 0; i < digitRenderers.Length; i++)
            {
                if (digits.Length > i)
                {
                    digitRenderers[i].sprite = digitSprites[digits[digits.Length - 1 - i]];
                    digitRenderers[i].enabled = true;
                }
                else
                {
                    digitRenderers[i].enabled = false;
                }
            }
        }

        private int[] GetDigitsArray(int value)
        {
            int length = value.ToString().Length;
            int[] digits = new int[length];
            for (int i = 0; i < length; i++)
            {
                digits[i] = (int)(value / Mathf.Pow(10, length - i - 1)) % 10;
            }

            return digits;
        }
    }
}