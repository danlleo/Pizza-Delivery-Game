using UnityEngine;

namespace Misc
{
    [DisallowMultipleComponent]
    public class GameManager : MonoBehaviour
    {
        public void Start()
        {
            InputAllowance.EnableInput();
        }
    }
}
    