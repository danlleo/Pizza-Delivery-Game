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
        [SerializeField] private AudioClip _objectiveSet;
        
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            _ui.InspectableObjectOpeningEvent.Event += InspectableObjectOpening_Event;
            _ui._inspectableObjectConfirmEvent.Event += InspectableObjectConfirmEvent;
            _ui.OnObjectiveUpdated.Event += OnObjectiveUpdated;
        }

        private void OnDisable()
        {
            _ui.InspectableObjectOpeningEvent.Event -= InspectableObjectOpening_Event;
            _ui._inspectableObjectConfirmEvent.Event -= InspectableObjectConfirmEvent;
            _ui.OnObjectiveUpdated.Event -= OnObjectiveUpdated;
        }

        private void InspectableObjectOpening_Event(object sender, InspectableObjectOpeningEventArgs e)
        {
            PlaySound(_audioSource, _itemObtained, 0.625f);
        }
        
        private void InspectableObjectConfirmEvent(object sender, EventArgs e)
        {
            PlaySound(_audioSource, _confirm, 0.48f);
        }
        
        private void OnObjectiveUpdated(object sender, EventArgs e)
        {
            PlaySound(_audioSource, _objectiveSet, 0.5f);
        }
    }
}
