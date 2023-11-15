using Interfaces;
using UnityEngine;

namespace Environment.Outdoor
{
    public class Car : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            // TODO: Sit in the car
        }

        public string GetActionDescription()
        {
            return "Sit in the car";
        }
    }
}
