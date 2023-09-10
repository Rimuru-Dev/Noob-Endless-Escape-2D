using System.Diagnostics.CodeAnalysis;
using Internal.Codebase.Infrastructure.Factory.Game;
using Internal.Codebase.Infrastructure.Factory.Hero;
using Internal.Codebase.Infrastructure.Factory.UI;
using Zenject;

namespace Internal.Codebase.Infrastructure.Installers
{
    [SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
    public sealed class FactoryInstaller : MonoInstaller
    {
        public override void InstallBindings() =>
            BindFactory();

        private void BindFactory()
        {
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
            Container.Bind<IHeroFactory>().To<HeroFactory>().AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
        }
    }
}