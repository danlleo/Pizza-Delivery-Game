using System;
using Player;
using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    [DisallowMultipleComponent]
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

        private void OnTriggerEnter(Collider other)
        {
            if (!_hasPickedUpAKeycard)
                return;
            
            if (!other.gameObject.TryGetComponent(out Player.Player _)) 
                return;

            EnteredLaboratoryEntryTriggerAreaStaticEvent.Call(this);
            
            _laboratoryEntryDoor.Close();
            
            Destroy(gameObject);
        }
        
        private void OnAnyPickedUpKeycardA(object sender, EventArgs e)
        {
            _hasPickedUpAKeycard = true;
        }
    }
}
