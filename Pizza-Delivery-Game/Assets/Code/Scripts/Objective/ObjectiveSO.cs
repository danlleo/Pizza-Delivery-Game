using UnityEngine;

namespace Objective
{
    [CreateAssetMenu(fileName = "Objective_", menuName = "Scriptable Objects/Objectives/Objective")]
    public class ObjectiveSO : ScriptableObject
    {
        [field: SerializeField] public string ObjectiveText { get; private set; }        
    }
}