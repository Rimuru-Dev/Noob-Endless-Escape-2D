using System;

namespace Internal.Codebase.Infrastructure.Services.Cleanup
{
    public interface ICleanupService : IDisposable
    {
        public void Cleanup();
        public void RegisterCleanupAction(Action cleanupAction);
        public void UnregisterCleanupAction(Action cleanupAction);
    }
}