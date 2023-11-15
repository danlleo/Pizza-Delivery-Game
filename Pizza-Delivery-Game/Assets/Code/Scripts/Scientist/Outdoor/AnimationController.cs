using System;
using UnityEngine;

namespace Scientist.Outdoor
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Scientist))]
    [RequireComponent(typeof(Animator))]
    public class AnimationController : MonoBehaviour
    {
        private Scientist _scientist;
        private Animator _animator;

        private void Awake()
        {
            _scientist = GetComponent<Scientist>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _scientist.StartedTalkingEvent.Event += StartedTalking_Event;
        }

        private void OnDisable()
        {
            _scientist.StartedTalkingEvent.Event -= StartedTalking_Event;
        }
        
        private void StartedTalking_Event(object sender, EventArgs e)
        {
            _animator.SetTrigger(AnimationParams.OnStartedTalking);
        }
    }
}