using System;
using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    public class RoomB : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Vector3 _targetTeleportPosition;

        private Player.Player _player;

        private Vector3 _defaultPosition;

        private bool _wasTeleported;
        
        private void Awake()
        {
            _defaultPosition = transform.position;
        }

        private void OnEnable()
        {
            PickedUpKeycardAStaticEvent.OnAnyPickedUpKeycardA += AnyPickedUpKeycardA;
        }

        private void OnDisable()
        {
            PickedUpKeycardAStaticEvent.OnAnyPickedUpKeycardA -= AnyPickedUpKeycardA;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out Player.Player player)) return;
            
            _player = player;
            _player.transform.SetParent(transform);
            
            print("Player detected");
        }

        private void OnTriggerExit(Collider other)
        {
            if (!_wasTeleported) return;
        }

        private void TeleportToTargetPosition()
        {
            _wasTeleported = true;
            transform.localPosition = _targetTeleportPosition;
        }

        private void TeleportToDefaultPosition()
        {
            transform.position = _defaultPosition;
        }
        
        private void AnyPickedUpKeycardA(object sender, EventArgs e)
        {
            TeleportToTargetPosition();
        }
    }
}
