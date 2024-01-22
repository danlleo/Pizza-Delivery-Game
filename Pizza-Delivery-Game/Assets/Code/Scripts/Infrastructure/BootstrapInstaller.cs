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
            BindScreenSettings();
            BindCursorLockState();
            BindCrossfade();
            BindTimeControl();
            BindGameManager();
            BindDataPersistenceManager();
        }

        private void BindScreenSettings()
        {
            Container.BindInterfacesAndSelfTo<ScreenSettings>()
                .AsSingle()
                .WithArguments(PlayerPrefs.HasKey(PlayerPrefsKeys.VSyncStatus) &&
                               PlayerPrefs.GetInt(PlayerPrefsKeys.VSyncStatus) != 0)
                .NonLazy();
        }

        private void BindCursorLockState()
        {
            Container.Bind<CursorLockState>().FromInstance(new CursorLockState(true)).AsSingle();
        }

        private void BindCrossfade()
        {
            Crossfade crossfade = Container.InstantiatePrefabForComponent<Crossfade>(_crossfade);

            Container.BindInstance(crossfade).AsSingle().NonLazy();
        }

        private void BindDataPersistenceManager()
        {
            Container
                .BindInterfacesAndSelfTo<DataPersistenceManager>()
                .AsSingle()
                .WithArguments("Data.game", true)
                .NonLazy();
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
            Container.BindInterfacesAndSelfTo<TimeControl.TimeControl>().AsSingle();
        }
    }
}
