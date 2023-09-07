using UnityEngine;

namespace Internal.Codebase.Runtime.Settings
{
    public sealed class FPSSettings
    {
        private readonly int targetFPS;

        public FPSSettings(int targetFPS = 60) =>
            this.targetFPS = targetFPS;

        public void ApplyTargetFrameRate() =>
            Application.targetFrameRate = targetFPS;
    }
}