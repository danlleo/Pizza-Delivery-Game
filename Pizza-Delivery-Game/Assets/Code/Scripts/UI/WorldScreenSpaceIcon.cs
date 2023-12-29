using UnityEngine;

namespace UI
{
    public struct WorldScreenSpaceIcon
    {
        public readonly Transform LookAtTarget;
        public Vector3 Offset;

        public WorldScreenSpaceIcon(Transform lookAtTarget, Vector3 offset)
        {
            LookAtTarget = lookAtTarget;
            Offset = offset;
        }
    }
}