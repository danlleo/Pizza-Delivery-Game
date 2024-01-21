using System.Collections.Generic;
using System.Linq;
using DataPersistence;
using DataPersistence.Data;
using Player.Inventory;
using UnityEngine;

namespace Inventory
{
    public class Inventory : PersistentData
    {
        private HashSet<string> _itemsHashSet = new();
        
        public bool HasItem(ItemSO item)
        {
            if (item != null) return _itemsHashSet.Contains(item.ID);
            
            Debug.LogWarning("Item you're passing is null");
            return false;
        }

        public bool TryAddItem(ItemSO item, out bool itemAdded)
        {
            itemAdded = false;

            if (item == null || HasItem(item)) 
                return false;
            
            _itemsHashSet.Add(item.ID);
            itemAdded = true;
            
            return true;
        }

        public bool TryRemoveItem(ItemSO item, out bool itemRemoved)
        {
            itemRemoved = false;

            if (item == null || !HasItem(item)) 
                return false;

            _itemsHashSet.Remove(item.ID);
            itemRemoved = true;

            return true;
        }
        
        public override void LoadData(GameData gameData)
        {
            _itemsHashSet = gameData.SavedInventoryItems.ToHashSet();
        }

        public override void SaveData(GameData gameData)
        {
            gameData.SavedInventoryItems = _itemsHashSet.ToList();
        }
    }
}
