using UnityEngine;

namespace WorldScreenSpaceIcon
{
    public abstract class WorldScreenSpaceIcon : MonoBehaviour
    {
        public virtual void OnDestroy()
        {
            this.CallWorldScreenSpaceIconLostStaticEvent();
        }

        public abstract WorldScreenSpaceIconData GetWorldScreenSpaceIconData();
    }
}