using System;
using Player;
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
            
            if (!other.gameObject.TryGetComponent(out Player.Player _)) 
                return;

            GravityPulldownEnableStateStaticEvent.Call(this, new GravityPulldownEnableStateStaticEventArgs(true));
            EnteredLaboratoryEntryTriggerAreaStaticEvent.Call(this);
            
            _laboratoryEntryDoor.Close();
            
            Destroy(gameObject);
        }
    }
}
