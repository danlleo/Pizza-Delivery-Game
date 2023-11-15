using Interfaces;
using UnityEngine;

namespace Scientist.Outdoor
{
    [RequireComponent(typeof(BoxCollider))]
    [DisallowMultipleComponent]
    public class InteractableCollider : MonoBehaviour, IInteractable
    {
        [SerializeField] private Scientist _scientist;
        
        private BoxCollider _boxCollider;

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
        }

        public void Interact()
        {
            _scientist.InteractedWithScientistEvent.Call(_scientist);
            _boxCollider.enabled = false;
        }

        public string GetActionDescription()
        {
            return "Scientist";
        }
    }
}
