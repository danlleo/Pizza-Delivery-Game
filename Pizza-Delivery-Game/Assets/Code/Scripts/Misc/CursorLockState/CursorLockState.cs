using UnityEngine;

namespace Misc.CursorLockState
{
    public class CursorLockState
    {
        public CursorLockState(bool isLocked)
        {
            if (isLocked) 
                LockCursor();
        }
        
        public void LockCursor()
            => Cursor.lockState = CursorLockMode.Locked;

        public void UnlockCursor()
            => Cursor.lockState = CursorLockMode.None;
    }
}
