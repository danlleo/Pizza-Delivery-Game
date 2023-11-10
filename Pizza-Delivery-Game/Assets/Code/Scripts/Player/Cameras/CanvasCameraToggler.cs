using System;
using UI.InspectableObject;
using UnityEngine;

namespace Player.Cameras
{
    [RequireComponent(typeof(Camera))]
    public class CanvasCameraToggler : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private UI.UI _ui;
        
        private Camera _canvasCamera;

        private void Awake()
        {
            _canvasCamera = GetComponent<Camera>();
            Disable();
        }

        private void OnEnable()
        {
            _ui.InspectableObjectOpeningEvent.Event += InspectableObjectOpening_Event;
            _ui.InspectableObjectCloseEvent.Event += InspectableObjectClose_Event;
        }

        private void OnDisable()
        {
            _ui.InspectableObjectOpeningEvent.Event -= InspectableObjectOpening_Event;
            _ui.InspectableObjectCloseEvent.Event -= InspectableObjectClose_Event;
        }

        private void Enable()
        {
            _canvasCamera.enabled = true;
        }

        private void Disable()
        {
            _canvasCamera.enabled = false;
        }
        
        private void InspectableObjectOpening_Event(object sender, InspectableObjectOpeningEventArgs e)
        {
            Enable();
        }

        private void InspectableObjectClose_Event(object sender, EventArgs e)
        {
            Disable();
        }
    }
}
