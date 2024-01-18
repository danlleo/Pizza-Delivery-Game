using DataPersistence;
using UnityEngine;

namespace Misc
{
    [DisallowMultipleComponent]
    public class GameManager : MonoBehaviour
    {
        private void Start()
        {
            LoadStaticEvent.CallLoadEvent(this);
        }

        public void Initialize()
        {
            InputAllowance.EnableInput();
        }
    }
}
    