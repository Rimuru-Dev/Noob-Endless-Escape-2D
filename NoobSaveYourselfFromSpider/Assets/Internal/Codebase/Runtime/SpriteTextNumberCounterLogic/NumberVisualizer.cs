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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
                ShowNumber(Random.Range(0, 9999));
        }

        public void ShowNumber(int number)
        {
            DisableAllImage();

            if (CanNotValidNumber(number))
                return;

            var numberString = $"{number}".PadLeft(4, '0');

            for (var i = 0; i < numberString.Length; i++)
            {
                var digitChar = numberString[i];
                var digit = int.Parse($"{digitChar}");

                if (digit < 0 || digit >= numberSprites.Length)
                    continue;

                numberImages[i].gameObject.SetActive(true);
                numberImages[i].sprite = numberSprites[digit];
            }
        }

        private static bool CanNotValidNumber(int number) =>
            number is < 0 or > 9999;

        private void DisableAllImage()
        {
            foreach (var image in numberImages)
                image.gameObject.SetActive(false);
        }
    }
}