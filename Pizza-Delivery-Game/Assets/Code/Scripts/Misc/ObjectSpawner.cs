using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Misc
{
    public class ObjectSpawner : Singleton<ObjectSpawner>
    {
        private Dictionary<Type, Queue<GameObject>> _pools = new();

        public T SpawnObject<T>(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
            where T : MonoBehaviour, ISpawnable
        {
            GameObject obj;
            Queue<GameObject> pool;

            // Here we check, if there is pool for this type
            if (!_pools.TryGetValue(typeof(T), out pool))
            {
                pool = new Queue<GameObject>();
                _pools[typeof(T)] = pool;
            }

            if (pool.Count > 0)
            {
                obj = pool.Dequeue();
                obj.transform.position = position;
                obj.transform.rotation = rotation;
                obj.SetActive(true);
            }
            else
            {
                obj = Instantiate(prefab, position, rotation, parent);
            }

            var spawnableComponent = obj.GetComponent<T>();
            spawnableComponent.OnSpawned();

            return spawnableComponent;
        }

        public void ReturnObject<T>(T objectToReturn) where T : MonoBehaviour, ISpawnable
        {
            objectToReturn.OnReturned();
            objectToReturn.gameObject.SetActive(false);
            _pools[typeof(T)].Enqueue(objectToReturn.gameObject);
        }
    }
}
