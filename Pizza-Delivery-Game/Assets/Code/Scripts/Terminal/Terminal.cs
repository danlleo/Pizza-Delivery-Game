using Environment.LaboratoryFirstLevel;
using Interfaces;
using UnityEngine;

namespace Terminal
{
    [DisallowMultipleComponent]
    [SelectionBase]
    public class Terminal : MonoBehaviour, IInteractable
    {
        [SerializeField] private Door.Door _door;
        
        public void Interact()
        {
            NoKeycardStaticEvent.Call(Player.Player.Instance);
        }

        public string GetActionDescription()
        {
            return "Insert keycard";
        }
    }
}
