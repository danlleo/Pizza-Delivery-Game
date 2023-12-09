using Interfaces;
using Tablet;
using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    [DisallowMultipleComponent]
    public class Tablet : MonoBehaviour, IInteractable
    {
        [Header("External references")]
        [SerializeField] private BloodWiper _bloodWiper;

        private bool _isInteractable = true;
        
        public void Interact()
        {
            Destroy(this);
            _bloodWiper.Initialize(transform.position);
            _bloodWiper.PickUp();
        }

        public string GetActionDescription()
        {
            return "Inspect tablet";
        }
    }
}
