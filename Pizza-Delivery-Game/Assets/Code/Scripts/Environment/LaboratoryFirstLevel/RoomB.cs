using System;
using System.Collections;
using Enums.Keycards;
using Player;
using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    [DisallowMultipleComponent]
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

        private void Awake()
        {
            _defaultPosition = transform.position;
            _defaultRotation = transform.rotation;
        }

        private void OnEnable()
        {
            Keycard.OnAnyPickedUpKeycard += Keycard_OnAnyPickedUpKeycard;
            LaboratoryEntryTriggerArea.OnAnyEnteredLaboratoryEntryTriggerArea += LaboratoryEntryTriggerArea_OnAnyEnteredLaboratoryEntryTriggerArea;
        }

        private void OnDisable()
        {
            Keycard.OnAnyPickedUpKeycard -= Keycard_OnAnyPickedUpKeycard;
            LaboratoryEntryTriggerArea.OnAnyEnteredLaboratoryEntryTriggerArea -= LaboratoryEntryTriggerArea_OnAnyEnteredLaboratoryEntryTriggerArea;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out Player.Player player)) return;
            
            _player = player;
            _player.transform.SetParent(transform);
        }

        private void TeleportRoomToTargetPosition()
        {
            GravityPulldownEnableStateStaticEvent.Call(this, new GravityPulldownEnableStateStaticEventArgs(false));
            transform.localPosition = _targetTeleportPosition;
        }

        private void RotateRoomToTargetRotation()
        {
            transform.rotation = Quaternion.Euler(_targetRotation);
        }

        private void RotateRoomToDefaultRotation()
        {
            transform.rotation = _defaultRotation;
        }
        
        private void TeleportRoomToDefaultPosition()
        {
            transform.position = _defaultPosition;
        }

        private void UnlockLaboratoryEntryDoor()
        {
            _laboratoryEntryDoor.Unlock();
            _laboratoryEntryDoor.Open();
        }
        
        private void Keycard_OnAnyPickedUpKeycard(object sender, Keycard.OnAnyPickedUpKeycardEventArgs e)
        {
            if (e.KeycardType != KeycardType.KeycardA)
                return;
            
            UnlockLaboratoryEntryDoor();
            TeleportRoomToTargetPosition();
            RotateRoomToTargetRotation();
            StartCoroutine(DelayGravityRoutine());
        }
        
        private void LaboratoryEntryTriggerArea_OnAnyEnteredLaboratoryEntryTriggerArea(object sender, EventArgs e)
        {
            TeleportRoomToDefaultPosition();
            RotateRoomToDefaultRotation();
            Destroy(this);
        }

        private IEnumerator DelayGravityRoutine()
        {
            yield return new WaitForSeconds(1f);
            GravityPulldownEnableStateStaticEvent.Call(this, new GravityPulldownEnableStateStaticEventArgs(true));
        }
    }
}
