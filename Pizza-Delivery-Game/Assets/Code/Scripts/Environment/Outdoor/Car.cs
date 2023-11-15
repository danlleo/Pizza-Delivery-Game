using System;
using Interfaces;
using Scientist.Outdoor;
using UnityEngine;

namespace Environment.Outdoor
{
    [RequireComponent(typeof(BoxCollider))]
    public class Car : MonoBehaviour, IInteractable
    {
        private BoxCollider _boxCollider;

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
        }

        private void OnEnable()
        {
            FinishedTalkingStaticEvent.Event += FinishedTalkingStatic_Event;
        }

        private void OnDisable()
        {
            FinishedTalkingStaticEvent.Event -= FinishedTalkingStatic_Event;
        }

        private void FinishedTalkingStatic_Event(object sender, EventArgs e)
        {
            // TODO: Enable collider
        }

        public void Interact()
        {
            // TODO: Sit in the car
        }

        public string GetActionDescription()
        {
            return "Sit in the car";
        }
    }
}
