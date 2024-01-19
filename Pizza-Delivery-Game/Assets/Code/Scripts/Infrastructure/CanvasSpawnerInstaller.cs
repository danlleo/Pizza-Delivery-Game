using UI;
using Zenject;

namespace Infrastructure
{
    public class CanvasSpawnerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindFactory();
        }

        private void BindFactory()
        {
            Container.BindFactory<Player.Player, OneHourLaterCanvas, OneHourLaterCanvas.Factory>();
        }
    }
}
