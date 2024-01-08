using DataPersistence;
using UI;
using UI.Crossfade;
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
    