﻿// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Internal.Codebase.Infrastructure.Services.CloudSave;
using Internal.Codebase.Runtime.MainMenu.HeroSwither.View;
using Internal.Codebase.Runtime.MainMenu.New.Currency;
using Internal.Codebase.Runtime.StorageData;
using UnityEngine;

namespace Internal.Codebase.Runtime.MainMenu.HeroSwither.Controller
{
    public sealed class CharacterSwitcher : IDisposable
    {
        private int currentIndex;
        private bool isSwitching;
        private int selectionSkinID;
        private int currentActualSkinId;
        private RectTransform characterTransform;

        private Storage storage;
        private readonly IYandexSaveService saveService;
        private readonly CharacterSwitcherView view;

        public CharacterSwitcher(CharacterSwitcherView characterSwitcherView, IYandexSaveService yandexSaveService)
        {
            view = characterSwitcherView;
            saveService = yandexSaveService;
        }

        ~CharacterSwitcher() =>
            Dispose();

        public void Prepare()
        {
            storage = saveService.Load();

            if (storage.userSkins == null)
                LoadDefault();
            else
                LoadUserdata();

            view.leftButton.onClick.AddListener(() => { SwitchCharacter(true); });
            view.rightButton.onClick.AddListener(() => { SwitchCharacter(false); });

            Refrash();
            Save();

            void Refrash()
            {
                currentActualSkinId = storage.userSkins.selectionSkinId;
                characterTransform = view.characterImage.GetComponent<RectTransform>();
                characterTransform.localScale = Vector3.zero;
                AnimateCharacter(true, currentActualSkinId);
                UpdateUI(currentActualSkinId);
                view.buyButton.onClick.AddListener(Buy);
            }

            void LoadDefault()
            {
                storage.userSkins = new UserSkins
                {
                    SkinDatas = new List<SkinData>()
                };

                foreach (var skin in view.skins)
                {
                    var skinData = new SkinData();
                    skinData.ID = skin.id;
                    skinData.IsOpen = skin.isOpen;

                    storage.userSkins.SkinDatas.Add(skinData);
                }

                storage.userSkins.SkinDatas[0].IsOpen = true;
                storage.userSkins.selectionSkinId = storage.userSkins.SkinDatas[0].ID;
            }

            void LoadUserdata()
            {
                foreach (var userSkin in storage.userSkins.SkinDatas)
                {
                    foreach (var skin in view.skins.Where(skin => userSkin.ID == skin.id))
                        skin.isOpen = userSkin.IsOpen;
                }
            }
        }

        public void Dispose()
        {
            view.leftButton.onClick.RemoveAllListeners();
            view.rightButton.onClick.RemoveAllListeners();
            view.buyButton.onClick.RemoveAllListeners();
        }

        private void Save() =>
            saveService.Save(storage);

        private void Buy()
        {
            var skin = view.skins[selectionSkinID];

            if (skin.isOpen)
                return;

            switch (skin.priceType)
            {
                case CurrancyTypeID.Emerald:
                {
                    if (storage.EmeraldCurrancy >= skin.price)
                    {
                        skin.isOpen = true;

                        storage.userSkins.SkinDatas[selectionSkinID].IsOpen = true;
                        storage.userSkins.selectionSkinId = selectionSkinID;

                        storage.EmeraldCurrancy = -skin.price;

                        view.selectSkin.SetActive(true);
                        view.numberVisualizer.gameObject.SetActive(false);
                        view.cyrrancy.gameObject.SetActive(false);

                        Save();
                    }
                }
                    break;
                case CurrancyTypeID.Fish:
                {
                    if (storage.FishCurrancy >= skin.price)
                    {
                        skin.isOpen = true;

                        storage.userSkins.SkinDatas[selectionSkinID].IsOpen = true;
                        storage.userSkins.selectionSkinId = selectionSkinID;

                        storage.FishCurrancy = -skin.price;

                        view.selectSkin.SetActive(true);
                        view.numberVisualizer.gameObject.SetActive(false);
                        view.cyrrancy.gameObject.SetActive(false);

                        Save();
                    }
                }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SwitchCharacter(bool isRight)
        {
            if (isSwitching)
                return;

            int newIndex;

            if (isRight)
            {
                newIndex = currentIndex + 1;

                if (newIndex >= view.skins.Count)
                    newIndex = 0;
            }
            else
            {
                newIndex = currentIndex - 1;

                if (newIndex < 0)
                    newIndex = view.skins.Count - 1;
            }

            selectionSkinID = newIndex;

            UpdateUI(newIndex);

            AnimateCharacter(true, newIndex);

            isSwitching = true;
        }

        private void AnimateCharacter(bool isExpanding, int newIndex = -1)
        {
            var targetScale = isExpanding ? 1.2f : 0f;

            characterTransform
                .DOScale(Vector3.one * targetScale, 0.5f)
                .OnComplete(() =>
                {
                    if (newIndex != -1)
                    {
                        view.characterImage.sprite = view.skins[newIndex].icon;
                        currentIndex = newIndex;
                    }

                    characterTransform
                        .DORotate(Vector3.forward * 360f, 0.5f, RotateMode.FastBeyond360)
                        .OnComplete(() =>
                        {
                            characterTransform.localRotation = Quaternion.identity;
                            characterTransform.localScale = Vector3.one;

                            isSwitching = false;
                        });
                });
        }

        private void UpdateUI(int id)
        {
            var skin = view.skins[id];

            if (!skin.isOpen)
            {
                view.selectSkin.SetActive(false);

                view.numberVisualizer.gameObject.SetActive(true);
                view.numberVisualizer.ShowNumber(skin.price);

                view.cyrrancy.gameObject.SetActive(true);
                view.cyrrancy.sprite = view.currancyIcons.FirstOrDefault(icon => icon.currancyTypeID == skin.priceType)!
                    .icon;
            }
            else
            {
                currentActualSkinId = id;
                storage.userSkins.selectionSkinId = id;

                view.selectSkin.SetActive(true);
                view.numberVisualizer.gameObject.SetActive(false);
                view.cyrrancy.gameObject.SetActive(false);
            }
        }
    }
}