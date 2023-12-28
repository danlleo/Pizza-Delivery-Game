using EventBus;
using Interfaces;
using Player.Inventory;
using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    [DisallowMultipleComponent]
    public class PipesFixArea : MonoBehaviour, IInteractable
    {
        [SerializeField] private ItemSO _wrench;
        
        public void Interact()
        {
            if (!Player.Player.Instance.GetComponent<Inventory>().HasItem(_wrench))
            {
                EventBus<FixPipesEvent>.Raise(new FixPipesEvent(false));
                return;
            }
            
            EventBus<FixPipesEvent>.Raise(new FixPipesEvent(true));
            Destroy(gameObject);
        }

        public string GetActionDescription()
        {
            return "Repair";
        }
    }
}