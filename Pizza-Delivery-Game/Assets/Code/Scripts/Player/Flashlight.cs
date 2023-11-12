using System.Collections;
using DG.Tweening;
using Enums.Player;
using Sounds.Audio;
using UnityEngine;

namespace Player
{
    [DisallowMultipleComponent]
    public class Flashlight : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField] private Inventory.Inventory _inventory;
        [SerializeField] private Inventory.ItemSO _item;
        [SerializeField] private Transform _flashLightHolderTransform;
        [SerializeField] private Camera _camera;
        [SerializeField] private PlayerAudio _playerAudio;
        [SerializeField] private Light _lightSourcePrefab;

        [Header("Settings")] 
        [SerializeField] private float _followDuration = 0.75f;
        [SerializeField] private bool _isEnabled = true;
        
        private bool _isOn;

        private Light _lightSource;
        private Coroutine _flickeringRoutine;
        
        private void Awake()
        {
            _lightSource = Instantiate(_lightSourcePrefab, transform.position, Quaternion.identity);
            _lightSource.enabled = false;
        }

        private void Update()
        {
            if (!_isEnabled) return;
            
            PlaceAtHolderPosition();
            FollowCameraWithSmooth();
        }

        public void ToggleLight()
        {
            if (!_isEnabled) return;
            if (!_inventory.HasItem(_item)) return;
            if (Player.Instance.State != PlayerState.Exploring) return;
            if (_flickeringRoutine != null)
                StopCoroutine(_flickeringRoutine);
            
            _isOn = !_isOn;
            _lightSource.enabled = _isOn;

            if (_isOn)
                StartCoroutine(FlickeringRoutine());
            
            _playerAudio.PlayFlashLightSwitchSound(_isOn);
        }
        
        private void PlaceAtHolderPosition()
        {
            _lightSource.transform.position = _flashLightHolderTransform.position;
        }
        
        private void FollowCameraWithSmooth()
        {
            _lightSource.transform.DORotateQuaternion(_camera.transform.rotation, _followDuration);
        }

        private IEnumerator FlickeringRoutine()
        {
            var timeElapsed = 0f;
            var reachMaxIntensityTime = 0.0215f;
            var reachMinIntensityTime = 0.0215f;
            var stayTimeInSeconds = 0.05f;
            
            while (timeElapsed <= reachMaxIntensityTime)
            {
                timeElapsed += Time.deltaTime;
                float t = timeElapsed / reachMaxIntensityTime;

                _lightSource.intensity = Mathf.Lerp(0f, 1f, t);
                yield return null;
            }

            timeElapsed = 0f;

            yield return new WaitForSeconds(stayTimeInSeconds);
            
            while (timeElapsed <= reachMinIntensityTime)
            {
                timeElapsed += Time.deltaTime;
                float t = timeElapsed / reachMaxIntensityTime;

                _lightSource.intensity = Mathf.Lerp(1f, 0f, t);
                yield return null;
            }
            
            timeElapsed = 0f;
            
            yield return new WaitForSeconds(stayTimeInSeconds);
            
            while (timeElapsed <= reachMaxIntensityTime)
            {
                timeElapsed += Time.deltaTime;
                float t = timeElapsed / reachMaxIntensityTime;

                _lightSource.intensity = Mathf.Lerp(0f, 1f, t);
                yield return null;
            }
        }
    }
}
