using Interfaces;
using UI;
using UnityEngine;

namespace Player
{
    public class WorldScreenSpaceIconDetect : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private UI.UI _ui;
        [SerializeField] private Transform _detectPoint;
        
        [Header("Settings")]
        [SerializeField] private LayerMask _worldScreenSpaceIconMask;
        
        [Space(5)]
        [SerializeField] private float _detectDistance;
        [SerializeField] private float _detectRadius;
        
        private Camera _camera;
        private RaycastHit _hitInfo;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            Plane[] cameraPlanes = GeometryUtility.CalculateFrustumPlanes(_camera);
            Collider[] hitColliders =
                Physics.OverlapSphere(_detectPoint.position, _detectRadius, _worldScreenSpaceIconMask);

            foreach (Collider hitCollider in hitColliders)
            {
                if (!hitCollider.TryGetComponent(out IWorldScreenSpaceIcon worldScreenSpaceIcon))
                    continue;

                bool isInView = GeometryUtility.TestPlanesAABB(cameraPlanes, hitCollider.bounds);

                if (!isInView)
                {
                    print("I don't see it");
                    continue;
                }

                _ui.WorldScreenSpaceIconDetectedEvent.Call(this,
                    new WorldScreenSpaceIconDetectedEventArgs(worldScreenSpaceIcon));
                
                print("I see it");
            }
        }
    }
}