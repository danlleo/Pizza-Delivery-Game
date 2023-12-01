using System.Collections;
using Interfaces;
using Player.Inventory;
using UnityEngine;

namespace Door
{
    [DisallowMultipleComponent]
    public class Door : MonoBehaviour, IInteractable
    {
        [Header("Settings")]
        [SerializeField] private float _rotateSpeed = 1f;
        [SerializeField] private float _rotationAmount = 90f;
        [SerializeField] private float _forwardDirection;
        [SerializeField] private bool _isLocked;
        
        private Vector3 _startRotation;
        
        private Coroutine _rotationRoutine;

        private void Awake()
        {
            Transform cachedTransform = transform;
            
            _startRotation = cachedTransform.rotation.eulerAngles;
        }

        public void Unlock()
        {
            _isLocked = false;
        }
        
        public void Interact()
        {
            Open(Player.Player.Instance.transform.forward);
        }

        public string GetActionDescription()
        {
            return "Open";
        }

        private void Open(Vector3 ownerForward)
        {
            if (_isLocked) return;

            _rotationRoutine ??= StartCoroutine(RotationRoutine(ownerForward));
        }

        private IEnumerator RotationRoutine(Vector3 ownerForward)
        {
            float dot = Vector3.Dot(transform.forward, ownerForward.normalized);
            float timeToRotate = 1f;
            float timer = 0f;
            
            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = Quaternion.Euler(dot >= _forwardDirection
                ? new Vector3(_startRotation.x, _startRotation.y - _rotationAmount, 0f)
                : new Vector3(_startRotation.x, _startRotation.y + _rotationAmount, 0f));

            while (timer < timeToRotate)
            {
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, timer);
                yield return null;
                timer += Time.deltaTime * _rotateSpeed;
            }
        }
    }
}
