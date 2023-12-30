using UnityEngine;

namespace WorldScreenSpaceIcon
{
    public class WorldScreenSpaceIconDetect : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private Transform _detectPoint;

        [Header("Settings")] 
        [SerializeField] private LayerMask _worldScreenSpaceIconMask;

        [Space(5)] 
        [SerializeField] private float _detectRadius;
        [SerializeField] private bool _isEnabled = true;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            Scan();
        }

        private void Scan()
        {
            if (!_isEnabled) return;

            Plane[] cameraPlanes = GeometryUtility.CalculateFrustumPlanes(_camera);
            Collider[] hitColliders =
                Physics.OverlapSphere(_detectPoint.position, _detectRadius, _worldScreenSpaceIconMask);

            if (hitColliders.Length == 0)
            {
                this.CallWorldScreenSpaceIconLostAllStaticEvent();
                return;
            }

            foreach (Collider hitCollider in hitColliders)
            {
                if (!hitCollider.TryGetComponent(out WorldScreenSpaceIcon worldScreenSpaceIcon)) continue;

                bool isInView = GeometryUtility.TestPlanesAABB(cameraPlanes, hitCollider.bounds);

                if (!isInView)
                {
                    worldScreenSpaceIcon.CallWorldScreenSpaceIconLostStaticEvent();

                    continue;
                }

                Vector3 directionToCollider = (hitCollider.transform.position - _detectPoint.position).normalized;

                if (!Physics.Raycast(_detectPoint.position, directionToCollider, out RaycastHit raycastHit,
                        float.MaxValue))
                {
                    worldScreenSpaceIcon.CallWorldScreenSpaceIconLostStaticEvent();

                    continue;
                }

                if (!raycastHit.collider.TryGetComponent(out WorldScreenSpaceIcon _))
                {
                    worldScreenSpaceIcon.CallWorldScreenSpaceIconLostStaticEvent();

                    continue;
                }

                worldScreenSpaceIcon.CallWorldScreenSpaceIconDetectedStaticEvent();
            }
        }
    }
}