using Internal.Codebase.Runtime.GameplayScene.Timer;
using Internal.Codebase.Runtime.General.SpriteTextNumberCounterLogic;
using UnityEngine;

namespace Internal.Codebase.Runtime.GameplayScene.UI.View
{
    [DisallowMultipleComponent]
    public sealed class AdvertismentTimerView : MonoBehaviour
    {
        [field: SerializeField] public Transform RootPanel { get; private set; }
        [field: SerializeField] public GameTimer Timer { get; private set; }
        [field: SerializeField] public NumberVisualizer TimeToPlayVisualizer { get; private set; }
    }
}