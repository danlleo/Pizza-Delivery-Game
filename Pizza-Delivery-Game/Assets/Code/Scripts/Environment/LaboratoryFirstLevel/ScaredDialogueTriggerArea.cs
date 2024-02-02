using System;
using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    [DisallowMultipleComponent]
    public class ScaredDialogueTriggerArea : MonoBehaviour
    {
        public static event EventHandler OnAnyEnteredScaredDialogueTriggerArea;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Player.Player _))
                return;
            
            OnAnyEnteredScaredDialogueTriggerArea?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject);
        }
    }
}
