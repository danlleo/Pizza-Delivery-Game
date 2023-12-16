using Enums.Keycards;
using InspectableObject;
using Interfaces;
using Player.Inventory;
using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    [DisallowMultipleComponent]
    public class Keycard : MonoBehaviour, IInteractable, IInspectable
    {
        [Header("External references")]
        [SerializeField] private InspectableObjectSO _inspectableObject;

        [Header("Settings")] 
        [SerializeField] private KeycardType _keycardType;
            
        public void Interact()
        {
            if (_keycardType == KeycardType.KeycardC)
                PickedUpKeycardCStaticEvent.Call(this);
            
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
    }
}
