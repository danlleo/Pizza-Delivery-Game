using UnityEngine;

namespace Misc
{
    public class CursorLockState : MonoBehaviour
    {
        [SerializeField] private bool _isLocked;

        private void Start()
        {
            if (!_isLocked)
                return;

            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
