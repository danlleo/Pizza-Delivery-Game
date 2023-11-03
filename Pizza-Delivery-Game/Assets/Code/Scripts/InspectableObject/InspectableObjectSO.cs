using UnityEngine;

namespace InspectableObject
{
    [CreateAssetMenu(fileName = "InspectableObject_", menuName = "Scriptable Objects/Inspectable Objects/Inspectable Object")]
    public class InspectableObjectSO : ScriptableObject
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Vector3 _scale;
        [SerializeField] private Vector3 _rectScale;
        [SerializeField] private Player.Inventory.ItemSO _item;
        [SerializeField] private string _headerText;
        [SerializeField, TextArea(1, 60)] private string _descriptionText;

        public GameObject Prefab => _prefab;
        public Vector3 Scale => _scale;
        public Vector3 RectScale => _rectScale;
        public Player.Inventory.ItemSO Item => _item;
        public string HeaderText => _headerText;
        public string DescriptionText => _descriptionText;
    }
}