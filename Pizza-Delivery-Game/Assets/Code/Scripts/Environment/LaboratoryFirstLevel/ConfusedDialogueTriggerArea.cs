using System;
using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    [DisallowMultipleComponent]
    public class ConfusedDialogueTriggerArea : MonoBehaviour
    {
        public static event EventHandler OnAnyEnteredConfusedDialogueTriggerArea;
        
        private bool _hasPickedUpAKeycard;
        
        private void OnEnable()
        {
            PickedUpKeycardAStaticEvent.OnAnyPickedUpKeycardA += OnAnyPickedUpKeycardA;
        }

        private void OnDisable()
        {
            PickedUpKeycardAStaticEvent.OnAnyPickedUpKeycardA -= OnAnyPickedUpKeycardA;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!_hasPickedUpAKeycard)
                return;
            
            if (!other.gameObject.TryGetComponent(out Player.Player _)) 
                return;
            
            OnAnyEnteredConfusedDialogueTriggerArea?.Invoke(this, EventArgs.Empty);
            
            Destroy(gameObject);
        }
        
        private void OnAnyPickedUpKeycardA(object sender, EventArgs e)
        {
            _hasPickedUpAKeycard = true;
        }
    }
}
