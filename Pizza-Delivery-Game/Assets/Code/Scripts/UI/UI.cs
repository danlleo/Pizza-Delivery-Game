using UI.Dialogue;
using UI.InspectableObject;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(DialogueOpeningEvent))]
    [RequireComponent(typeof(DialogueClosingEvent))]
    [RequireComponent(typeof(InspectableObjectFinishedReadingEvent))]
    [RequireComponent(typeof(InspectableObjectClosingEvent))]
    [RequireComponent(typeof(InspectableObjectOpeningEvent))]
    [DisallowMultipleComponent]
    public class UI : MonoBehaviour
    {
        [HideInInspector] public DialogueOpeningEvent DialogueOpeningEvent;
        [HideInInspector] public DialogueClosingEvent DialogueClosingEvent;
        
        [HideInInspector] public InspectableObjectFinishedReadingEvent InspectableObjectFinishedReadingEvent;
        [HideInInspector] public InspectableObjectOpeningEvent InspectableObjectOpeningEvent;
        [HideInInspector] public InspectableObjectClosingEvent InspectableObjectClosingEvent;

        private void Awake()
        {
            DialogueOpeningEvent = GetComponent<DialogueOpeningEvent>();
            DialogueClosingEvent = GetComponent<DialogueClosingEvent>();
            InspectableObjectFinishedReadingEvent = GetComponent<InspectableObjectFinishedReadingEvent>();
            InspectableObjectOpeningEvent = GetComponent<InspectableObjectOpeningEvent>();
            InspectableObjectClosingEvent = GetComponent<InspectableObjectClosingEvent>();
        }
    }
}
