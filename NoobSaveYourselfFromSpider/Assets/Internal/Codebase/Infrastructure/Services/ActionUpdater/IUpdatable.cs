using System;

namespace Internal.Codebase.Infrastructure.Services.ActionUpdater
{
    public interface IActionUpdaterService : IDisposable
    {
        public void Subscribe(Action updateable, UpdateType updateType);
        public void Unsubscribe(Action updateable, UpdateType updateType);
        public void FixedUpdate();
        public void Update();
        public void LateUpdate();
        public void Pause(bool pause);
    }
}