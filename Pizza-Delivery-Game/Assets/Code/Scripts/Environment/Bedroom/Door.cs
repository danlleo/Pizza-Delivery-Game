using Interfaces;
using UnityEngine;

namespace Environment.Bedroom
{
    public class Door : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            print("Interacting00");
        }

        public string GetActionDescription()
        {
            return "Leave";
        }
    }
}
