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
        [SerializeField] private float _followDurationDelayInSeconds = 0.75f;
        
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
            //if (!_inventory.HasItem(_item)) return;
            if (Player.Instance.State != PlayerState.Exploring) return;
            if (_flickeringRoutine != null)
                StopCoroutine(_flickeringRoutine);
            
            _isOn = !_isOn;
            _lightSource.enabled = _isOn;

            if (_isOn)
                FlickerIfChanceMet();
            
            _playerAudio.PlayFlashLightSwitchSound(_isOn);
        }

        private void FlickerIfChanceMet()
        {
            float randomChance = Random.Range(1, 100);

            print(randomChance);
            
            if (_chanceToBeginFlickering == 0)
            {
                StartCoroutine(FlickeringRoutine(false));
                return;
            }
            
            if (randomChance >= _chanceToBeginFlickering)
            {
                StartCoroutine(FlickeringRoutine(true));
                return;
            }
            
            StartCoroutine(FlickeringRoutine(false));
        }
        
        private void PlaceAtHolderPosition()
        {
            _lightSource.transform.position = _flashLightHolderTransform.position;
        }
        
        private void FollowCameraWithSmooth()
        {
            _lightSource.transform.DORotateQuaternion(_camera.transform.rotation, _followDurationDelayInSeconds);
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
                
                timeElapsed = 0f;
                flickeringCount++;
            }
            
            while (timeElapsed <= targetTime)
            {
                timeElapsed += Time.deltaTime;
                float t = timeElapsed / targetTime;

                _lightSource.intensity = Mathf.Lerp(_lightSource.intensity, 1f, t);
                yield return null;
            }
        }
    }
}
