using UnityEngine;

namespace Internal.Codebase.Runtime.GameplayScene.UI.View
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public sealed class UIRoot : MonoBehaviour
    {
        [field: SerializeField] public GameplayCanvasView GameplayCanvasView { get;  set; }
    }
}