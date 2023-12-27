using Enums.Keypad;
using EventBus;
using Interfaces;
using UnityEngine;

namespace Keypad
{
    [DisallowMultipleComponent]
    public class ButtonPress : MonoBehaviour
    {
        private IKeypadButton _selectedButton;
        private Camera _camera;

        private EventBinding<ButtonPressedEvent> _buttonPressEventBinding;
        
        private void Awake()
        {
            _camera = Camera.main;
        }

        private void OnEnable()
        {
            _buttonPressEventBinding = new EventBinding<ButtonPressedEvent>(HandlePressedButtonEvent);
            
            EventBus<ButtonPressedEvent>.Register(_buttonPressEventBinding);
        }

        private void OnDisable()
        {
            EventBus<ButtonPressedEvent>.Deregister(_buttonPressEventBinding);
        }

        private void Update()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out RaycastHit hit)) return;

            if (!hit.collider.TryGetComponent(out IKeypadButton keypadButton))
            {
                SetSelectedKeypadButtonType(ButtonType.Default);
                ClearSelectedButton();
                return;
            }
            
            SetSelectedButton(keypadButton);
            SetSelectedKeypadButtonType(ButtonType.Hovered);
        }

        private void SetSelectedButton(IKeypadButton keypadButton)
            => _selectedButton = keypadButton;
        
        private void ClearSelectedButton()
            => _selectedButton = null;
        
        private void SetSelectedKeypadButtonType(ButtonType type)
            => _selectedButton?.SetType(type);

        private void HandlePressedButtonEvent(ButtonPressedEvent buttonPressedEvent)
            => _selectedButton?.Press();
    }
}