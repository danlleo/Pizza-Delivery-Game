using UnityEditor;
using UnityEngine;

namespace Player.Inventory
{
    [CreateAssetMenu(fileName = "Item_", menuName = "Scriptable Objects/Inventory/Item")]
    public class ItemSO : ScriptableObject
    {
        [ContextMenuItem("Generate ID", nameof(GenerateID))]
        [SerializeField] private string _id;

        public string ID => _id;

        private void GenerateID()
            => _id = GUID.Generate().ToString();
    }
}
