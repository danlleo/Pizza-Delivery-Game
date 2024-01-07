using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Environment.LaboratoryFirstLevel
{
    public class LightFlickering : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField] private Light _lightSource;

        [Header("Settings")]
        [SerializeField] private float _minIntensity;
        [SerializeField] private float _maxIntensity = 1f;
        [FormerlySerializedAs("_flickeringDurationInSeconds")] [SerializeField] private float _minFlickeringDurationInSeconds = 0.1f;
        [SerializeField] private float _maxFlickeringDurationInSeconds = 0.1f;
        
        private float _randomTime;
        private float _timer;

        private void Awake()
        {
            SetRandomTime();
        }

        private void Update()
        {
            Flick();
        }

        private void Flick()
        {
            IncreaseTimer();

            if (!(_timer >= _randomTime)) return;
            
            ResetTimer();
            SetRandomTime();
            SetLightSourceIntensity();
        }

        private void SetLightSourceIntensity()
            => _lightSource.intensity = Random.Range(_minIntensity, _maxIntensity);
        
        private void SetRandomTime()
            => _randomTime = Random.Range(_minFlickeringDurationInSeconds, _maxFlickeringDurationInSeconds);
        
        private void IncreaseTimer()
            => _timer += Time.deltaTime;
        
        private void ResetTimer()
            => _timer = 0f;
    }
}