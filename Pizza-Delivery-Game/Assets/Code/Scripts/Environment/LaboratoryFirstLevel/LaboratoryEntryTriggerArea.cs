using System;
using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    public class LaboratoryEntryTriggerArea : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private Door.Door _laboratoryEntryDoor;
        
        private bool _hasPickedUpAKeycard;
        
        private void OnEnable()
        {
            PickedUpKeycardAStaticEvent.OnAnyPickedUpKeycardA += OnAnyPickedUpKeycardA;
        }

        private void OnDisable()
        {
            PickedUpKeycardAStaticEvent.OnAnyPickedUpKeycardA -= OnAnyPickedUpKeycardA;
        }

        private void OnAnyPickedUpKeycardA(object sender, EventArgs e)
        {
            _hasPickedUpAKeycard = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_hasPickedUpAKeycard)
                return;
            
            if (!other.gameObject.TryGetComponent(out Player.Player player)) 
                return;
            
            EnteredLaboratoryEntryTriggerAreaStaticEvent.Call(this);
            
            _laboratoryEntryDoor.Close();
            
            Destroy(gameObject);
        }
    }
}
