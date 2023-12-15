using DG.Tweening;
using Misc;
using UnityEngine;

namespace Tablet
{
    [DisallowMultipleComponent]
    public class BloodWiper : MonoBehaviour
    {
        [SerializeField] private float _rotationTimeInSeconds = .35f;
        [SerializeField] private float _moveTimeInSeconds = .35f;
        
        private Vector3 _initialPosition;
        private Quaternion _initialRotation;
        
        public void Initialize(Vector3 initialPosition, Quaternion initialRotation)
        {
            _initialPosition = initialPosition;
            _initialRotation = initialRotation;
        }

        public void PickUp()
        {
            PickedUpStaticEvent.Call(this);
            CrosshairDisplayStateChangedStaticEvent.Call(this, new CrosshairDisplayStateChangedEventArgs(false));
            
            if (Camera.main != null) 
                transform.SetParent(Camera.main.transform);
            
            MoveToPlayer();
            RotateTowardsPlayer();
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
    }
}
