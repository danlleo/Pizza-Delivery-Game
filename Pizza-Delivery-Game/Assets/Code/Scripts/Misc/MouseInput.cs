using UnityEngine;

namespace Misc
{
    public class MouseInput : MonoBehaviour
    {
        [SerializeField] private Player.MouseRotation _mouseRotation;
        
        private void LateUpdate()
        {
            float mouseX = Input.GetAxis(Axis.MouseX);
            float mouseY = Input.GetAxis(Axis.MouseY);

            var rotateDirection = new Vector2(mouseX, mouseY);
            
            _mouseRotation.RotateTowards(rotateDirection);
        }
    }
}
