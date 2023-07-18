// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using UnityEngine;
using UnityEngine.UI;

namespace Internal.Codebase.Runtime.SpriteTextNumberCounterLogic
{
    public sealed class NumberVisualizer : MonoBehaviour
    {
        public Sprite[] numberSprites;
        public Image[] numberImages;

        #region Visualize Text

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
                ShowNumber(Random.Range(0, 9999));
            if (Input.GetKeyDown(KeyCode.W))
                ShowNumber(0);
            if (Input.GetKeyDown(KeyCode.R))
                ShowNumber(145);
        }

        public void ShowNumber(int number)
        {
            DisableAllImage();

            if (CanNotValidNumber(number))
                return;

            var numberString = $"{number:0000}";

            var length = numberString.Length;
            for (var i = 0; i < length; i++)
            {
                var digitChar = numberString[i];
                var digit = digitChar - '0';

                if (digit < 0 || digit >= numberSprites.Length)
                    continue;

                var image = numberImages[i];
                if (!image.gameObject.activeSelf)
                {
                    image.gameObject.SetActive(true);
                    image.sprite = numberSprites[digit];
                }
            }
        }

        private static bool CanNotValidNumber(int number) =>
            number is < 0 or > 9999;

        private void DisableAllImage()
        {
            foreach (var image in numberImages)
            {
                if (image.gameObject.activeSelf)
                    image.gameObject.SetActive(false);
            }
        }

        #endregion

        #region Endless Auto Visualize

        private int currentNumber = 0;

        private void Start()
        {
            InvokeRepeating(nameof(UpdateNumber), 1f, .5f);
        }

        private void UpdateNumber()
        {
            currentNumber++;
            var numberString = currentNumber.ToString("0000");

            for (var i = 0; i < numberString.Length; i++)
            {
                var digitChar = numberString[i];
                var digit = digitChar - '0';

                if (digit >= 0 && digit < numberSprites.Length)
                {
                    numberImages[i].sprite = numberSprites[digit];
                }
            }

            if (currentNumber >= 9999)
            {
                CancelInvoke(nameof(UpdateNumber));
            }
        }

        #endregion
    }
}