using System;
using DataPersistence;
using Enums.Scenes;
using Interfaces;
using Misc;
using Misc.Loader;
using Sounds.Audio;
using UnityEngine;
using Utilities;

namespace Environment.Bedroom
{
    [SelectionBase]
    [RequireComponent(typeof(BoxCollider))]
    [DisallowMultipleComponent]
    public class Door : MonoBehaviour, IInteractable
    {
        [Header("External references")]
        [SerializeField] private BedroomAudio _bedroomAudio;
        [SerializeField] private BoxCollider _blockCollider;
        
        private BoxCollider _interactableCollider;

        private void Awake()
        {
            _interactableCollider = GetComponent<BoxCollider>();
            
            DisableDoorInteractCollider();
        }

        private void OnEnable()
        {
            WokeUpStaticEvent.OnWokeUp += WokeUpStaticEvent_OnWokeUp;
        }
        
        private void OnDisable()
        {
            WokeUpStaticEvent.OnWokeUp -= WokeUpStaticEvent_OnWokeUp;
        }

        public void Interact()
        {
            ServiceLocator.ServiceLocator.GetCrossfadeService()
                .FadeIn(InputAllowance.DisableInput, () => Loader.Load(Scene.OutdoorScene), 1.5f);
           
            _bedroomAudio.PlayDoorOpenSound();
            OpenedDoorStaticEvent.Call(this);
            SaveStaticEvent.CallSaveEvent(this);
            Destroy(this);
        }

        public string GetActionDescription()
        {
            return "Leave";
        }

        private void EnableDoorInteractCollider()
            => _interactableCollider.Enable();

        private void DisableDoorInteractCollider()
            => _interactableCollider.Disable();

        private void DisableDoorBlockCollider()
            => _blockCollider.Disable();
        
        private void WokeUpStaticEvent_OnWokeUp(object sender, EventArgs e)
        {
            EnableDoorInteractCollider();
            DisableDoorBlockCollider();
        }
    }
}
