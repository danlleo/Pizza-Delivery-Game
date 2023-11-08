using Interfaces;
using UnityEngine;

namespace Environment.Bedroom.PC
{
    [DisallowMultipleComponent]
    public class Browser : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            print("Closed");
        }

        public string GetActionDescription()
        {
            return "Close";
        }
    }
}
