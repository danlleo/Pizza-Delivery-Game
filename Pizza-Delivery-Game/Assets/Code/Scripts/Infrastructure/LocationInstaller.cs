using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class LocationInstaller : MonoInstaller
    {
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Player.Player _playerPrefab;
        [SerializeField] private UI.UI _ui;
        
        public override void InstallBindings()
        {
            BindPlayer();
            BindUI();
        }

        private void BindUI()
        {
            UI.UI ui = Container.InstantiatePrefabForComponent<UI.UI>(_ui);

            Container
                .BindInstance(ui)
                .AsSingle();
        }

        private void BindPlayer()
        {
            Player.Player player = Container.InstantiatePrefabForComponent<Player.Player>(_playerPrefab, _startPoint.position,
                quaternion.identity, null);

            Container
                .BindInstance(player)
                .AsSingle();
        }
    }
}
