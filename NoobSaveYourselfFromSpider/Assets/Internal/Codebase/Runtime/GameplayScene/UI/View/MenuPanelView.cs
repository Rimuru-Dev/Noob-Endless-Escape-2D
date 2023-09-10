using UnityEngine;
using UnityEngine.UI;

namespace Internal.Codebase.Runtime.GameplayScene.UI.View
{
    [DisallowMultipleComponent]
    public sealed class MenuPanelView : MonoBehaviour
    {
        [field: SerializeField] public Button PlayPauseButton { get; private set; }

        [field: SerializeField] public Transform MenuPanelRoot { get; private set; }
        [field: SerializeField] public Button ResumePauseButton { get; private set; }
        [field: SerializeField] public Button GoMainMenuButton { get; private set; }
    }
}