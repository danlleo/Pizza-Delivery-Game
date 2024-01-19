using EventBus;
using Interfaces;
using Player.Inventory;
using UnityEngine;
using Zenject;

namespace Environment.LaboratoryFirstLevel
{
    [DisallowMultipleComponent]
    public class PipesFixArea : MonoBehaviour, IInteractable, IItemReceiver
    {
        [SerializeField] private ItemSO _wrench;

        private Player.Player _player;
        
        [Inject]
        private void Construct(Player.Player player)
        {
            _player = player;
        }
        
        public void Interact()
        {
            if (!_player.Inventory.HasItem(_wrench))
            {
                EventBus<FixPipesEvent>.Raise(new FixPipesEvent(false));
                return;
            }

            this.CallOnAnyItemUseEvent(new OnAnyItemUseEventArgs(this, _wrench));
        }

        public string GetActionDescription()
        {
            return "Repair";
        }

        public void OnReceive()
        {
            EventBus<FixPipesEvent>.Raise(new FixPipesEvent(true));
        }

        public void OnDecline()
        {
            
        }
    }
}
