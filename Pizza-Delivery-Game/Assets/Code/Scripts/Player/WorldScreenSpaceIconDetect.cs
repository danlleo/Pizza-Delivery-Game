using Interfaces;
using UnityEngine;

namespace Player
{
    public class WorldScreenSpaceIconDetect : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private Transform _detectPoint;
        
        [Header("Settings")]
        [SerializeField] private LayerMask _worldScreenSpaceIconMask;
        
        [Space(5)]
        [SerializeField] private float _detectDistance;
        [SerializeField] private float _detectRadius;
        
        private void Update()
        {
            var ray = new Ray(_detectPoint.position, _detectPoint.forward * _detectDistance);

            if (!Physics.SphereCast(ray, _detectRadius, out RaycastHit hit, _worldScreenSpaceIconMask))
                return;

            if (!hit.collider.TryGetComponent(out IWorldScreenSpaceIcon worldScreenSpaceIcon))
                return;
            
            
        }
    }
}