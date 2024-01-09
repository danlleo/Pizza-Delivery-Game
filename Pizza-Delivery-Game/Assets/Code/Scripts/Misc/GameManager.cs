using DataPersistence;
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

        private void Start()
        {
            LoadStaticEvent.CallLoadEvent(this);
        }
    }
}
    