using System;
using UnityEngine;

namespace Monster
{
    [RequireComponent(typeof(Monster))]
    [RequireComponent(typeof(Animator))]
    [DisallowMultipleComponent]
    public class AnimationController : MonoBehaviour
    {
        private Monster _monster;
        private Animator _animator;
        
        private void Awake()
        {
            _monster = GetComponent<Monster>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _monster.StartedChasingEvent.Event += StartedChasing_Event;
            _monster.StoppedChasingEvent.Event += StoppedChasing_Event;
        }

        private void OnDisable()
        {
            _monster.StartedChasingEvent.Event -= StartedChasing_Event;
            _monster.StoppedChasingEvent.Event -= StoppedChasing_Event;
        }

        private void StartedChasing_Event(object sender, EventArgs e)
        {
            
        }

        private void StoppedChasing_Event(object sender, EventArgs e)
        {
            
        }
    }
}
