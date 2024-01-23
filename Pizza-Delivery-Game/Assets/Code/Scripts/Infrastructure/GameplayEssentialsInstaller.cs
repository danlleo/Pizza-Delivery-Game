using Environment.Share;
using InspectableObject;
using Misc;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameplayEssentialsInstaller : MonoInstaller
    {
        [SerializeField] private Transform _startPoint;
        
        [SerializeField] private Player.Player _playerPrefab;
        [SerializeField] private UI.UI _uiPrefab;
        [SerializeField] private LocationDetails _locationDetailsPrefab;
        
        public override void InstallBindings()
        {
            BindPlayer();
            BindUI();
            BindInputHandler();
            BindLocationDetails();
            BindInspectableObjectTrigger();
        }
        
        private void BindLocationDetails()
        {
            Container.InstantiatePrefab(_locationDetailsPrefab);
        }

        private void BindUI()
        {
            UI.UI ui = Container.InstantiatePrefabForComponent<UI.UI>(_uiPrefab);

            Container
                .BindInstance(ui)
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayer()
        {
            Player.Player player = Container.InstantiatePrefabForComponent<Player.Player>(_playerPrefab, _startPoint.position,
                quaternion.identity, null);

            Container
                .BindInstance(player)
                .AsSingle()
                .NonLazy();
        }

        private void BindInputHandler()
        {
            Container.BindInterfacesAndSelfTo<InputHandler>().FromNew().AsSingle();
        }

        private void BindInspectableObjectTrigger()
        {
            Container.Bind<InspectableObjectTrigger>().FromNew().AsSingle();
        }
    }
}
