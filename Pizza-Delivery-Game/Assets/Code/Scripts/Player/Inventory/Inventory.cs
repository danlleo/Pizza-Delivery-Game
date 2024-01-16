using System.Collections.Generic;
using System.Linq;
using DataPersistence.Data;
using Interfaces;
using UnityEngine;

namespace Player.Inventory
{
    [DisallowMultipleComponent]
    public class Inventory : MonoBehaviour, IDataPersistence
    {
        private HashSet<string> _itemsHashSet = new();

        private void OnEnable()
        {
            OnAnyAddedItemEvent.Event += OnAnyAddedItem;
            OnAnyRemovedItemEvent.Event += OnAnyRemovedItem;
        }
        
        private void OnDisable()
        {
            OnAnyAddedItemEvent.Event -= OnAnyAddedItem;
            OnAnyRemovedItemEvent.Event -= OnAnyRemovedItem;
        }

        public bool HasItem(ItemSO item)
            => _itemsHashSet.Contains(item.ID);
        
        private void OnAnyAddedItem(object sender, AddingItemEventArgs e)
        {
            if (TryAddItem(e.Item, out bool itemAdded))
            {
                print("Item added");
                return;
            }

            print("Contains duplicate or null");
        }
        
        private void OnAnyRemovedItem(object sender, RemovingItemEventArgs e)
        {
            if (TryRemoveItem(e.Item, out bool itemRemoved))
            {
                print("Item removed");
                return;
            }
            
            print("Item null or it wasn't in the inventory");
        }

        private bool TryAddItem(ItemSO item, out bool itemAdded)
        {
            itemAdded = false;

            if (item == null || HasItem(item)) 
                return false;
            
            _itemsHashSet.Add(item.ID);
            itemAdded = true;
            
            print("Added");
            
            return true;
        }

        private bool TryRemoveItem(ItemSO item, out bool itemRemoved)
        {
            itemRemoved = false;

            if (item == null || !HasItem(item)) 
                return false;

            _itemsHashSet.Remove(item.ID);
            itemRemoved = true;

            print("Removed");
            
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
