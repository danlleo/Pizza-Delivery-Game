using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    [RequireComponent(typeof(StaminaEvent))]
    public class Player : MonoBehaviour
    {
        [FormerlySerializedAs("SprintingEvent")] public StaminaEvent _staminaEvent;

        public void Awake()
        {
            _staminaEvent = GetComponent<StaminaEvent>();
        }
    }
}
