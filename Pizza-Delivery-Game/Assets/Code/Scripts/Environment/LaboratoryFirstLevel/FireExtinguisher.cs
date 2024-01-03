using System;
using Interfaces;
using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    [DisallowMultipleComponent]
    public class FireExtinguisher : MonoBehaviour, IInteractable
    {
        private void OnEnable()
        {
            InspectedForbiddenStaticEvent.OnAnyInspectedForbidden += OnAnyInspectedForbidden;
        }
        
        private void OnDisable()
        {
            InspectedForbiddenStaticEvent.OnAnyInspectedForbidden -= OnAnyInspectedForbidden;
        }
        
        public void Interact()
        {
            this.CallInteractedWithFireExtinguisherStaticEvent();
        }

        public string GetActionDescription()
        {
            return "Don't interact with it";
        }
        
        private void OnAnyInspectedForbidden(object sender, EventArgs e)
        {
            Destroy(this);
        }
    }
}