using System.Collections;
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
        [SerializeField] private float _detectRadius;
        
        private Camera _camera;
        
        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Start()
        {
            StartCoroutine(ScanRoutine());
        }

        private IEnumerator ScanRoutine()
        {
            var delay = new WaitForSeconds(0.1f);

            while (true)
            {
                yield return delay;
                Scan();
            }
        }
        
        private void Scan()
        {
            Plane[] cameraPlanes = GeometryUtility.CalculateFrustumPlanes(_camera);
            Collider[] hitColliders =
                Physics.OverlapSphere(_detectPoint.position, _detectRadius, _worldScreenSpaceIconMask);

            if (hitColliders.Length == 0)
            {
                _ui.WorldScreenSpaceIconLostAllEvent.Call(this);
                return;
            }
            
            foreach (Collider hitCollider in hitColliders)
            {
                if (!hitCollider.TryGetComponent(out IWorldScreenSpaceIcon worldScreenSpaceIcon))
                {
                    _ui.WorldScreenSpaceIconLostEvent.Call(this,
                        new WorldScreenSpaceIconLostEventArgs(worldScreenSpaceIcon));
                    
                    continue;
                }

                bool isInView = GeometryUtility.TestPlanesAABB(cameraPlanes, hitCollider.bounds);

                if (!isInView)
                {
                    _ui.WorldScreenSpaceIconLostEvent.Call(this,
                        new WorldScreenSpaceIconLostEventArgs(worldScreenSpaceIcon));

                    continue;
                }

                Vector3 directionToCollider = (hitCollider.transform.position - _detectPoint.position).normalized;

                if (!Physics.Raycast(_detectPoint.position, directionToCollider, out RaycastHit raycastHit, float.MaxValue))
                {
                    _ui.WorldScreenSpaceIconLostEvent.Call(this,
                        new WorldScreenSpaceIconLostEventArgs(worldScreenSpaceIcon));

                    continue;
                }

                if (!raycastHit.collider.TryGetComponent(out IWorldScreenSpaceIcon _))
                {
                    _ui.WorldScreenSpaceIconLostEvent.Call(this,
                        new WorldScreenSpaceIconLostEventArgs(worldScreenSpaceIcon));

                    continue;
                }
                    
                _ui.WorldScreenSpaceIconDetectedEvent.Call(this,
                    new WorldScreenSpaceIconDetectedEventArgs(worldScreenSpaceIcon));
            }
        }
    }
}