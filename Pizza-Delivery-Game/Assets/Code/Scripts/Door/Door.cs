using System.Collections;
using UnityEngine;

namespace Door
{
    [DisallowMultipleComponent]
    public class Door : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _slideSpeed = 1f;
        [SerializeField] private bool _isLocked;
        
        private Vector3 _startPosition;
        
        private Coroutine _slideRoutine;

        private void Awake()
        {
            _startPosition = transform.position;
        }
        
        public void Unlock()
        {
            _isLocked = false;
        }
        
        public void Open()
        {
            if (_isLocked) return;

            _slideRoutine ??= StartCoroutine(SlideRoutine());
        }

        private IEnumerator SlideRoutine()
        {
            float duration = 1.0f / _slideSpeed;  // Total time to complete the slide
            float elapsed = 0;  // Time elapsed since the start of the slide
            float targetHeightMultiplyValue = 2.3f;
            
            Vector3 endPosition = _startPosition + Vector3.up * targetHeightMultiplyValue; // Assuming you want to slide the door up by 1 unit

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float fraction = elapsed / duration;  // Fraction of the total duration completed

                transform.position = Vector3.Lerp(_startPosition, endPosition, fraction);

                yield return null;  // Wait for the next frame
            }

            transform.position = endPosition;  // Ensure the door is exactly in the end position
        }

    }
}
