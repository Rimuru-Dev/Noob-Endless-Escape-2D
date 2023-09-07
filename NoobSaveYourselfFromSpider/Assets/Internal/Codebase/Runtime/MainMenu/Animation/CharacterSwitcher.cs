// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Internal.Codebase.Runtime.SpriteTextNumberCounterLogic;
using Internal.Codebase.Runtime.StorageData;
using YG;

namespace Internal.Codebase.Runtime.MainMenu.Animation
{
    [Serializable]
    public sealed class SkinShopData
    {
        public int id;

        [NaughtyAttributes.ShowAssetPreview(256, 256)]
        public Sprite icon;

        public CurrancyTypeID priceType;
        public int price;
        public bool isOpen;
    }

    [Serializable]
    public sealed class CurrancyIcons
    {
        public Sprite icon;
        public CurrancyTypeID currancyTypeID;
    }

    public interface IFuckingSaveLoad
    {
        public void Save();
        public void Load();
    }

    public sealed class CharacterSwitcher : MonoBehaviour, IFuckingSaveLoad
    {
        public Image characterImage;
        public List<SkinShopData> skins;
        public Button buyButton;

        public GameObject selectSkin;
        public Image cyrrancy;
        public NumberVisualizer numberVisualizer;

        public List<CurrancyIcons> currancyIcons;

        private RectTransform characterTransform;
        private int currentIndex;
        private bool isSwitching;

        private int currentActualSkinId;
        private int selectionSkinID;

        //     private Storage storage;
        //    private IPersistenProgressService persistenProgressService;

        [System.Diagnostics.Conditional("DEBUG")]
        private void OnValidate()
        {
            int id = -1;
            foreach (var skin in skins)
            {
                id++;
                skin.id = id;
            }
        }

        private void Awake()
        {
            if (YandexGame.SDKEnabled)
                Load();

            YandexGame.GetDataEvent += Load;
        }

        public void Save()
        {
            // if (storage != null)
            // {
            //     if (YandexGame.savesData.storage == null)
            //     {
            //         YandexGame.savesData.storage = new Storage();
            //         YandexGame.savesData.storage = storage;
            //         Debug.Log("FATAL ERROR SKIN SHOP");
            //     }
            //     else
            //         YandexGame.savesData.storage = storage;
            // }

            // YandexGame.savesData.storage.userSkins = storage.userSkins;
            YandexGame.savesData.storage.userSkins = storage.userSkins;
            YandexGame.savesData.storage.fishCurrancy = storage.fishCurrancy;
            YandexGame.savesData.storage.emeraldCurrancy = storage.emeraldCurrancy;
        }

        public Storage storage;

        public void Load()
        {
            if (YandexGame.savesData.storage.userSkins == null)
                LoadDefault();
            else
                LoadUserdata();

            currentActualSkinId = storage.userSkins.selectionSkinId;
            characterTransform = characterImage.GetComponent<RectTransform>();
            characterTransform.localScale = Vector3.zero;
            AnimateCharacter(true, currentActualSkinId);
            UpdateUI(currentActualSkinId);
            buyButton.onClick.AddListener(Buy);

            void LoadDefault()
            {
                YandexGame.savesData.storage.userSkins = new UserSkins();
                YandexGame.savesData.storage.userSkins.SkinDatas = new List<SkinData>();

                foreach (var skin in skins)
                {
                    var skinData = new SkinData();
                    skinData.ID = skin.id;
                    skinData.IsOpen = skin.isOpen;

                    YandexGame.savesData.storage.userSkins.SkinDatas.Add(skinData);
                }

                YandexGame.savesData.storage.userSkins.SkinDatas[0].IsOpen = true;
                YandexGame.savesData.storage.userSkins.selectionSkinId =
                    YandexGame.savesData.storage.userSkins.SkinDatas[0].ID;

                storage = YandexGame.savesData.storage;
            }

            void LoadUserdata()
            {
                storage = YandexGame.savesData.storage;

                foreach (var userSkin in storage.userSkins.SkinDatas)
                {
                    foreach (var skin in skins.Where(skin => userSkin.ID == skin.id))
                    {
                        skin.isOpen = userSkin.IsOpen;
                    }
                }
            }
        }

        // public void Initialize(Storage storage, IPersistenProgressService persistenProgressService)
        // {
        //    // this.storage = storage;
        //   //  this.persistenProgressService = persistenProgressService;
        //
        //     if (storage.userSkins.SkinDatas == null || storage.userSkins.SkinDatas.Count <= 0)
        //     {
        //         storage.userSkins.SkinDatas = new List<SkinData>();
        //
        //         foreach (var skin in skins)
        //         {
        //             var skinData = new SkinData();
        //             skinData.ID = skin.id;
        //             skinData.IsOpen = skin.isOpen;
        //             storage.userSkins.SkinDatas.Add(skinData);
        //         }
        //
        //         storage.userSkins.SkinDatas[0].IsOpen = true;
        //         storage.userSkins.selectionSkinId = storage.userSkins.SkinDatas[0].ID;
        //
        //         if (YandexGame.SDKEnabled)
        //             persistenProgressService.Save(this.storage);
        //     }
        //     else
        //     {
        //         foreach (var userSkin in storage.userSkins.SkinDatas)
        //         {
        //             foreach (var skin in skins.Where(skin => userSkin.ID == skin.id))
        //             {
        //                 skin.isOpen = userSkin.IsOpen;
        //             }
        //         }
        //     }
        //
        //     currentActualSkinId = storage.userSkins.selectionSkinId;
        //
        //     characterTransform = characterImage.GetComponent<RectTransform>();
        //     characterTransform.localScale = Vector3.zero;
        //
        //     AnimateCharacter(true, currentActualSkinId);
        //     UpdateUI(currentActualSkinId);
        //
        //     buyButton.onClick.AddListener(Buy);
        // }

        private void Buy()
        {
            var skin = skins[selectionSkinID];

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

                        selectSkin.SetActive(true);
                        numberVisualizer.gameObject.SetActive(false);
                        cyrrancy.gameObject.SetActive(false);

                        Save();
                        // persistenProgressService.Save(storage);
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

                        selectSkin.SetActive(true);
                        numberVisualizer.gameObject.SetActive(false);
                        cyrrancy.gameObject.SetActive(false);

                        Save();
                        // persistenProgressService.Save(storage);
                    }
                }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void SwitchCharacter(bool isRight)
        {
            if (isSwitching)
                return;

            int newIndex;

            if (isRight)
            {
                newIndex = currentIndex + 1;

                if (newIndex >= skins.Count)
                    newIndex = 0;
            }
            else
            {
                newIndex = currentIndex - 1;

                if (newIndex < 0)
                    newIndex = skins.Count - 1;
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
                        characterImage.sprite = skins[newIndex].icon;
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
            var skin = skins[id];

            if (!skin.isOpen)
            {
                selectSkin.SetActive(false);

                numberVisualizer.gameObject.SetActive(true);
                numberVisualizer.ShowNumber(skin.price);

                cyrrancy.gameObject.SetActive(true);
                cyrrancy.sprite = currancyIcons.FirstOrDefault(icon => icon.currancyTypeID == skin.priceType)!.icon;
            }
            else
            {
                currentActualSkinId = id;
                storage.userSkins.selectionSkinId = id;

                selectSkin.SetActive(true);
                numberVisualizer.gameObject.SetActive(false);
                cyrrancy.gameObject.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            YandexGame.GetDataEvent -= Load;
            buyButton.onClick.RemoveListener(Buy);
        }
    }
}