using Sounds.Audio;
using UnityEngine;

namespace Player
{
    public class Flashlight : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private PlayerAudio _playerAudio;
        [SerializeField] private Light _lightSource;

        [Header("Settings")]
        [SerializeField] private bool _isEnabled = true;
        [SerializeField] private bool _isOn;
        
        public void ToggleLight()
        {
            if (!_isEnabled) return;
            
            _isOn = !_isOn;
            _lightSource.enabled = _isOn;
            
            _playerAudio.PlayFlashLightSwitchSound(_isOn);
        }
    }
}
