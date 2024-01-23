using InspectableObject;
using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    public class Wrench : UI.InspectableObject.InspectableObject
    {
        [SerializeField] private InspectableObjectSO _wrenchInspectableObject;

        protected override InspectableObjectSO InspectableObjectSO => _wrenchInspectableObject;
        protected override string ActionDescription => "Wrench";
    }
}
