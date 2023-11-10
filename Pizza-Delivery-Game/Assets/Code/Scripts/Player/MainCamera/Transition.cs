using System;
using Environment.Bedroom.PC;
using UnityEngine;

namespace Player.MainCamera
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Camera))]
    public class Transition : MonoBehaviour
    {
        private Camera _camera;

        private Vector3 _cachedLocalPosition;
        private Quaternion _cachedRotation;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }

        private void OnEnable()
        {
            StartedUsingPCStaticEvent.OnStarted += StartedUsingPCStaticEvent_OnStarted;
            StoppedUsingPCStaticEvent.OnEnded += StoppedUsingPCStaticEvent_OnEnded;
        }

        private void OnDisable()
        {
            StartedUsingPCStaticEvent.OnStarted -= StartedUsingPCStaticEvent_OnStarted;
            StoppedUsingPCStaticEvent.OnEnded -= StoppedUsingPCStaticEvent_OnEnded;
        }

        private void StartedUsingPCStaticEvent_OnStarted(object sender, EventArgs e)
        {
            _cachedLocalPosition = _camera.transform.localPosition;
            _cachedRotation = _camera.transform.rotation;
        }
        
        private void StoppedUsingPCStaticEvent_OnEnded(object sender, EventArgs e)
        {
            _camera.transform.localPosition = _cachedLocalPosition;
            _camera.transform.rotation = _cachedRotation;
        }
    }
}
