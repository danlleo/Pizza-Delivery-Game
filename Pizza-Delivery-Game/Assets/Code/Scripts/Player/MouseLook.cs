using DataPersistence;
using DG.Tweening;
using Settings;
using UnityEngine;

namespace Player
{
    [DisallowMultipleComponent]
    public class MouseLook : MonoBehaviour
    {
        public const float DefaultMouseSensitivity = 28f;
        
        [Header("External references")]
        [SerializeField] private Player _player;
        [SerializeField] private Camera _camera;
        
        [Header("Settings")]
        [SerializeField] private float _verticalClampValueInDegrees = 90f;
        [SerializeField, Range(0f, 1f)] private float _smoothingTime = 0.15f;
        [SerializeField] private bool _useSmoothing;
        
        private float _mouseSensitivity;
        private float _horizontalRotation;
        private float _verticalRotation;

        private void Awake()
        {
            _mouseSensitivity = PlayerPrefs.HasKey(PlayerPrefsKeys.MouseSensitivity)
                ? PlayerPrefs.GetFloat(PlayerPrefsKeys.MouseSensitivity)
                : DefaultMouseSensitivity;
        }

        private void OnEnable()
        {
            Settings.OnAnySettingsChanged.Event += OnAnySettingsChanged;
        }

        private void OnDisable()
        {
            Settings.OnAnySettingsChanged.Event -= OnAnySettingsChanged;
        }

        private void OnAnySettingsChanged(object sender, OnAnySettingsChangedArgs e)
        {
            _mouseSensitivity = e.MouseSensitivity;
        }

        public void RotateTowards(Vector2 input)
        {
            input *= _mouseSensitivity * Time.deltaTime;

            _horizontalRotation += input.x;
            
            // Because vertical input value contains really small number, we decrease _verticalRotation variable,
            // so later we could clamp it
            _verticalRotation -= input.y;
            _verticalRotation =
                Mathf.Clamp(_verticalRotation, -_verticalClampValueInDegrees, _verticalClampValueInDegrees);
            
            if (_useSmoothing)
            {
                if (input == Vector2.zero) return;
                
                _camera.transform.DOLocalRotate(new Vector3(_verticalRotation, 0f, 0f), _smoothingTime);
                _player.transform.DORotateQuaternion(Quaternion.Euler(new Vector3(0f, _horizontalRotation, 0f)), _smoothingTime);
                return;
            }
            
            // We rotate camera separately from the body
            _camera.transform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
            _player.transform.Rotate(Vector3.up * input.x);
        }
    }
}
