using System;
using Enums.Gravity;
using UnityEngine;

namespace Player
{
    [DisallowMultipleComponent]
    public class GravityPulldown : MonoBehaviour
    {
        [Header("External References")]
        [SerializeField] private CharacterControllerMovement _characterControllerMovement;
        [SerializeField] private CharacterController _characterController;
        
        [Header("Settings")]
        [SerializeField] private GravityType _gravityType;
        
        private Vector3 _velocity;

        private float _gravityValue;

        private bool _hasLanded;
        private bool _gravityEnabled;

        private void Awake()
        {
            switch (_gravityType)
            {
                case GravityType.Earth:
                    SetGravityValue(9.807f);
                    break;
                case GravityType.Jupiter:
                    SetGravityValue(24.79f);
                    break;
                case GravityType.Moon:
                    SetGravityValue(1.62f);
                    break;
                case GravityType.Uranus:
                    SetGravityValue(8.87f);
                    break;
                case GravityType.Neptune:
                    SetGravityValue(11.15f);
                    break;
                case GravityType.Saturn:
                    SetGravityValue(10.44f);
                    break;
                case GravityType.Sun:
                    SetGravityValue(274f);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _gravityEnabled = true;
        }

        private void OnEnable()
        {
            GravityPulldownEnableStateStaticEvent.OnAnyGravityPulldownEnableStateChanged += OnAnyGravityPulldownEnableStateChanged;
        }

        private void OnDisable()
        {
            GravityPulldownEnableStateStaticEvent.OnAnyGravityPulldownEnableStateChanged -= OnAnyGravityPulldownEnableStateChanged;
        }

        private void Update()
        {
            if (!_gravityEnabled) return;
            
            if (_characterControllerMovement.IsGrounded() && _velocity.y < 0)
            {
                ResetVelocity();

                if (_hasLanded) return;
                
                _characterControllerMovement.Land();
                _hasLanded = true;

                return;
            }

            _hasLanded = false;
            
            ApplyGravity();
        }

        private void ApplyGravity()
        {
            _velocity.y += _gravityValue * Time.deltaTime;
            _characterController.Move(_velocity * Time.deltaTime);
        }

        private void ResetVelocity()
            => _velocity.y = -2f;

        private void SetGravityValue(float value)
            => _gravityValue = -value;
        
        private void OnAnyGravityPulldownEnableStateChanged(object sender, GravityPulldownEnableStateStaticEventArgs e)
        {
            _gravityEnabled = e.Enabled;
        }
    }
}
