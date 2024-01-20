using System;
using Misc;
using UI.InspectableObject;
using UnityEngine;
using Zenject;

namespace InspectableObject
{
    [DisallowMultipleComponent]
    public class InspectableObjectTrigger : Singleton<InspectableObjectTrigger>
    {
        private UI.UI _ui;

        [Inject]
        private void Construct(UI.UI ui)
        {
            _ui = ui;
        }
        
        public void Invoke(InspectableObjectSO inspectableObject, Action onComplete)
        {
            _ui.InspectableObjectOpeningEvent.Call(_ui,
                new InspectableObjectOpeningEventArgs(inspectableObject, onComplete));
        }
    }
}