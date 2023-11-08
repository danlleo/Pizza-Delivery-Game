using System;
using Cinemachine;
using Environment.Bedroom.PC;
using UnityEngine;

namespace Misc
{
    public class BedroomCamerasTransition : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private CinemachineVirtualCamera _mainVirtualCamera;
        [SerializeField] private CinemachineVirtualCamera _computerScreenVirtualCamera;        
        
        [Header("Settings")]
        [SerializeField] private int _lowPriorityValue = 1;
        [SerializeField] private int _highPriorityValue = 10;

        private void OnEnable()
        {
            StartedUsingPCStaticEvent.OnStarted += StartedUsingPCStaticEventOn_Started;
            StoppedUsingPCStaticEvent.OnEnded += StoppedUsingPCStaticEventOn_Ended;
        }

        private void OnDisable()
        {
            StartedUsingPCStaticEvent.OnStarted += StartedUsingPCStaticEventOn_Started;
            StoppedUsingPCStaticEvent.OnEnded += StoppedUsingPCStaticEventOn_Ended;
        }

        private void StartedUsingPCStaticEventOn_Started(object sender, EventArgs e)
        {
            _mainVirtualCamera.Priority = _lowPriorityValue;
            _computerScreenVirtualCamera.Priority = _highPriorityValue;
        }
        
        private void StoppedUsingPCStaticEventOn_Ended(object sender, EventArgs e)
        {
            _computerScreenVirtualCamera.Priority = _lowPriorityValue;
            _mainVirtualCamera.Priority = _highPriorityValue;
        }
    }
}
