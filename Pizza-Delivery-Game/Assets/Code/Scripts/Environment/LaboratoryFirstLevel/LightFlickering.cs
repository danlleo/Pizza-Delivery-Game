using UnityEngine;
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
        [SerializeField] private float _flickeringDurationInSeconds = 0.1f;

        private float _timer;

        private void Update()
        {
            _timer += Time.deltaTime;

            if (!(_timer >= _flickeringDurationInSeconds)) return;
        
            _timer = 0f;
            _lightSource.intensity = Random.Range(_minIntensity, _maxIntensity);
        }
    }
}
