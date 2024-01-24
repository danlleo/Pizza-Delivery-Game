using InspectableObject;
using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    [DisallowMultipleComponent]
    public class Flashlight : UI.InspectableObject.InspectableObject
    {
        [SerializeField] private InspectableObjectSO _inspectableObject;
        
        protected override InspectableObjectSO InspectableObjectSO => _inspectableObject;
        protected override string ActionDescription => "Flashlight";
    }
}
