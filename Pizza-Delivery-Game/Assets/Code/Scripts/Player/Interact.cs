using Interfaces;
using UnityEngine;

namespace Player
{
    public class Interact : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField] private LayerMask _interactableLayerMask;
        [SerializeField] private Camera _playerCamera;
        
        [Header("Settings")]
        [SerializeField] private float _interactDistance;

        private void Update()
        {
            if (!Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.forward,
                    out RaycastHit hit, _interactDistance, _interactableLayerMask))
                return;

            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
            }
        }
        
#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Debug.DrawLine(_playerCamera.transform.position, _playerCamera.transform.forward * _interactDistance, Color.green);
        }

#endif
    }
}
