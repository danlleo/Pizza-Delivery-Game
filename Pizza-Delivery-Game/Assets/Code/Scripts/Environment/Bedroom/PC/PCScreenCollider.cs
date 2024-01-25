using System;
using Interfaces;
using Misc;
using UnityEngine;
using Utilities;

namespace Environment.Bedroom.PC
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(BoxCollider))]
    public class PCScreenCollider : MonoBehaviour, IInteractable
    {
        private BoxCollider _boxCollider;

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
            _boxCollider.Disable();
        }

        private void OnEnable()
        {
            PC.OnAnyAllowedUsingPC.Event += OnAnyAllowedUsingPC;
        }

        private void OnDisable()
        {
            PC.OnAnyAllowedUsingPC.Event -= OnAnyAllowedUsingPC;
        }

        private void OnAnyAllowedUsingPC(object sender, EventArgs e)
        {
            _boxCollider.Enable();
        }

        public void Interact()
        {
            _boxCollider.Disable();
            CrosshairDisplayStateChangedStaticEvent.Call(this, new CrosshairDisplayStateChangedEventArgs(false));
            OnAnyStartedUsingPC.Call(this);
        }

        public string GetActionDescription()
        {
            return "Read vacancy";
        }
    }
}
