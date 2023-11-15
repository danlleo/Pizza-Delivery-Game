using Enums.Player;
using Interfaces;
using UnityEngine;

namespace Player
{
    [DisallowMultipleComponent]
    public class Interact : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField] private Player _player;
        [SerializeField] private Transform _raycastPointTransform;
        [SerializeField] private LayerMask _interactableLayerMask;
        
        [Header("Settings")]
        [SerializeField] private float _interactDistance;
        
        private RaycastHit _hit;
        
        private void Update()
        {
            if (!Physics.Raycast(_raycastPointTransform.position, _raycastPointTransform.forward,
                    out _hit, _interactDistance, _interactableLayerMask))
            {
                _player.HoveringOverInteractableEvent.Call(_player, new HoveringOverInteractableEventArgs(false));
                return;
            }

            if (!_hit.collider.TryGetComponent(out IInteractable interactable))
            {
                _player.HoveringOverInteractableEvent.Call(_player, new HoveringOverInteractableEventArgs(false));
                return;
            }
            
            _player.HoveringOverInteractableEvent.Call(_player, new HoveringOverInteractableEventArgs(true, interactable.GetActionDescription()));
        }

        public void TryInteract()
        {
            if (_player.State != PlayerState.Exploring)
                return;
            
            if (_hit.collider == null) return;
            if (!_hit.collider.TryGetComponent(out IInteractable interactable)) return;
            
            interactable.Interact();
        }
    }
}
