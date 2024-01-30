using UI;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class LaboratorySecondLevelEssentialsInstaller : MonoInstaller
    {
        [SerializeField] private Transform _monsterSpawnPoint;
        [SerializeField] private Monster.Monster _monsterPrefab;
        [SerializeField] private LoseItCanvas _loseItCanvasPrefab;
        
        public override void InstallBindings()
        {
            BindMonster();
            BindLoseItCanvas();
        }

        private void BindLoseItCanvas()
        {
            LoseItCanvas loseItCanvas = Container.InstantiatePrefabForComponent<LoseItCanvas>(_loseItCanvasPrefab);

            Container
                .BindInstance(loseItCanvas)
                .AsSingle()
                .NonLazy();
        }

        private void BindMonster()
        {
            Monster.Monster monster = Container.InstantiatePrefabForComponent<Monster.Monster>(_monsterPrefab,
                _monsterSpawnPoint.position, Quaternion.identity, null);

            Container
                .BindInstance(monster)
                .AsSingle()
                .NonLazy();
        }
    }
}