using System;

namespace Internal.Codebase.Runtime.General.StorageData
{
    [Serializable]
    public sealed class AudioSettings
    {
        public float volume;

        public AudioSettings() =>
            volume = 0.15f;
    }
}