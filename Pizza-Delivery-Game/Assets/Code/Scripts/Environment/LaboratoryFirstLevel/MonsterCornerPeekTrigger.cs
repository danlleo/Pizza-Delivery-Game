using Misc;
using Monster;
using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    [RequireComponent(typeof(BoxCollider))]
    public class MonsterCornerPeekTrigger : MonoBehaviour
    {
        [SerializeField] private MonsterCornerPeek _monsterCornerPeek;
        
        private void OnTriggerEnter(Collider other)
        {
            print("Govno");
            
            if (!other.gameObject.TryGetComponent(out Player.Player _)) return;
            
            print("Fart");
            
            ObjectSpawner.Instance.SpawnObject<MonsterCornerPeek>(_monsterCornerPeek.gameObject,
                _monsterCornerPeek.transform.position, _monsterCornerPeek.transform.rotation);
            
            Destroy(gameObject);
        }
    }
}