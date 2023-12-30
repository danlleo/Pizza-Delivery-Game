using System;
using DG.Tweening;
using Interfaces;
using Misc;
using Tablet;
using UnityEngine;
using Utilities;

namespace Environment.LaboratoryFirstLevel
{
    [RequireComponent(typeof(BoxCollider))]
    [DisallowMultipleComponent]
    public class Tablet : MonoBehaviour, IInteractable
    {
        [Header("Settings")]
        [SerializeField] private float _rotationTimeInSeconds = .35f;
        [SerializeField] private float _moveTimeInSeconds = .35f;
        
        private bool _isInteractable = true;

        private Vector3 _initialPosition;
        private Quaternion _initialRotation;

        private BoxCollider _boxCollider;
        
        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
            
            _initialPosition = transform.position;
            _initialRotation = transform.rotation;
        }

        private void OnEnable()
        {
            PutDownStaticEvent.OnTabletPutDown += OnAnyTabletPutDown;
        }

        public void Interact()
        {
            PickUp();
        }

        public string GetActionDescription()
        {
            return "Inspect tablet";
        }
        
        private void PickUp()
        {
            _boxCollider.Disable();

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
            CrosshairDisplayStateChangedStaticEvent.Call(this, new CrosshairDisplayStateChangedEventArgs(true));
            
            transform.DOKill();
            transform.SetParent(null);
            transform.DOMove(_initialPosition, _moveTimeInSeconds).OnComplete(() => _boxCollider.Enable());
            transform.DORotate(_initialRotation.eulerAngles, _rotationTimeInSeconds);
        }
        
        private void OnAnyTabletPutDown(object sender, EventArgs e)
        {
            PutBack();
        }
    }
}
