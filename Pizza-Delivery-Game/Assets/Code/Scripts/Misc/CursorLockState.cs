using UnityEngine;

namespace Misc
{
    [DisallowMultipleComponent]
    public class CursorLockState : MonoBehaviour
    {
        [SerializeField] private bool _isLocked;

        private void Start()
        {
            if (!_isLocked)
                return;

            LockCursor();
        }

        private void OnEnable()
        {
            CursorLockStateChangedStaticEvent.OnCursorLockStatedChanged += OnCursorLockStatedChanged;
        }

        private void OnDisable()
        {
            CursorLockStateChangedStaticEvent.OnCursorLockStatedChanged -= OnCursorLockStatedChanged;
        }
        
        private void LockCursor()
            => Cursor.lockState = CursorLockMode.Locked;

        private void UnlockCursor()
            => Cursor.lockState = CursorLockMode.None;
        
        private void OnCursorLockStatedChanged(object sender, CursorLockStateChangedStaticEventArgs e)
        {
            if (e.IsLocked)
            {
                LockCursor();
                return;
            }
            
            UnlockCursor();
        }
    }
}
