using System;
using Enums.Keycards;
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
            Keycard.OnAnyPickedUpKeycard += Keycard_OnAnyPickedUpKeycard;
        }

        private void OnDisable()
        {
            Keycard.OnAnyPickedUpKeycard -= Keycard_OnAnyPickedUpKeycard;
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
        
        private void Keycard_OnAnyPickedUpKeycard(object sender, Keycard.OnAnyPickedUpKeycardEventArgs e)
        {
            if (e.KeycardType == KeycardType.KeycardA)
                _hasPickedUpAKeycard = true;
        }
    }
}
