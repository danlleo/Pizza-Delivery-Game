using System;
using DataPersistence;
using Enums.Scenes;
using Interfaces;
using Misc;
using Misc.Loader;
using Sounds.Audio;
using UI;
using UnityEngine;

namespace Environment.Bedroom
{
    [SelectionBase]
    [RequireComponent(typeof(BoxCollider))]
    [DisallowMultipleComponent]
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private BedroomAudio _bedroomAudio;

        private BoxCollider _boxCollider;

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
            
            DisableDoorCollider();
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
            Crossfade.Instance.FadeIn(InputAllowance.DisableInput, () => Loader.Load(Scene.TestingFeaturesScene));
            _bedroomAudio.PlayDoorOpenSound();
            SaveStaticEvent.CallSaveEvent(this);
            Destroy(this);
        }

        public string GetActionDescription()
        {
            return "Leave";
        }

        private void EnableDoorCollider()
            => _boxCollider.enabled = true;

        private void DisableDoorCollider()
            => _boxCollider.enabled = false;
        
        private void WokeUpStaticEvent_OnWokeUp(object sender, EventArgs e)
        {
            EnableDoorCollider();
        }
    }
}
