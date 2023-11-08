using Interfaces;
using UnityEngine;

namespace Environment.Bedroom
{
    public class PCScreen : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            throw new System.NotImplementedException();
        }

        public string GetActionDescription()
        {
            return "Read vacancy";
        }
    }
}
