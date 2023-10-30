using InspectableObject;
using Interfaces;
using UnityEngine;

namespace Environment.Bedroom
{
    public class Flashlight : MonoBehaviour, IInteractable
    {
        [SerializeField] private InspectableObjectSO _inspectableObject;
        
        public void Interact()
        {
            Trigger.Instance.Invoke(_inspectableObject);
        }

        public string GetActionDescription()
        {
            return "Flashlight";
        }
    }
}
