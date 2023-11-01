using DG.Tweening;
using UnityEngine;

namespace Scientist
{
    public class LookAt : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private Transform _neck;
        [SerializeField] private Transform _lookAtTarget;

        [Header("Settings")] 
        [SerializeField] private float _rangeToLookAtInMeters;
        
        [SerializeField] private float _horizontalLockDegree;
        [SerializeField] private float _verticalLockDegree;
        
        [SerializeField] private float _lookAtSpeed;

        private Quaternion _initialRotation;

        private void Awake()
        {
            _initialRotation = _neck.transform.rotation;
        }

        private void Update()
        {
            Vector3 direction = _lookAtTarget.position - transform.position;
            
            if (Vector3.Magnitude(direction) <= _rangeToLookAtInMeters)
            {
                float horizontalRotationClamped = 
                    Mathf.Clamp(direction.x, -_verticalLockDegree, _verticalLockDegree);
                float verticalRotationClamped =
                    Mathf.Clamp(direction.y, -_horizontalLockDegree, _horizontalLockDegree);

                Quaternion endRotation =
                    Quaternion.Euler(new Vector3(0f, horizontalRotationClamped, verticalRotationClamped));
                
                _neck.DORotateQuaternion(endRotation, _lookAtSpeed);
            }
            else
            {
                _neck.DORotateQuaternion(_initialRotation, _lookAtSpeed);
            }
        }
    }
}
