using System;
using DG.Tweening;
using Interfaces;
using Misc;
using Tablet;
using UnityEngine;
using Utilities;
using Zenject;

namespace Environment.LaboratoryFirstLevel
{
    [RequireComponent(typeof(BoxCollider))]
    [DisallowMultipleComponent]
    public class Tablet : MonoBehaviour, IInteractable
    {
        public static event EventHandler OnAnyInteractedWithTablet;
        
        [Header("Settings")]
        [SerializeField] private float _rotationTimeInSeconds = .35f;
        [SerializeField] private float _moveTimeInSeconds = .35f;
        
        private bool _isInteractable = true;

        private Vector3 _initialPosition;
        private Quaternion _initialRotation;

        private BoxCollider _boxCollider;

        private Player.Player _player;

        [Inject]
        private void Construct(Player.Player player)
        {
            _player = player;
        }
        
        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
            
            _initialPosition = transform.position;
            _initialRotation = transform.rotation;
        }

        private void OnEnable()
        {
            PutDownStaticEvent.OnAnyTabletPutDown += OnAnyTabletPutDown;
        }

        private void OnDisable()
        {
            PutDownStaticEvent.OnAnyTabletPutDown -= OnAnyTabletPutDown;
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

            InputAllowance.DisableInput();
            PickedUpStaticEvent.Call(this);
            CrosshairDisplayStateChangedStaticEvent.Call(this, new CrosshairDisplayStateChangedEventArgs(false));
            
            OnAnyInteractedWithTablet?.Invoke(this, EventArgs.Empty);
            
            if (Camera.main != null) 
                transform.SetParent(Camera.main.transform);
            
            MoveToPlayer();
            RotateTowardsPlayer();
        }
        
        private void MoveToPlayer()
        {
            Transform itemHolderTransform = _player.GetItemHolderTransform();
            transform.DOLocalMove(itemHolderTransform.localPosition, _moveTimeInSeconds);
        }
        
        private void RotateTowardsPlayer()
        {
            transform.DOLocalRotateQuaternion(Quaternion.Euler(0f, -185f, 0f), _rotationTimeInSeconds);
        }

        private void PutBack()
        {
            CrosshairDisplayStateChangedStaticEvent.Call(this, new CrosshairDisplayStateChangedEventArgs(true));
            
            transform.SetParent(null);
            transform.DOKill();
            transform.DOMove(_initialPosition, _moveTimeInSeconds).OnComplete(() => _boxCollider.Enable());
            transform.DORotate(_initialRotation.eulerAngles, _rotationTimeInSeconds);
        }
        
        private void OnAnyTabletPutDown(object sender, EventArgs e)
        {
            PutBack();
        }
    }
}
