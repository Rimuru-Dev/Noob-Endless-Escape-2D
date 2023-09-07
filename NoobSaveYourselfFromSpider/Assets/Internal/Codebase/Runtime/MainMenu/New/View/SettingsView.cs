using UnityEngine;
using UnityEngine.UI;

namespace Internal.Codebase.Runtime.MainMenu.New.View
{
    public sealed class SettingsView : MonoBehaviour
    {
        // InteractionWindow
        [field: SerializeField] public GameObject Panel { get; private set; }
        [field: SerializeField] public Button OpenPanel { get; private set; }
        [field: SerializeField] public Button ClosePanel { get; private set; }

        // Audio Settings
        [field: SerializeField] public Slider AudioSlider { get; private set; }
    }
}