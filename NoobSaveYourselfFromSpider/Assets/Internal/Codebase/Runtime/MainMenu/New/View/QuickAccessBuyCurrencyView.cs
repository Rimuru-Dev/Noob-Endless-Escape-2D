using UnityEngine;
using UnityEngine.UI;

namespace Internal.Codebase.Runtime.MainMenu.New.View
{
    [DisallowMultipleComponent]
    public sealed class QuickAccessBuyCurrencyView : MonoBehaviour
    {
        [field: SerializeField] public Button BuyFishButton { get; private set; }
        [field: SerializeField] public Button BuyEmeraldButton { get; private set; }
    }
}