using System;
using Common;
using Environment.Bedroom.PC;
using Interfaces;
using UnityEngine;
using Utilities;

namespace Environment.Bedroom
{
    [RequireComponent(typeof(BoxCollider))]
    [DisallowMultipleComponent]
    public class Closet : MonoBehaviour, IInteractable
    {
        [Header("External references")]
        [SerializeField, ChildrenOnly] private BoxCollider _blockCollider;
        [SerializeField, ChildrenOnly] private GameObject _tshirt;

        private BoxCollider _closetCollider;

        private void Awake()
        {
            _closetCollider = GetComponent<BoxCollider>();
            _closetCollider.Disable();
            _blockCollider.Enable();
        }

        private void OnEnable()
        {
            WokeUpStaticEvent.OnWokeUp += OnAnyWokeUp;
        }

        private void OnDisable()
        {
            WokeUpStaticEvent.OnWokeUp -= OnAnyWokeUp;
        }

        private void OnAnyWokeUp(object sender, EventArgs e)
        {
            _closetCollider.Enable();
            _blockCollider.Disable();
        }

        public void Interact()
        {
            Destroy(_tshirt);
            OnAnyGotDressed.Call(this);
            Destroy(this);
        }

        public string GetActionDescription()
        {
            return "Get dressed";
        }
    }
}
