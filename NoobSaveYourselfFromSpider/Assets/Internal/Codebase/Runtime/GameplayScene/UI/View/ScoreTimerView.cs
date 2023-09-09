using Internal.Codebase.Runtime.General.SpriteTextNumberCounterLogic;
using UnityEngine;

namespace Internal.Codebase.Runtime.GameplayScene.UI.View
{
    [DisallowMultipleComponent]
    public sealed class ScoreTimerView : MonoBehaviour
    {
        [field: SerializeField] public NumberVisualizer ScoreVisualizer { get; private set; }
    }
}