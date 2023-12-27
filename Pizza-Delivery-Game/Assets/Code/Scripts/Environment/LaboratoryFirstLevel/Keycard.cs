using System;
using Enums.Keycards;
using InspectableObject;
using Interfaces;
using Player.Inventory;
using UI;
using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    [DisallowMultipleComponent]
    public class Keycard : MonoBehaviour, IInteractable, IInspectable, IWorldScreenSpaceIcon
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
            Player.Player player = Player.Player.Instance;
            player.AddingItemEvent.Call(player, new AddingItemEventArgs(_inspectableObject.Item));
        }

        public WorldScreenSpaceIcon GetWorldScreenSpaceIcon()
        {
            return new WorldScreenSpaceIcon(transform, Vector3.zero);
        }
    }
}
