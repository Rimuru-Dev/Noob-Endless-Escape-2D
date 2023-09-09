using System;

namespace Internal.Codebase.Runtime.StorageData
{
    [Serializable]
    public sealed class AudioSettings
    {
        public float volume;

        public AudioSettings() =>
            volume = 0.15f;
    }
}