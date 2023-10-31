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

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            _ui.InspectableObjectOpeningEvent.Event += InspectableObjectOpening_Event;
        }

        private void OnDisable()
        {
            _ui.InspectableObjectOpeningEvent.Event -= InspectableObjectOpening_Event;
        }

        private void InspectableObjectOpening_Event(object sender, InspectableObjectOpeningEventArgs e)
        {
            PlaySound(_audioSource, _itemObtained, 0.625f);
        }
    }
}
