using Internal.Codebase.Runtime.SpriteTextNumberCounterLogic;
using UnityEngine;
using UnityEngine.UI;

namespace Internal.Codebase.Runtime.MainMenu
{
    public sealed class CurrancyUIView : MonoBehaviour
    {
        [field: SerializeField] public NumberVisualizer NumberVisualizer { get; private set; }
    }
}