using UnityEngine;
using UnityEngine.UI;

namespace Internal.Codebase.Runtime.MainMenu.New.View
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public sealed class CurrencyShopView : MonoBehaviour
    {
        [field: SerializeField] public GameObject Panel { get; private set; }
        [field: SerializeField] public Button[] OpenChop { get; private set; }
        [field: SerializeField] public Button CloseChop { get; private set; }

        [field: SerializeField] public Button BuyFish { get; private set; }
        [field: SerializeField] public Button BuyEmerald { get; private set; }
    }
}