// **************************************************************** //
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
using Internal.Codebase.Runtime.General.StorageData;
using Internal.Codebase.Runtime.MainMenu.Currency;
using Internal.Codebase.Runtime.MainMenu.HeroSwither.View;
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

            Refrash();
            Save();
            view.numberVisualizer.InitVisualizeText();
            storage.Refresh();

            view.leftButton.onClick.AddListener(() =>
            {
                storage.Refresh();
                SwitchCharacter(false);
            });
            view.rightButton.onClick.AddListener(() =>
            {
                storage.Refresh();
                SwitchCharacter(true);
            });

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

                foreach (var skinData in view.skins.Select(skin => new SkinData
                         {
                             ID = skin.id,
                             IsOpen = skin.isOpen
                         }))
                {
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
            storage.Refresh();
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

            storage.Refresh();
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

            AnimateCharacter(true, selectionSkinID);

            UpdateUI(selectionSkinID);

            isSwitching = true;
        }

        private void AnimateCharacter(bool isExpanding, int newIndex = -1)
        {
            var targetScale = isExpanding ? 1.2f : 0f;

            storage.Refresh();
            UpdateUI(selectionSkinID);

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

                            UpdateUI(selectionSkinID);
                            isSwitching = false;
                        });

                    UpdateUI(selectionSkinID);
                });

            storage.Refresh();
        }

        private void UpdateUI(int id)
        {
            var skin = view.skins[id];

            storage.Refresh();

            if (!skin.isOpen)
            {
                view.selectSkin.SetActive(false);

                view.numberVisualizer.gameObject.SetActive(true);
                view.numberVisualizer.ShowNumber(skin.price);

                view.cyrrancy.gameObject.SetActive(true);
                view.cyrrancy.sprite = view.currancyIcons.FirstOrDefault(icon => icon.currancyTypeID == skin.priceType)!
                    .icon;

                storage.Refresh();
            }
            else
            {
                currentActualSkinId = id;
                storage.userSkins.selectionSkinId = id;

                view.selectSkin.SetActive(true);
                view.numberVisualizer.gameObject.SetActive(false);
                view.cyrrancy.gameObject.SetActive(false);
            }

            storage.Refresh();
        }
    }
}