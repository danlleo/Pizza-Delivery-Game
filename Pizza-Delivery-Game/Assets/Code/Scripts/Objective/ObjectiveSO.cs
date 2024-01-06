using UnityEditor;
using UnityEngine;

namespace Objective
{
    [CreateAssetMenu(fileName = "Objective_", menuName = "Scriptable Objects/Objectives/Objective")]
    public class ObjectiveSO : ScriptableObject
    {
        [field: SerializeField] public string ObjectiveText { get; private set; }        
        
#if UNITY_EDITOR
        [ContextMenuItem("Generate ID", nameof(GenerateID))]
#endif
        [SerializeField] private string _id;

        public string ID => _id;
        
#if UNITY_EDITOR
        private void GenerateID()
            => _id = GUID.Generate().ToString();
#endif
    }
}