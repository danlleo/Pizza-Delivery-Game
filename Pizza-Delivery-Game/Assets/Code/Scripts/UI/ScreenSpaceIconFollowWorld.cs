using UnityEngine;

namespace UI
{
    public class ScreenSpaceIconFollowWorld : MonoBehaviour
    {
        private Transform _lookAt;
        private Vector3 _offset;

        private Camera _mainCamera;
        
        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            Vector3 pos = _mainCamera.WorldToScreenPoint(_lookAt.position + _offset);
            
            if (transform.position != pos)
                transform.position = pos;
        }

        public void Initialize(Transform lookAtTarget, Vector3 offset)
        {
            _lookAt = lookAtTarget;
            _offset = offset;
        }
    }
}
