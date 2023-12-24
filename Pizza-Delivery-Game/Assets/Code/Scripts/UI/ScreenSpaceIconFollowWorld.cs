using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image))]
    [DisallowMultipleComponent]
    public class ScreenSpaceIconFollowWorld : MonoBehaviour
    {
        private Image _image;
        
        private Transform _lookAt;
        private Vector3 _offset;
        private Camera _mainCamera;
        
        private void Awake()
        {
            _image = GetComponent<Image>();
            _mainCamera = Camera.main;
            
            SetImageEnabledState(false);
        }

        private void Update()
        {
            Place();
        }

        public void InitializeAndDisplay(Transform lookAtTarget, Vector3 offset)
        {
            _lookAt = lookAtTarget;
            _offset = offset;

            Place();
            SetImageEnabledState(true);
        }

        private void Place()
        {
            Vector3 pos = _mainCamera.WorldToScreenPoint(_lookAt.position + _offset);
            
            if (transform.position != pos)
                transform.position = pos;
        }

        private void SetImageEnabledState(bool isEnabled)
            => _image.enabled = isEnabled;
    }
}
