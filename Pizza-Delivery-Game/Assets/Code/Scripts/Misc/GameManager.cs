using DataPersistence;
using UnityEngine;

namespace Misc
{
    [DisallowMultipleComponent]
    public class GameManager : MonoBehaviour
    {
        public void Start()
        {
            LoadStaticEvent.CallLoadEvent(this);
            InputAllowance.EnableInput();
        }
    }
}
    