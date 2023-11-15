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
            _scientist.StartedWalkingEvent.Event += StartedWalking_Event;
            _scientist.StoppedWalkingEvent.Event += StoppedWalking_Event;
        }

        private void OnDisable()
        {
            _scientist.StartedTalkingEvent.Event -= StartedTalking_Event;
            _scientist.StartedWalkingEvent.Event -= StartedWalking_Event;
            _scientist.StoppedWalkingEvent.Event -= StoppedWalking_Event;
        }
        
        private void StartedTalking_Event(object sender, EventArgs e)
        {
            _animator.SetTrigger(AnimationParams.OnStartedTalking);
        }
        
        private void StartedWalking_Event(object sender, EventArgs e)
        {
            _animator.SetBool(AnimationParams.IsWalking, true);
        }
        
        private void StoppedWalking_Event(object sender, EventArgs e)
        {
            _animator.SetBool(AnimationParams.IsWalking, false);
        }
    }
}