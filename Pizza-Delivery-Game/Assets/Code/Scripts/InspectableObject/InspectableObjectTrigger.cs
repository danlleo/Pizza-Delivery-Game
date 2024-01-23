using System;
using UI.InspectableObject;
using Zenject;

namespace InspectableObject
{
    public class InspectableObjectTrigger 
    {
        private UI.UI _ui;

        [Inject]
        private InspectableObjectTrigger(UI.UI ui)
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