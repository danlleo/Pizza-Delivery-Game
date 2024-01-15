using UI.Dialogue;
using UI.InspectableObject;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    [RequireComponent(typeof(DialogueOpeningEvent))]
    [RequireComponent(typeof(DialogueClosingEvent))]
    [RequireComponent(typeof(InspectableObjectFinishedReadingEvent))]
    [RequireComponent(typeof(InspectableObjectOpeningEvent))]
    [RequireComponent(typeof(InspectableObjectClosingEvent))]
    [RequireComponent(typeof(InspectableObjectCloseEvent))]
    [RequireComponent(typeof(InspectableObjectConfirmEvent))]
    [RequireComponent(typeof(OnObjectiveUpdated))]
    [DisallowMultipleComponent]
    public class UI : MonoBehaviour
    {
        [HideInInspector] public DialogueOpeningEvent DialogueOpeningEvent;
        [HideInInspector] public DialogueClosingEvent DialogueClosingEvent;

        [HideInInspector] public InspectableObjectFinishedReadingEvent InspectableObjectFinishedReadingEvent;
        [HideInInspector] public InspectableObjectOpeningEvent InspectableObjectOpeningEvent;
        [HideInInspector] public InspectableObjectClosingEvent InspectableObjectClosingEvent;
        [HideInInspector] public InspectableObjectCloseEvent InspectableObjectCloseEvent;

        [FormerlySerializedAs("ConfirmEvent")] [HideInInspector] public InspectableObjectConfirmEvent _inspectableObjectConfirmEvent;

        [HideInInspector] public OnObjectiveUpdated OnObjectiveUpdated;
        
        private void Awake()
        {
            DialogueOpeningEvent = GetComponent<DialogueOpeningEvent>();
            DialogueClosingEvent = GetComponent<DialogueClosingEvent>();

            InspectableObjectFinishedReadingEvent = GetComponent<InspectableObjectFinishedReadingEvent>();
            InspectableObjectOpeningEvent = GetComponent<InspectableObjectOpeningEvent>();
            InspectableObjectClosingEvent = GetComponent<InspectableObjectClosingEvent>();
            InspectableObjectCloseEvent = GetComponent<InspectableObjectCloseEvent>();

            _inspectableObjectConfirmEvent = GetComponent<InspectableObjectConfirmEvent>();

            OnObjectiveUpdated = GetComponent<OnObjectiveUpdated>();
        }
    }
}