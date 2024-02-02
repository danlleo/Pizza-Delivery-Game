using System;
using Enums.Keycards;
using InspectableObject;
using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    [DisallowMultipleComponent]
    public class Keycard : UI.InspectableObject.InspectableObject
    {
        public static event EventHandler<OnAnyPickedUpKeycardEventArgs> OnAnyPickedUpKeycard;

        public class OnAnyPickedUpKeycardEventArgs : EventArgs
        {
            public readonly KeycardType KeycardType;

            public OnAnyPickedUpKeycardEventArgs(KeycardType keycardType)
            {
                KeycardType = keycardType;
            }
        }
        
        [Header("External references")]
        [SerializeField] private InspectableObjectSO _inspectableObject;

        [Header("Settings")] 
        [SerializeField] private KeycardType _keycardType;

        protected override InspectableObjectSO InspectableObjectSO => _inspectableObject;
        protected override string ActionDescription => "Keycard";

        public override void Interact()
        {
            OnAnyPickedUpKeycard?.Invoke(this, new OnAnyPickedUpKeycardEventArgs(_keycardType));
            
            base.Interact();
        }
    }
}
