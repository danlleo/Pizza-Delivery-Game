using UnityEngine;

namespace Environment.LaboratorySecondLevel
{
    [DisallowMultipleComponent]
    public class MonsterAttractionArea : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Player.Player _))
                return;
            
            print("Attracted");
            
            AttractedMonsterStaticEvent.Call(this, new AttractedMonsterEventArgs(transform.position));
        }
    }
}
