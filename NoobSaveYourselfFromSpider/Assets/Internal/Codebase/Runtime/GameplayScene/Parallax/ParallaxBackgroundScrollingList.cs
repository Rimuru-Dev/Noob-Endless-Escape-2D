using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Internal.Codebase.Runtime.GameplayScene.Parallax
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public sealed class ParallaxBackgroundScrollingList : MonoBehaviour
    {
        [field: SerializeField] public List<ParallaxBackgroundScrolling> BackgroundScrollingList { get; private set; }

        [System.Diagnostics.Conditional("DEBUG")]
        private void OnValidate()
        {
            BackgroundScrollingList = new List<ParallaxBackgroundScrolling>();

            var allBackgroundScrolling = GetComponentsInChildren<ParallaxBackgroundScrolling>();

            foreach (var backgroundScrolling in allBackgroundScrolling.Where(x => x != null).ToList())
                BackgroundScrollingList.Add(backgroundScrolling);
        }

        public void SetPauseForAllParallaxBackgroundScrolling(bool pause)
        {
            foreach (var backgroundScrolling in BackgroundScrollingList)
                backgroundScrolling.SetPause(pause);
        }
    }
}