using Interfaces;
using UnityEngine;

namespace Player
{
    public class Interact : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private Transform _raycastPointTransform;
        [SerializeField] private LayerMask _interactableLayerMask;
        
        [Header("Settings")]
        [SerializeField] private float _interactDistance;

        private void Update()
        {
            if (!Physics.Raycast(_raycastPointTransform.position, _raycastPointTransform.forward,
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
            Debug.DrawRay(_raycastPointTransform.position, _raycastPointTransform.forward * _interactDistance);
        }

#endif
    }
}
