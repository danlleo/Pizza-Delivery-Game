using System;
using DG.Tweening;
using Enums.Keypad;
using EventBus;
using Interfaces;
using UnityEngine;

namespace Keypad
{
    public class ButtonEnter : MonoBehaviour, IKeypadButton
    {
        [Header("External references")] 
        [SerializeField] private Material _defaultMaterial;
        [SerializeField] private Material _selectedMaterial;

        private MeshRenderer _meshRenderer;

        private Vector3 _targetPosition;
        private Vector3 _defaultPosition;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _defaultPosition = transform.localPosition;
            _targetPosition = _defaultPosition + Vector3.forward * .005f;
        }

        public void Press()
        {
            Sequence pressSequence = DOTween.Sequence();
            pressSequence.Append(transform.DOLocalMove(_targetPosition, .1f));
            pressSequence.Append(transform.DOLocalMove(_defaultPosition, .1f));

            EventBus<PasswordConfirmedEvent>.Raise(new PasswordConfirmedEvent());
        }
        
        public void SetType(ButtonType buttonType)
        {
            _meshRenderer.material = buttonType switch
            {
                ButtonType.Default => _defaultMaterial,
                ButtonType.Hovered => _selectedMaterial,
                _ => throw new ArgumentOutOfRangeException(nameof(buttonType), buttonType, null)
            };
        }
    }
}
