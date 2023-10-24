using Player;
using UnityEngine;

namespace Misc
{
    [DisallowMultipleComponent]
    public class KeyboardInput : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField] private CharacterControllerMovement _movement;
        [SerializeField] private Interact _interact;

        [Header("Settings")] 
        [SerializeField] private bool _useRawInput;

        private float _horizontal;
        private float _vertical;
        
        private void Update()
        {
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
