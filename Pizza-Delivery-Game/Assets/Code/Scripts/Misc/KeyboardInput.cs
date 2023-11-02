using Player;
using UnityEngine;
using Flashlight = Player.Flashlight;

namespace Misc
{
    [DisallowMultipleComponent]
    public class KeyboardInput : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private UI.UI _ui;
        [SerializeField] private CharacterControllerMovement _movement;
        [SerializeField] private Interact _interact;
        [SerializeField] private Flashlight _flashlight;

        [Header("Settings")] 
        [SerializeField] private bool _useRawInput;

        private float _horizontal;
        private float _vertical;
        
        private void Update()
        {
            if (!InputAllowance.InputEnabled) return;
            
            ReadInput();
            
            var input = new Vector2(_horizontal, _vertical);
            
            _movement.Move(input);
            
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _movement.BeginSprint();
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                _movement.Sprint();
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                _movement.StopSprint();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                _interact.TryInteract();
                _ui.InspectableObjectClosingEvent.Call(_ui);
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                _flashlight.ToggleLight();
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                _movement.BeginCrouch();
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {
                _movement.Crouch();
            }

            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                _movement.EndCrouch();
            }
        }
        
        private void ReadInput()
        {
            if (_useRawInput)
            {
                _horizontal = Input.GetAxisRaw(Axis.Horizontal);
                _vertical = Input.GetAxisRaw(Axis.Vertical);
            }
            else
            {
                _horizontal = Input.GetAxis(Axis.Horizontal);
                _vertical = Input.GetAxis(Axis.Vertical);
            }
        }
    }
}
