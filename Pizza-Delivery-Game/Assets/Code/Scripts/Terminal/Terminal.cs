using System;
using Environment.LaboratoryFirstLevel;
using Interfaces;
using Inventory;
using Player.Inventory;
using UnityEngine;
using Zenject;

namespace Terminal
{
    [DisallowMultipleComponent]
    [SelectionBase]
    public class Terminal : MonoBehaviour, IInteractable, IItemReceiver
    {
        [Header("External references")]
        [SerializeField] private ItemSO _requiredKeycard;
        [SerializeField] private Door.Door _door;
        
        private Player.Player _player;

        [Inject]
        private void Construct(Player.Player player)
        {
            _player = player;
        }
        
        public void Interact()
        {
            if (_player.Inventory.HasItem(_requiredKeycard))
            {
                this.CallOnAnyItemUseEvent(new OnAnyItemUseEventArgs(this, _requiredKeycard));
                return;
            }

            KeycardStateStaticEvent.Call(this, new KeycardStateStaticEventArgs(false, transform.position));
        }

        public string GetActionDescription()
        {
            return "Insert a keycard";
        }

        public void OnReceive()
        {
            _door.Unlock();
            _door.Open();
            
            KeycardStateStaticEvent.Call(this, new KeycardStateStaticEventArgs(true, transform.position));
            
            Destroy(this);
        }

        public void OnDecline()
        {
            KeycardStateStaticEvent.Call(this, new KeycardStateStaticEventArgs(false, transform.position));
        }
    }
}
