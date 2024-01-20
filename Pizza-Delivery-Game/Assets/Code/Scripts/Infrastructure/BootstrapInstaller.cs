using DataPersistence;
using Misc;
using Misc.CursorLockState;
using UI.Crossfade;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private DataPersistenceManager _dataPersistenceManager;
        [SerializeField] private Crossfade _crossfade;
        
        public override void InstallBindings()
        {
            BindCursorLockState();
            BindCrossfade();
            BindTimeControl();
            BindGameManager();
            BindDataPersistenceManager();
        }

        private void BindCursorLockState()
        {
            Container.Bind<CursorLockState>().FromInstance(new CursorLockState(true)).AsSingle();
        }

        private void BindCrossfade()
        {
            Crossfade crossfade = Container.InstantiatePrefabForComponent<Crossfade>(_crossfade);

            Container.BindInstance(crossfade).AsSingle();
        }

        private void BindDataPersistenceManager()
        {
            DataPersistenceManager dataPersistenceManager =
                Container.InstantiatePrefabForComponent<DataPersistenceManager>(_dataPersistenceManager);

            Container
                .BindInstance(dataPersistenceManager)
                .AsSingle();
        }

        private void BindGameManager()
        {
            GameManager gameManager = Container.InstantiatePrefabForComponent<GameManager>(_gameManager);

            Container
                .BindInstance(gameManager)
                .AsSingle();
        }

        private void BindTimeControl()
        {
            Container.Bind<TimeControl.TimeControl>().AsSingle();
        }
    }
}
