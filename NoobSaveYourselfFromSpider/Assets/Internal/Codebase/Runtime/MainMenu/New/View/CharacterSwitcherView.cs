using System.Collections.Generic;
using Internal.Codebase.Runtime.MainMenu.Animation;
using Internal.Codebase.Runtime.MainMenu.HeroSwither;
using Internal.Codebase.Runtime.SpriteTextNumberCounterLogic;
using UnityEngine;
using UnityEngine.UI;

namespace Internal.Codebase.Runtime.MainMenu.New.View
{
    [DisallowMultipleComponent]
    public sealed class CharacterSwitcherView : MonoBehaviour
    {
        public Image characterImage;
        public List<SkinShopData> skins;
        public Button buyButton;
        public Button leftButton;
        public Button rightButton;

        public GameObject selectSkin;
        public Image cyrrancy;
        public NumberVisualizer numberVisualizer;

        public List<CurrancyIcons> currancyIcons;
    }
}