using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    [DisallowMultipleComponent]
    public class ScaredDialogueTriggerArea : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Player.Player _))
                return;
            
            OnAnyEnteredScaredDialogueTriggerArea.Call(this);
            Destroy(gameObject);
        }
    }
}
