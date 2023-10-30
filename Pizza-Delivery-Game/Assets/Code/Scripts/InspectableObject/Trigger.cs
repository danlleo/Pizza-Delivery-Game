using System;
using Misc;
using UI.InspectableObject;
using UnityEngine;

namespace InspectableObject
{
    [DisallowMultipleComponent]
    public class Trigger : Singleton<Trigger>
    {
        [SerializeField] private UI.UI _ui;
        
        public void Invoke(InspectableObjectSO inspectableObject, Action onComplete)
        {
            _ui.InspectableObjectOpeningEvent.Call(_ui,
                new InspectableObjectOpeningEventArgs(inspectableObject, onComplete));
        }
    }
}