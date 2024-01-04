using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    [DisallowMultipleComponent]
    public class LaboratoryWorkerStartCryTriggerArea : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Player.Player _))
                return;

            LaboratoryWorkerCryStaticEvent.Call(this, new LaboratoryWorkerCryEventArgs(true));
            Destroy(gameObject);
        }
    }
}
