using UnityEngine;

namespace Player
{
    public class MouseRotation : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField] private Player _player;
        [SerializeField] private Camera _camera;
        
        [Header("Settings")]
        [SerializeField] private float _mouseSensitivity = 5f;
        [SerializeField] private float _verticalClampValueInDegrees = 90f;

        private float _verticalRotation;
        
        public void RotateTowards(Vector2 input)
        {
            input *= _mouseSensitivity * Time.deltaTime;
            
            // Because vertical input value contains really small number, we decrease _verticalRotation variable,
            // so later we could clamp it
            _verticalRotation -= input.y;
            _verticalRotation =
                Mathf.Clamp(_verticalRotation, -_verticalClampValueInDegrees, _verticalClampValueInDegrees);
            
            // We rotate camera separately from the body
            _camera.transform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
            _player.transform.Rotate(Vector3.up * input.x);
        }
    }
}
