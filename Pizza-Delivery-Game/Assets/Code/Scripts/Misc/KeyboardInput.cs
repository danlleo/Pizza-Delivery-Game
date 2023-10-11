using Player;
using UnityEngine;

namespace Misc
{
    public class KeyboardInput : MonoBehaviour
    {
        [SerializeField] private CharacterControllerMovement _movement;

        private void Update()
        {
            float horizontal = Input.GetAxis(Axis.Horizontal);
            float vertical = Input.GetAxis(Axis.Vertical);

            var input = new Vector2(horizontal, vertical);
            
            _movement.Move(input);
        }

        public static bool IsEKeyPressedDown()
            => Input.GetKeyDown(KeyCode.E);
    }
}
