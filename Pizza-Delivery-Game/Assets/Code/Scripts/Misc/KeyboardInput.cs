using Player;
using UnityEngine;

namespace Misc
{
    public class KeyboardInput : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField] private CharacterControllerMovement _movement;

        [Header("Settings")] 
        [SerializeField] private bool _useRawInput;

        private float _horizontal;
        private float _vertical;
        
        private void Update()
        {
            ReadInput();
            
            var input = new Vector2(_horizontal, _vertical);
            
            _movement.Move(input);
        }

        public static bool IsEKeyPressedDown()
            => Input.GetKeyDown(KeyCode.E);

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
