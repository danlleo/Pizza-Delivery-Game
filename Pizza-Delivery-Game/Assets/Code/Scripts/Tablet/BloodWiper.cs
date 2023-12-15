using System.Collections;
using DG.Tweening;
using Misc;
using UnityEngine;

namespace Tablet
{
    [DisallowMultipleComponent]
    public class BloodWiper : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _rotationTimeInSeconds = .35f;
        [SerializeField] private float _moveTimeInSeconds = .35f;
        [SerializeField, Range(0f, 3f)] private float _timeBeforeCanWipeInSeconds = 1f; 
        
        private Vector3 _initialPosition;
        private Quaternion _initialRotation;

        private bool _canWipe;

        public void Initialize(Vector3 initialPosition, Quaternion initialRotation)
        {
            _initialPosition = initialPosition;
            _initialRotation = initialRotation;
        }

        public void PickUp()
        {
            PickedUpStaticEvent.Call(this);
            CrosshairDisplayStateChangedStaticEvent.Call(this, new CrosshairDisplayStateChangedEventArgs(false));
            CursorLockStateChangedStaticEvent.Call(this, new CursorLockStateChangedStaticEventArgs(false));
            
            if (Camera.main != null) 
                transform.SetParent(Camera.main.transform);
            
            MoveToPlayer();
            RotateTowardsPlayer();
            StartCoroutine(WaitUntilCanWipeRoutine());
        }

        private void MoveToPlayer()
        {
            Transform itemHolderTransform = Player.Player.Instance.GetItemHolderTransform();
            transform.DOLocalMove(itemHolderTransform.localPosition, _moveTimeInSeconds);
        }
        
        private void RotateTowardsPlayer()
        {
            transform.DOLocalRotateQuaternion(Quaternion.Euler(0f, -185f, 0f), _rotationTimeInSeconds);
        }

        private void PutBack()
        {
            transform.DOMove(_initialPosition, _moveTimeInSeconds);
            transform.DORotate(_initialRotation.eulerAngles, _rotationTimeInSeconds);
        }

        private void Wipe()
        {
            if (!_canWipe) return;
        }

        private IEnumerator WaitUntilCanWipeRoutine()
        {
            yield return new WaitForSeconds(_timeBeforeCanWipeInSeconds);
            _canWipe = true;
        }
    }
}
