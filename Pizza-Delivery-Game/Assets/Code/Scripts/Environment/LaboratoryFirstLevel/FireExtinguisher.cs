using Interfaces;
using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    public class FireExtinguisher : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            this.CallInteractedWithFireExtinguisherStaticEvent();
            Destroy(this);
        }

        public string GetActionDescription()
        {
            return "Don't interact with it";
        }
    }
}