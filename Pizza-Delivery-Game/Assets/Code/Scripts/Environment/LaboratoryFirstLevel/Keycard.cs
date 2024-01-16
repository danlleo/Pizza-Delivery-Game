using System;
using Enums.Keycards;
using InspectableObject;
using Interfaces;
using Player.Inventory;
using UnityEngine;
using WorldScreenSpaceIcon;

namespace Environment.LaboratoryFirstLevel
{
    [DisallowMultipleComponent]
    public class Keycard : WorldScreenSpaceIcon.WorldScreenSpaceIcon, IInteractable, IInspectable
    {
        [Header("External references")]
        [SerializeField] private InspectableObjectSO _inspectableObject;

        [Header("Settings")] 
        [SerializeField] private KeycardType _keycardType;
            
        public void Interact()
        {
            switch (_keycardType)
            {
                case KeycardType.KeycardA:
                    PickedUpKeycardAStaticEvent.Call(this);
                    break;
                case KeycardType.KeycardB:
                    break;
                case KeycardType.KeycardC:
                    PickedUpKeycardCStaticEvent.Call(this);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            Trigger.Instance.Invoke(_inspectableObject, AddToInventory);
            Destroy(gameObject);
        }

        public string GetActionDescription()
        {
            return "Keycard";
        }

        public void AddToInventory()
        {
            OnAnyAddedItemEvent.Call(this, new AddingItemEventArgs(_inspectableObject.Item));
        }

        public override WorldScreenSpaceIconData GetWorldScreenSpaceIconData()
        {
            return new WorldScreenSpaceIconData(transform, Vector3.zero);
        }
    }
}
