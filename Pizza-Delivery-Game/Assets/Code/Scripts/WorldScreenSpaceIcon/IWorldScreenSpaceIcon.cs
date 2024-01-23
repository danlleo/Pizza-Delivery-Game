using UnityEngine;

namespace WorldScreenSpaceIcon
{
    public abstract class WorldScreenSpaceIcon : MonoBehaviour
    {
        public virtual void OnDestroy()
        {
            this.CallWorldScreenSpaceIconLostStaticEvent();
        }

        public virtual WorldScreenSpaceIconData GetWorldScreenSpaceIconData()
        {
            return new WorldScreenSpaceIconData(transform, Vector3.zero);
        }
    }
}