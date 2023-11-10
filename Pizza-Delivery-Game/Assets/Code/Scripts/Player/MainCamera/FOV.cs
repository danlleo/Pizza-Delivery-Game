using Cinemachine;
using DG.Tweening;
using UnityEngine;

namespace Player.MainCamera
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class FOV : MonoBehaviour
    {
        [Header("External References")] 
        [SerializeField] private Player _player;
        
        [Header("Settings")]
        [SerializeField] private float _targetFOV;
        [SerializeField] private float _changeFOVSpeedInSeconds = 0.1245f;
        
        private CinemachineVirtualCamera _cinemachineVirtualCamera;
        
        private float _initialFOV;

        private void Awake()
        {
            _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
            _initialFOV = _cinemachineVirtualCamera.m_Lens.FieldOfView;
        }

        private void OnEnable()
        {
            _player.SprintStateChangedEvent.Event += SprintStateChanged_Event;
        }

        private void OnDisable()
        {
            _player.SprintStateChangedEvent.Event -= SprintStateChanged_Event;
        }

        private void SprintStateChanged_Event(object sender, SprintStateChangedEventArgs e)
        {
            if (e.IsSprinting)
            {
                DOVirtual.Float(_initialFOV, _targetFOV, _changeFOVSpeedInSeconds, UpdateFOVValue)
                    .SetEase(Ease.Linear);

                return;
            }
            
            float currentZoomValue = _cinemachineVirtualCamera.m_Lens.FieldOfView;
            
            DOVirtual.Float(currentZoomValue, _initialFOV, _changeFOVSpeedInSeconds, UpdateFOVValue)
                .SetEase(Ease.Linear);
        }
        
        private void UpdateFOVValue(float value)
            => _cinemachineVirtualCamera.m_Lens.FieldOfView = value;
    }
}
