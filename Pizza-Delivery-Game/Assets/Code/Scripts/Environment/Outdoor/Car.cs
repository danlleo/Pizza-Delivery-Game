using System;
using Enums.Scenes;
using Interfaces;
using Misc;
using Misc.Loader;
using Scientist;
using UI;
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
            _boxCollider.enabled = false;
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
            _boxCollider.enabled = true;
        }

        public void Interact()
        {
            Crossfade.Instance.FadeIn(InputAllowance.DisableInput, () => Loader.Load(Scene.LaboratoryEntryScene));
            Destroy(this);
        }

        public string GetActionDescription()
        {
            return "Sit in the car";
        }
    }
}
