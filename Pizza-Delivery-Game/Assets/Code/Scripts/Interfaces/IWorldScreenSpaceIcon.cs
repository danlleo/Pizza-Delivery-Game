using UnityEngine;
using Vector3 = System.Numerics.Vector3;

namespace Interfaces
{
    public interface IWorldScreenSpaceIcon
    {
        public Transform GetLookAtTarget();
        public Vector3 GetOffset();
    }
}