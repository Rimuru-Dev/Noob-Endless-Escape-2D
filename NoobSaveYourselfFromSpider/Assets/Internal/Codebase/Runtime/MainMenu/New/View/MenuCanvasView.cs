using System;
using Internal.Codebase.Runtime.BiomeShop;
using UnityEngine;
using UnityEngine.UI;

namespace Internal.Codebase.Runtime.MainMenu.New.View
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public sealed class MenuCanvasView : MonoBehaviour, IDisposable
    {
        [field: SerializeField] public CurrancyUIView CurrancFishView { get; private set; }
        [field: SerializeField] public CurrancyUIView CurrancEmeraldView { get; private set; }
        [field: SerializeField] public SettingsView SettingsView { get; private set; }
        [field: SerializeField] public CurrencyShopView CurrencyShopView { get; private set; }
        [field: SerializeField] public BestDistanceUIView BestDistanceView { get; private set; }
        [field: SerializeField] public QuickAccessBuyCurrencyView QuickAccessBuyCurrencyView { get; private set; }
        [field: SerializeField] public CharacterSwitcherView CharacterSwitcherView { get; private set; }
        [field: SerializeField] public BiomeShopView BiomeShopView { get; private set; }

        private void OnDestroy() =>
            Dispose();

        // TODO: Added Disposable and Cleanup Service
        public void Dispose()
        {
            foreach (var button in CurrencyShopView.OpenChop)
                button.onClick.RemoveAllListeners();
            CurrencyShopView.CloseChop.onClick.RemoveAllListeners();
            
            SettingsView.OpenPanel.onClick.RemoveAllListeners();
            SettingsView.ClosePanel.onClick.RemoveAllListeners();
            BiomeShopView.OpenPanel.onClick.RemoveAllListeners();
            BiomeShopView.CloseWindow.onClick.RemoveAllListeners();
            BiomeShopView.BuyBiomWinter.onClick.RemoveAllListeners();
            BiomeShopView.PlayBiomWinter.onClick.RemoveAllListeners();
            BiomeShopView.PlayBiomeForest.onClick.RemoveAllListeners();
        }
    }
}