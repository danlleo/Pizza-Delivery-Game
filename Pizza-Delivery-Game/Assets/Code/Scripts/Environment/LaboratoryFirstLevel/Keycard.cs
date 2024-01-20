using System;
using Enums.Keycards;
using InspectableObject;
using Interfaces;
using UnityEngine;
using WorldScreenSpaceIcon;
using Zenject;

namespace Environment.LaboratoryFirstLevel
{
    [DisallowMultipleComponent]
    public class Keycard : WorldScreenSpaceIcon.WorldScreenSpaceIcon, IInteractable, IInspectable
    {
        [Header("External references")]
        [SerializeField] private InspectableObjectSO _inspectableObject;

        [Header("Settings")] 
        [SerializeField] private KeycardType _keycardType;

        private Player.Player _player;

        [Inject]
        private void Construct(Player.Player player)
        {
            _player = player;
        }
        
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
            
            InspectableObjectTrigger.Instance.Invoke(_inspectableObject, AddToInventory);
            Destroy(gameObject);
        }

        public string GetActionDescription()
        {
            return "Keycard";
        }

        public void AddToInventory()
        {
            _player.Inventory.TryAddItem(_inspectableObject.Item, out bool _);
        }

        public override WorldScreenSpaceIconData GetWorldScreenSpaceIconData()
        {
            return new WorldScreenSpaceIconData(transform, Vector3.zero);
        }
    }
}
