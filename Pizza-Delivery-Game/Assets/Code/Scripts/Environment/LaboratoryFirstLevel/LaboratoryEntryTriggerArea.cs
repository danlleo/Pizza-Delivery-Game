using System;
using Enums.Keycards;
using Player;
using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    [DisallowMultipleComponent]
    public class LaboratoryEntryTriggerArea : MonoBehaviour
    {
        public static event EventHandler OnAnyEnteredLaboratoryEntryTriggerArea;
        
        [Header("External references")] 
        [SerializeField] private Door.Door _laboratoryEntryDoor;
        
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

            OnAnyEnteredLaboratoryEntryTriggerArea?.Invoke(this, EventArgs.Empty);
            
            _laboratoryEntryDoor.Close();
            
            Destroy(gameObject);
        }
        
        private void Keycard_OnAnyPickedUpKeycard(object sender, Keycard.OnAnyPickedUpKeycardEventArgs e)
        {
            if (e.KeycardType == KeycardType.KeycardA)
                _hasPickedUpAKeycard = true;
        }
    }
}
