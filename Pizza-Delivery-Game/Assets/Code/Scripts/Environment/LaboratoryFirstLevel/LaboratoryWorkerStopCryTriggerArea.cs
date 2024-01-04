using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    [DisallowMultipleComponent]
    public class LaboratoryWorkerStopCryTriggerArea : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Player.Player _))
                return;

            LaboratoryWorkerCryStaticEvent.Call(this, new LaboratoryWorkerCryEventArgs(false));
            Destroy(gameObject);
        }
    }
}