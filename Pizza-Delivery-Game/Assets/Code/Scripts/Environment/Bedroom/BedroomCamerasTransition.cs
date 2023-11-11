using System;
using Cinemachine;
using Environment.Bedroom.PC;
using Misc;
using UnityEngine;

namespace Environment.Bedroom
{
    public class BedroomCamerasTransition : Singleton<BedroomCamerasTransition>
    {
        [Header("External references")] 
        [SerializeField] private CinemachineVirtualCamera _mainVirtualCamera;
        [SerializeField] private CinemachineVirtualCamera _computerScreenVirtualCamera;        
        
        [Header("Settings")]
        [SerializeField] private int _lowPriorityValue = 1;
        [SerializeField] private int _highPriorityValue = 10;

        private Camera _mainCamera;

        protected override void Awake()
        {
            base.Awake();
            
            _mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            StartedUsingPCStaticEvent.OnStarted += StartedUsingPCStaticEventOn_Started;
            StoppedUsingPCStaticEvent.OnEnded += StoppedUsingPCStaticEvent_OnEnded;
        }

        private void OnDisable()
        {
            StartedUsingPCStaticEvent.OnStarted -= StartedUsingPCStaticEventOn_Started;
            StoppedUsingPCStaticEvent.OnEnded -= StoppedUsingPCStaticEvent_OnEnded;
        }

        public void ResetMainCamera()
        {
            ResetMainCameraPosition();
            ResetMainCameraRotation();
        }
        
        private void ResetMainCameraPosition()
        {
            _mainCamera.transform.localPosition = Vector3.zero;
        }

        private void ResetMainCameraRotation()
        {
            _mainCamera.transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
        
        private void StartedUsingPCStaticEventOn_Started(object sender, EventArgs e)
        {
            _mainVirtualCamera.Priority = _lowPriorityValue;
            _computerScreenVirtualCamera.Priority = _highPriorityValue;
        }
        
        private void StoppedUsingPCStaticEvent_OnEnded(object sender, EventArgs e)
        {
            _mainVirtualCamera.Priority = _highPriorityValue;
            _computerScreenVirtualCamera.Priority = _lowPriorityValue;
        }
    }
}
