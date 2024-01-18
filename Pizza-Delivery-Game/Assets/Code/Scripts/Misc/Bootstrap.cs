using DataPersistence;
using UnityEngine;

namespace Misc
{
    [DisallowMultipleComponent]
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private ServiceRegistrar _serviceRegistrar;
        [SerializeField] private DataPersistenceManager _dataPersistenceManager;
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private UI.UI _ui;
        [SerializeField] private Player.Player _player;
        
        private void Awake()
        {
            _serviceRegistrar.Initialize();
            _dataPersistenceManager.Initialize();
            _gameManager.Initialize();
            _player.Initialize();
            _ui.Initialize(_player);
        }
    }
}
