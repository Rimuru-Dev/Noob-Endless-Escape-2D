using System;

namespace Internal.Codebase.Infrastructure.Services.ActionUpdater
{
    [Serializable]
    public enum UpdateType
    {
        FixedUpdate = 0,
        Update = 1,
        LateUpdate = 2,
    }
}