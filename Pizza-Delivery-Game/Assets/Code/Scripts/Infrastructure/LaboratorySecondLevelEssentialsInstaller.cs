using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class LaboratorySecondLevelEssentialsInstaller : MonoInstaller
    {
        [SerializeField] private Transform _monsterSpawnPoint;
        [SerializeField] private Monster.Monster _monsterPrefab;

        public override void InstallBindings()
        {
            BindMonster();
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