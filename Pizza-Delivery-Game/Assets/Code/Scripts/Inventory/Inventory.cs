using System.Collections.Generic;
using System.Linq;
using DataPersistence.Data;
using Interfaces;
using Player.Inventory;

namespace Inventory
{
    public class Inventory : IDataPersistence
    {
        private HashSet<string> _itemsHashSet = new();

        public bool HasItem(ItemSO item)
            => _itemsHashSet.Contains(item.ID);

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

        public void LoadData(GameData gameData)
        {
            _itemsHashSet = gameData.SavedInventoryItems.ToHashSet();
        }

        public void SaveData(GameData gameData)
        {
            gameData.SavedInventoryItems = _itemsHashSet.ToList();
        }
    }
}
