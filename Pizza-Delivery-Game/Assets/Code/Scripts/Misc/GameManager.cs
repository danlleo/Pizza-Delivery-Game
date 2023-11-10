using UnityEngine;

namespace Misc
{
    [DisallowMultipleComponent]
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            InputAllowance.EnableInput();
        }
    }
}
    