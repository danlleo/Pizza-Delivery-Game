using System;
using Cinemachine;
using UnityEngine;
using Zenject;

namespace Environment.Bedroom
{
    [DisallowMultipleComponent]
    public class BedroomCamerasTransition : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private CinemachineVirtualCamera _computerScreenVirtualCamera;        
        
        [Header("Settings")]
        [SerializeField] private int _lowPriorityValue = 1;
        [SerializeField] private int _highPriorityValue = 10;

        private CinemachineVirtualCamera _mainVirtualCamera;
        private Camera _mainCamera;

        [Inject]
        private void Construct(Player.Player player)
        {
            _mainVirtualCamera = player.MainVirtualCamera;
        }
        
        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            PC.OnAnyStartedUsingPC.Event += OnAnyStartedUsingPC;
            PC.OnAnyStoppedUsingPC.Event += OnAnyStoppedUsingPC;
            PC.OnAnyFinishedAllPCTasks.Event += OnAnyFinishedAllPCTasks;
        }

        private void OnDisable()
        {
            PC.OnAnyStartedUsingPC.Event -= OnAnyStartedUsingPC;
            PC.OnAnyStoppedUsingPC.Event -= OnAnyStoppedUsingPC;
            PC.OnAnyFinishedAllPCTasks.Event -= OnAnyFinishedAllPCTasks;
        }

        private void ResetMainCamera()
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
        
        private void OnAnyStartedUsingPC(object sender, EventArgs e)
        {
            _mainVirtualCamera.Priority = _lowPriorityValue;
            _computerScreenVirtualCamera.Priority = _highPriorityValue;
        }
        
        private void OnAnyStoppedUsingPC(object sender, EventArgs e)
        {
            _mainVirtualCamera.Priority = _highPriorityValue;
            _computerScreenVirtualCamera.Priority = _lowPriorityValue;
        }
        
        private void OnAnyFinishedAllPCTasks(object sender, EventArgs e)
        {
            ResetMainCamera();
        }
    }
}
