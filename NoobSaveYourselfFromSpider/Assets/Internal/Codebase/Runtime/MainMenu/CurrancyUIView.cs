using UnityEngine;
using UnityEngine.UI;

namespace Internal.Codebase.Runtime.MainMenu
{
    public sealed class CurrancyUIView : MonoBehaviour
    {
        [field: SerializeField] public Text Currancytext { get; private set; }
    }
}