using UnityEngine;

namespace Share
{
    [DisallowMultipleComponent]
    public class CameraBreathEffect : MonoBehaviour
    {
        [SerializeField] private float _amplitude = 0.1f;  // Radius of circular movement
        [SerializeField] private float _frequency = 1f;    // Speed of movement

        private Vector3 _originalPosition;
        private float _randomAngle;

        private void Start()
        {
            _originalPosition = transform.position;
            _randomAngle = Random.Range(0f, 360f);
        }

        private void Update()
        {
            CircularBreathePosition();
        }

        private void CircularBreathePosition()
        {
            // Oscillate the camera's position in a circular motion
            float angle = _randomAngle + (Time.time * _frequency) * Mathf.Rad2Deg;
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * _amplitude;
            float y = Mathf.Sin(angle * Mathf.Deg2Rad) * _amplitude;
        
            transform.position = _originalPosition + new Vector3(x, y, 0);
        }
    }
}
