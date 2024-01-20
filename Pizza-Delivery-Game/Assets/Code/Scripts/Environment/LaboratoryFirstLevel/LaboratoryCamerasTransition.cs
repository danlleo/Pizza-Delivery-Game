using Cinemachine;
using EventBus;
using Keypad;
using Misc;
using UnityEngine;
using Zenject;

namespace Environment.LaboratoryFirstLevel
{
    public class LaboratoryCamerasTransition : Singleton<LaboratoryCamerasTransition>
    {
        [Header("External references")] 
        [SerializeField] private CinemachineVirtualCamera _keypadVirtualCamera;        
        
        [Header("Settings")]
        [SerializeField] private int _lowPriorityValue = 1;
        [SerializeField] private int _highPriorityValue = 10;
        
        private CinemachineVirtualCamera _mainVirtualCamera;

        private EventBinding<InteractedWithKeypadEvent> _interactedWithKeypadEventBinding;

        [Inject]
        private void Construct(Player.Player player)
        {
            _mainVirtualCamera = player.MainVirtualCamera;
        }

        private void OnEnable()
        {
            _interactedWithKeypadEventBinding =
                new EventBinding<InteractedWithKeypadEvent>(HandleInteractedWithKeypadEvent);

            EventBus<InteractedWithKeypadEvent>.Register(_interactedWithKeypadEventBinding);
        }

        private void OnDisable()
        {
            EventBus<InteractedWithKeypadEvent>.Deregister(_interactedWithKeypadEventBinding);
        }

        private void HandleInteractedWithKeypadEvent(InteractedWithKeypadEvent interactedWithKeypadEvent)
        {
            _mainVirtualCamera.Priority = _lowPriorityValue;
            _keypadVirtualCamera.Priority = _highPriorityValue;
        }
    }
}
