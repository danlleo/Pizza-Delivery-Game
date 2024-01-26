using System;
using DataPersistence;
using Enums.Scenes;
using Environment.Bedroom.PC;
using Interfaces;
using Misc;
using Misc.Loader;
using Sounds.Audio;
using UI.Crossfade;
using UnityEngine;
using Utilities;
using Zenject;

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
        private Crossfade _crossfade;

        private bool _isPlayerNaked;
        
        [Inject]
        private void Construct(Crossfade crossfade)
        {
            _crossfade = crossfade;
        }
        
        private void Awake()
        {
            _interactableCollider = GetComponent<BoxCollider>();
            _isPlayerNaked = true;
            
            DisableDoorInteractCollider();
        }

        private void OnEnable()
        {
            WokeUpStaticEvent.OnWokeUp += WokeUpStaticEvent_OnWokeUp;
            PC.OnAnyGotDressed.Event += OnAnyGotDressed;
        }

        private void OnDisable()
        {
            WokeUpStaticEvent.OnWokeUp -= WokeUpStaticEvent_OnWokeUp;
            PC.OnAnyGotDressed.Event -= OnAnyGotDressed;
        }

        public void Interact()
        {
            if (_isPlayerNaked)
            {
                OnAnyTriedLeaveNaked.Call(this);
                return;
            }
            
            _crossfade
                .FadeIn(InputAllowance.DisableInput, () => Loader.Load(Scene.OutdoorComicScene), 1.5f);
           
            _bedroomAudio.PlayDoorOpenSound();
            OnAnyLeftBedroom.Call(this);
            OpenedDoorStaticEvent.Call(this);
            SaveStaticEvent.Call(this);
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
        
        private void OnAnyGotDressed(object sender, EventArgs e)
        {
            _isPlayerNaked = false;
        }
    }
}
