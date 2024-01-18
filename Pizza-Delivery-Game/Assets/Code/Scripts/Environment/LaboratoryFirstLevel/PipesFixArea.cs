using EventBus;
using Interfaces;
using Player.Inventory;
using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    [DisallowMultipleComponent]
    public class PipesFixArea : MonoBehaviour, IInteractable, IItemReceiver
    {
        [SerializeField] private ItemSO _wrench;
        
        public void Interact()
        {
            if (!Player.Player.Instance.Inventory.HasItem(_wrench))
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
