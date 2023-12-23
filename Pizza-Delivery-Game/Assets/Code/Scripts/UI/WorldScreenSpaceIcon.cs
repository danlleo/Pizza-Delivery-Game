using UnityEngine;

namespace UI
{
    public class WorldScreenSpaceIcon
    {
        // Malachi 3:10
        public Transform LookAtTarget;
        public Vector3 Offset;

        public WorldScreenSpaceIcon(Transform lookAtTarget, Vector3 offset)
        {
            LookAtTarget = lookAtTarget;
            Offset = offset;
        }
    }
}