using UnityEngine;

namespace Misc
{
    public class MouseInput : MonoBehaviour
    {
        [SerializeField] private Player.MouseRotation _mouseRotation;
        
        private void LateUpdate()
        {
            if (!InputAllowance.InputEnabled) return;
            
            float mouseX = Input.GetAxisRaw(Axis.MouseX);
            float mouseY = Input.GetAxisRaw(Axis.MouseY);

            var rotateDirection = new Vector2(mouseX, mouseY);
            
            _mouseRotation.RotateTowards(rotateDirection);
        }
    }
}
