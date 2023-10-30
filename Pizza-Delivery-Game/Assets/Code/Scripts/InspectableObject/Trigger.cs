using Misc;
using UI.InspectableObject;
using UnityEngine;

namespace InspectableObject
{
    [DisallowMultipleComponent]
    public class Trigger : Singleton<Trigger>
    {
        [SerializeField] private Player.Player _player;
        
        public void Invoke(InspectableObjectSO inspectableObject)
        {
            _player._inspectableObjectOpeningEvent.Call(_player,
                new InspectableObjectOpeningEventArgs(inspectableObject));
        }
    }
}