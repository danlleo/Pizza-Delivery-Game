using DG.Tweening;
using Misc;
using UnityEngine;

namespace Tablet
{
    [DisallowMultipleComponent]
    public class BloodWiper : MonoBehaviour
    {
        private Vector3 _initialPosition;
        
        public void Initialize(Vector3 initialPosition)
        {
            _initialPosition = initialPosition;
        }

        public void PickUp()
        {
            PickedUpStaticEvent.Call(this);
            CrosshairDisplayStateChangedStaticEvent.Call(this, new CrosshairDisplayStateChangedEventArgs(false));

            Transform itemHolderTransform = Player.Player.Instance.GetItemHolderTransform();

            if (Camera.main != null) 
                transform.SetParent(Camera.main.transform);
            
            RotateTowardsPlayer();
            //transform.DOLocalMove(itemHolderTransform.localPosition, .35f);
        }

        private void RotateTowardsPlayer()
        {
            
        }
    }
}
