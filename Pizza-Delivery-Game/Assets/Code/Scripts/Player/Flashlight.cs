using System;
using DG.Tweening;
using Sounds.Audio;
using UnityEngine;

namespace Player
{
    [DisallowMultipleComponent]
    public class Flashlight : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private Transform _flashLightHolderTransform;
        [SerializeField] private Camera _camera;
        [SerializeField] private PlayerAudio _playerAudio;
        [SerializeField] private Light _lightSource;

        [Header("Settings")] 
        [SerializeField] private float _followDuration = 0.75f;
        [SerializeField] private bool _isEnabled = true;
        
        private bool _isOn;

        private void Awake()
        {
            _lightSource.enabled = false;
        }

        private void Update()
        {
            if (!_isEnabled) return;
            
            PlaceAtHolderPosition();
            FollowCameraWithSmooth();
        }

        private void PlaceAtHolderPosition()
        {
            _lightSource.transform.position = _flashLightHolderTransform.position;
        }
        
        private void FollowCameraWithSmooth()
        {
            _lightSource.transform.DORotateQuaternion(_camera.transform.rotation, _followDuration);
        }
        
        public void ToggleLight()
        {
            if (!_isEnabled) return;
            
            _isOn = !_isOn;
            _lightSource.enabled = _isOn;
            
            _playerAudio.PlayFlashLightSwitchSound(_isOn);
        }
    }
}
