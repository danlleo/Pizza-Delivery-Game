using Zenject;

namespace Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindTimeControl();
        }

        private void BindTimeControl()
        {
            Container.BindInterfacesAndSelfTo<TimeControl.TimeControl>().AsSingle();
        }
    }
}
