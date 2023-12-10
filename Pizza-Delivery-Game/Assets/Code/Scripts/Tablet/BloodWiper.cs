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

            Transform itemHolderTransform = Player.Player.Instance.GetItemHolderTransform();

            if (Camera.main != null) 
                transform.SetParent(Camera.main.transform);
            
            transform.DOLocalMove(itemHolderTransform.localPosition, _moveTimeInSeconds);
            RotateTowardsPlayer();
        }

        private void RotateTowardsPlayer()
        {
            Vector3 itemHolderPosition = Player.Player.Instance.GetItemHolderTransform().position;
            Vector3 directionToItemHolder = itemHolderPosition - transform.position;
            
            Quaternion lookRotation = Quaternion.LookRotation(directionToItemHolder);
            
            transform.DORotateQuaternion(lookRotation, _rotationTimeInSeconds);
        }
    }
}
