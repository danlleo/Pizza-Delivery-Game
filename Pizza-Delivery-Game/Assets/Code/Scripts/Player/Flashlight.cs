using System.Collections;
using Enums.Player;
using Player.Inventory;
using Sounds.Audio;
using UnityEngine;

namespace Player
{
    [DisallowMultipleComponent]
    public class Flashlight : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private CharacterControllerMovement _characterController;
        [SerializeField] private Inventory.Inventory _inventory;
        [SerializeField] private ItemSO _item;
        [SerializeField] private Transform _flashLightHolderTransform;
        [SerializeField] private Camera _camera;
        [SerializeField] private PlayerAudio _playerAudio;
        [SerializeField] private Light _lightSourcePrefab;
        
        [Header("Settings")] 
        [SerializeField] private float _flashlightFollowSpeed = 0.75f;
        [SerializeField] private float _lightFadingTimeInSeconds = 0.25f;
        
        [Space(5)]
        [SerializeField, Range(0f, 100f)] private float _chanceToBeginFlickering = 25f;
        [SerializeField, Range(1, 3)] private int _maxFlickeringCount = 4;
        [SerializeField] private float _stayBetweenTransitionTimeInSeconds = 0.05f;
        [SerializeField] private float _minReachTargetIntensityTime = 0.0215f;
        [SerializeField] private float _maxReachTargetIntensityTime = 0.0515f;
        [SerializeField] private float _minLowIntensityValue = 0.1f;
        [SerializeField] private float _maxLowIntensityValue = .225f;
        [SerializeField] private float _minHighIntensityValue = 0.85f; 
        [SerializeField] private float _maxHighIntensityValue = 1.4f;

        [Space(5)] 
        [SerializeField] private float _swingTimeInSeconds = 0.5f;
        [Tooltip("Use negative value")]
        [SerializeField] private float _swingMinimumHeightValue = -.5f;
        [Tooltip("Use positive value")]
        [SerializeField] private float _swingMaximumHeightValue = .5f;
        
        [Space(5)]
        [SerializeField] private bool _isEnabled = true;
        
        private bool _isOn;

        private GameObject _parent;
        private Light _lightSource;

        private Coroutine _resetLightSourcePositionRoutine;
        private Coroutine _flickeringRoutine;
        private Coroutine _lightFadingRoutine;
        
        private void Awake()
        {
            _parent = new GameObject("FlashlightParent");
            _lightSource = Instantiate(_lightSourcePrefab, Vector3.zero, Quaternion.identity);
            _lightSource.enabled = false;
            _lightSource.transform.SetParent(_parent.transform);
        }

        private void Update()
        {
            if (!_isEnabled) return;
            
            PlaceAtHolderPosition();
            FollowCameraWithSmooth();
            Swing();
        }

        public void ToggleLight()
        {
            if (!_isEnabled) return;
            if (!_inventory.HasItem(_item)) return;
            if (Player.Instance.State != PlayerState.Exploring) return;
            
            if (_flickeringRoutine != null)
                StopCoroutine(_flickeringRoutine);
            
            if (_lightFadingRoutine != null)
                StopCoroutine(_lightFadingRoutine);
            
            _isOn = !_isOn;

            if (_isOn)
            {
                FlickerIfChanceMet();
                _lightSource.enabled = true;
            }
            else
            {
                _lightFadingRoutine = StartCoroutine(FadingLightRoutine());
            }
            
            _playerAudio.PlayFlashLightSwitchSound(_isOn);
        }

        private void FlickerIfChanceMet()
        {
            float randomChance = Random.Range(1, 101);

            if (_chanceToBeginFlickering == 0)
            {
                _flickeringRoutine = StartCoroutine(FlickeringRoutine(false));
                return;
            }
            
            if (randomChance <= _chanceToBeginFlickering)
            {
                _flickeringRoutine = StartCoroutine(FlickeringRoutine(true));
                return;
            }
            
            _flickeringRoutine = StartCoroutine(FlickeringRoutine(false));
        }
        
        private void PlaceAtHolderPosition()
        {
            _parent.transform.position = _flashLightHolderTransform.position;
        }
        
        private void FollowCameraWithSmooth()
        {
            _parent.transform.rotation = Quaternion.Slerp(_parent.transform.rotation, _camera.transform.rotation,
                _flashlightFollowSpeed * Time.deltaTime);
        }

        private void Swing()
        {
            if (!_characterController.IsMoving)
            {
                if (_resetLightSourcePositionRoutine != null)
                {
                    StopCoroutine(_resetLightSourcePositionRoutine);
                    _resetLightSourcePositionRoutine = null;
                }

                Vector3 endSwingPosition = Vector3.up *
                                           (Mathf.PingPong(Time.time, 2.0f) *
                                            (_swingMaximumHeightValue - _swingMinimumHeightValue) +
                                            _swingMinimumHeightValue);

                _lightSource.transform.localPosition = Vector3.Lerp(_lightSource.transform.localPosition,
                    endSwingPosition, Time.deltaTime);
                
                return;
            }

            _resetLightSourcePositionRoutine ??= StartCoroutine(ResetLightSourcePositionRoutine());
        }

        private IEnumerator ResetLightSourcePositionRoutine()
        {
            var timeElapsed = 0f;
            
            while (timeElapsed <= _swingTimeInSeconds)
            {
                timeElapsed += Time.deltaTime;
                
                float t = timeElapsed / _swingTimeInSeconds;

                _lightSource.transform.localPosition =
                    Vector3.Lerp(_lightSource.transform.localPosition, Vector3.zero, t);

                yield return null;
            }

            _lightSource.transform.localPosition = Vector3.zero;
        }
        
        private IEnumerator FadingLightRoutine()
        {
            var timeElapsed = 0f;

            while (timeElapsed <= _lightFadingTimeInSeconds)
            {
                timeElapsed += Time.deltaTime;

                float t = timeElapsed / _lightFadingTimeInSeconds;

                _lightSource.intensity = Mathf.Lerp(_lightSource.intensity, 0f, t);

                yield return null;
            }

            _lightSource.enabled = false;
        }
        
        private IEnumerator FlickeringRoutine(bool shouldFlicker)
        {
            var timeElapsed = 0f;

            float targetTime = _minReachTargetIntensityTime;
            
            int flickeringCount = 0;
            int randomMaxFlickeringCount = shouldFlicker ? Random.Range(1, _maxFlickeringCount) : 1;
            
            while (flickeringCount < randomMaxFlickeringCount)
            {
                while (timeElapsed <= targetTime)
                {
                    timeElapsed += Time.deltaTime;

                    float t = timeElapsed / targetTime;

                    float randomHighIntensityValue = Random.Range(_minHighIntensityValue, _maxHighIntensityValue);
                    float randomLowIntensityValue = Random.Range(_minLowIntensityValue, _maxLowIntensityValue);
                    
                    float lerpFrom = flickeringCount % 2 == 0 ? randomLowIntensityValue : randomHighIntensityValue;
                    float lerpTo = flickeringCount % 2 == 0 ? randomHighIntensityValue : randomLowIntensityValue;
                    
                    _lightSource.intensity = Mathf.Lerp(lerpFrom, lerpTo, t);
                    yield return null;
                }
                
                yield return new WaitForSeconds(_stayBetweenTransitionTimeInSeconds);
                
                if (shouldFlicker)
                    targetTime = Random.Range(_minReachTargetIntensityTime, _maxReachTargetIntensityTime);
                
                if (flickeringCount > 0)
                    _playerAudio.PlayFlashLightFlickSound();
                
                timeElapsed = 0f;
                flickeringCount++;
            }
            
            while (timeElapsed <= targetTime)
            {
                timeElapsed += Time.deltaTime;
                float t = timeElapsed / targetTime;

                _lightSource.intensity = Mathf.Lerp(_lightSource.intensity, _maxHighIntensityValue, t);
                yield return null;
            }
        }
    }
}
