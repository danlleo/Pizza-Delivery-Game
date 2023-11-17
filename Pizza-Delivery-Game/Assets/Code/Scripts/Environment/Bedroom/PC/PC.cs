using Interfaces;
using UnityEngine;

namespace Environment.Bedroom.PC
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(BoxCollider))]
    public class PC : MonoBehaviour, IInteractable
    {
        private BoxCollider _boxCollider;

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
        }

        public void Interact()
        {
            _boxCollider.enabled = false;
            Player.Player.Instance.CallStartedUsingPC();
        }

        public string GetActionDescription()
        {
            return "Read vacancy";
        }
    }
}
