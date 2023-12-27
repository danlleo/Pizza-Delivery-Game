using EventBus;
using Interfaces;
using Misc;
using UnityEngine;
using UnityEngine.Serialization;

namespace Keypad
{
    [RequireComponent(typeof(BoxCollider))]
    [DisallowMultipleComponent]
    public class Keypad : MonoBehaviour, IInteractable
    {
        [FormerlySerializedAs("_buttonHover")]
        [Header("External References")]
        [SerializeField] private ButtonPress _buttonPress;

        private BoxCollider _keypadBoxCollider;
        
        private void Awake()
        {
            _keypadBoxCollider = GetComponent<BoxCollider>();
            
            _buttonPress.enabled = false;
            _keypadBoxCollider.enabled = true;
        }

        public void Interact()
        {
            InputAllowance.DisableInput();
            EventBus<InteractedWithKeypadEvent>.Raise(new InteractedWithKeypadEvent());
            CrosshairDisplayStateChangedStaticEvent.Call(this, new CrosshairDisplayStateChangedEventArgs(false));
            CursorLockStateChangedStaticEvent.Call(this, new CursorLockStateChangedStaticEventArgs(false));

            _buttonPress.enabled = true;
            _keypadBoxCollider.enabled = false;
        }

        public string GetActionDescription()
        {
            return "Keypad";
        }
    }
}
