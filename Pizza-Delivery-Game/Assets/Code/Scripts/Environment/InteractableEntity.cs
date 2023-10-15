using Interfaces;
using UnityEngine;

namespace Environment
{
    public class InteractableEntity : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            print("You're interacting with me :)");
        }

        public string GetActionDescription()
            => "Pick Up";
    }
}
