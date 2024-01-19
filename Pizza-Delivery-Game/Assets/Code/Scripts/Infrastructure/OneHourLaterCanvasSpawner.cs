using UI;
using Zenject;

namespace Infrastructure
{
    public class OneHourLaterCanvasSpawner
    {
        private Player.Player _player;
        
        private readonly OneHourLaterCanvas.Factory _factory;

        [Inject]
        private void Construct(Player.Player player)
        {
            _player = player;
        }
        
        public OneHourLaterCanvasSpawner(OneHourLaterCanvas.Factory factory)
        {
            _factory = factory;
        }

        public void Spawn()
        {
            _factory.Create(_player);
        }
    }
}
