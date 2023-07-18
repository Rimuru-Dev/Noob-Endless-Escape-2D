﻿// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Internal.Codebase.Runtime.MainMenu.Animation
{
    public sealed class CharacterSwitcher : MonoBehaviour
    {
        public Image characterImage;
        public Sprite[] characterSprites;

        private RectTransform characterTransform;
        private int currentIndex = 0;
        private bool isSwitching = false;

        private void Start()
        {
            characterTransform = characterImage.GetComponent<RectTransform>();
            characterTransform.localScale = Vector3.zero;
            AnimateCharacter(true);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && !isSwitching)
            {
                SwitchCharacter(false);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && !isSwitching)
            {
                SwitchCharacter(true);
            }
        }

        public void SwitchCharacter(bool isRight)
        {
            int newIndex;
            if (isRight)
            {
                newIndex = currentIndex + 1;
                if (newIndex >= characterSprites.Length)
                {
                    newIndex = 0;
                }

                AnimateCharacter(true, newIndex);
            }
            else
            {
                newIndex = currentIndex - 1;
                if (newIndex < 0)
                {
                    newIndex = characterSprites.Length - 1;
                }

                AnimateCharacter(false, newIndex);
            }

            isSwitching = true;
        }

        private void AnimateCharacter(bool isExpanding, int newIndex = -1)
        {
            float targetScale = isExpanding ? 1.2f : 0f;

            characterTransform.DOScale(Vector3.one * targetScale, 0.5f)
                .OnComplete(() =>
                {
                    if (newIndex != -1)
                    {
                        characterImage.sprite = characterSprites[newIndex];
                        currentIndex = newIndex;
                    }

                    characterTransform.DORotate(Vector3.forward * 360f, 0.5f, RotateMode.FastBeyond360)
                        .OnComplete(() =>
                        {
                            characterTransform.localRotation = Quaternion.identity;
                            characterTransform.localScale = Vector3.one;

                            isSwitching = false;
                        });
                });
        }
    }
}