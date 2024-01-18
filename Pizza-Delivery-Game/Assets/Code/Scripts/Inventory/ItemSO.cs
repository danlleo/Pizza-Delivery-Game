using UnityEditor;
using UnityEngine;

namespace Player.Inventory
{
    [CreateAssetMenu(fileName = "Item_", menuName = "Scriptable Objects/Inventory/Item")]
    public class ItemSO : ScriptableObject
    {
        #if UNITY_EDITOR
        [ContextMenuItem("Generate ID", nameof(GenerateID))]
        #endif
        [SerializeField] private string _id;
        [SerializeField] private string _itemName;
        
        public string ID => _id;
        public string ItemName => _itemName;
        
        #if UNITY_EDITOR
        private void GenerateID()
            => _id = GUID.Generate().ToString();
        #endif
    }
}
