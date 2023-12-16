using Interfaces;
using UnityEngine;

namespace Monster
{
    [RequireComponent(typeof(Animator))]
    public class MonsterCornerPeek : MonoBehaviour, ISpawnable
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        
        public void OnSpawned()
        {
            
        }

        public void OnReturned()
        {
            
        }
    }
}
