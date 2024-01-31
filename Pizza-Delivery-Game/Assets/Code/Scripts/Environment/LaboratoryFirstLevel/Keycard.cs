using System;
using Enums.Keycards;
using InspectableObject;
using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    [DisallowMultipleComponent]
    public class Keycard : UI.InspectableObject.InspectableObject
    {
        [Header("External references")]
        [SerializeField] private InspectableObjectSO _inspectableObject;

        [Header("Settings")] 
        [SerializeField] private KeycardType _keycardType;

        protected override InspectableObjectSO InspectableObjectSO => _inspectableObject;
        protected override string ActionDescription => "Keycard";

        public override void Interact()
        {
            switch (_keycardType)
            {
                case KeycardType.KeycardA:
                    PickedUpKeycardAStaticEvent.Call(this);
                    break;
                case KeycardType.KeycardB:
                    PickedUpKeycardBStaticEvent.Call(this);
                    break;
                case KeycardType.KeycardC:
                    PickedUpKeycardCStaticEvent.Call(this);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            base.Interact();
        }
    }
}
