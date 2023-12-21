using System;
using Player;
using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    public class RoomB : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField] private Door.Door _laboratoryEntryDoor;

        [Header("Settings")]
        [SerializeField] private Vector3 _targetTeleportPosition;
        [SerializeField] private Vector3 _targetRotation;
        
        private Player.Player _player;

        private Vector3 _defaultPosition;
        private Quaternion _defaultRotation;
        
        private bool _wasTeleported;
        
        private void Awake()
        {
            _defaultPosition = transform.position;
            _defaultRotation = transform.rotation;
        }

        private void OnEnable()
        {
            PickedUpKeycardAStaticEvent.OnAnyPickedUpKeycardA += AnyPickedUpKeycardA;
            EnteredLaboratoryEntryTriggerAreaStaticEvent.OnAnyEnteredLaboratoryEntryTriggerArea += OnAnyEnteredLaboratoryEntryTriggerArea;
        }

        private void OnDisable()
        {
            PickedUpKeycardAStaticEvent.OnAnyPickedUpKeycardA -= AnyPickedUpKeycardA;
            EnteredLaboratoryEntryTriggerAreaStaticEvent.OnAnyEnteredLaboratoryEntryTriggerArea -= OnAnyEnteredLaboratoryEntryTriggerArea;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out Player.Player player)) return;
            
            _player = player;
            _player.transform.SetParent(transform);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!_wasTeleported) return;
            GravityPulldownEnableStateStaticEvent.Call(this, new GravityPulldownEnableStateStaticEventArgs(true));
        }

        private void TeleportToTargetPosition()
        {
            _wasTeleported = true;
            GravityPulldownEnableStateStaticEvent.Call(this, new GravityPulldownEnableStateStaticEventArgs(false));
            transform.position = _targetTeleportPosition;
        }

        private void RotateToTargetRotation()
        {
            transform.rotation = Quaternion.Euler(_targetRotation);
        }

        private void RotateToDefaultRotation()
        {
            transform.rotation = _defaultRotation;
        }
        
        private void TeleportToDefaultPosition()
        {
            transform.position = _defaultPosition;
        }

        private void UnlockLaboratoryEntryDoor()
        {
            _laboratoryEntryDoor.Unlock();
            _laboratoryEntryDoor.Open();
        }
        
        private void AnyPickedUpKeycardA(object sender, EventArgs e)
        {
            UnlockLaboratoryEntryDoor();
            TeleportToTargetPosition();
            RotateToTargetRotation();
        }
        
        private void OnAnyEnteredLaboratoryEntryTriggerArea(object sender, EventArgs e)
        {
            TeleportToDefaultPosition();
            RotateToDefaultRotation();
            
            Destroy(this);
        }
    }
}
