using UnityEngine;

namespace UI
{
    public class FollowWorld : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private Transform _lookAt;

        [Header("Settings")]
        [SerializeField] private Vector3 _offset;

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
    }
}
