using Common;
using Environment.Share;
using InspectableObject;
using Misc;
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
        [SerializeField] private ModelViewCamera _modelViewCameraPrefab;
        
        public override void InstallBindings()
        {
            BindModelViewCamera();
            BindPlayer();
            BindUI();
            BindInputHandler();
            BindLocationDetails();
            BindInspectableObjectTrigger();
        }

        private void BindModelViewCamera()
        {
            ModelViewCamera modelViewCamera = Container.InstantiatePrefabForComponent<ModelViewCamera>(_modelViewCameraPrefab);

            Container
                .BindInstance(modelViewCamera)
                .AsSingle()
                .NonLazy();
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
                Quaternion.identity, null);

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
