using UnityEngine;

namespace WorldScreenSpaceIcon
{
    public struct WorldScreenSpaceIconData
    {
        public readonly Transform LookAtTarget;
        public Vector3 Offset;

        public WorldScreenSpaceIconData(Transform lookAtTarget, Vector3 offset)
        {
            LookAtTarget = lookAtTarget;
            Offset = offset;
        }
    }
}