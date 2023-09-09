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
    // TODO: Refactoring on NumberVisualizerService : INumberVisualizerService
    public sealed class NumberVisualizer : MonoBehaviour
    {
        public bool IsStartCounter; // TODO: Refactoring this!
        public Sprite[] numberSprites;
        public Image[] numberImages;

        #region Visualize Text

#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                ShowNumber(Random.Range(0, 9999));

            if (Input.GetKeyDown(KeyCode.Alpha2))
                ShowNumber(0);

            if (Input.GetKeyDown(KeyCode.Alpha3))
                ShowNumber(145);
        }
#endif
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

                // if (i > numberImages.Length)
                //     continue;
                
                var image = numberImages[i];
                // if (image == null)
                //     continue;

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

        public int currentNumber;

        private void Start()
        {
            if (IsStartCounter)
                InvokeRepeating(nameof(UpdateNumber), 1f, .5f);
        }

        public bool IsPause { get; set; }
        private void UpdateNumber()
        {
            if (IsPause)
                return;
            
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