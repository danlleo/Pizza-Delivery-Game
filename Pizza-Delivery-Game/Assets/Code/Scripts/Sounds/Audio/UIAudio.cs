using System;
using UI.InspectableObject;
using UnityEngine;

namespace Sounds.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class UIAudio : AudioPlayer
    {
        [Header("External references")] 
        [SerializeField] private UI.UI _ui;
        
        [Header("Settings")]
        [SerializeField] private AudioClip _itemObtained;
        [SerializeField] private AudioClip _confirm;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            _ui.InspectableObjectOpeningEvent.Event += InspectableObjectOpening_Event;
            _ui.ConfirmEvent.Event += Confirm_Event;
        }

        private void OnDisable()
        {
            _ui.InspectableObjectOpeningEvent.Event -= InspectableObjectOpening_Event;
            _ui.ConfirmEvent.Event -= Confirm_Event;
        }

        private void InspectableObjectOpening_Event(object sender, InspectableObjectOpeningEventArgs e)
        {
            PlaySound(_audioSource, _itemObtained, 0.625f);
        }
        
        private void Confirm_Event(object sender, EventArgs e)
        {
            PlaySound(_audioSource, _confirm, 0.48f);
        }
    }
}
