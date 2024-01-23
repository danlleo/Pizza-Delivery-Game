using InspectableObject;
using UnityEngine;
using WorldScreenSpaceIcon;

namespace Environment.LaboratoryFirstLevel
{
    [DisallowMultipleComponent]
    public class Flashlight : UI.InspectableObject.InspectableObject
    {
        [SerializeField] private InspectableObjectSO _inspectableObject;
        
        protected override InspectableObjectSO InspectableObjectSO => _inspectableObject;
        protected override string ActionDescription => "Flashlight";
        
        public override WorldScreenSpaceIconData GetWorldScreenSpaceIconData()
        {
            throw new System.NotImplementedException();
        }
    }
}
