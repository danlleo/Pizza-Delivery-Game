using UI.Dialogue;
using UI.InspectableObject;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(DialogueOpeningEvent))]
    [RequireComponent(typeof(DialogueClosingEvent))]
    [RequireComponent(typeof(InspectableObjectFinishedReadingEvent))]
    [RequireComponent(typeof(InspectableObjectOpeningEvent))]
    [RequireComponent(typeof(InspectableObjectClosingEvent))]
    [RequireComponent(typeof(InspectableObjectCloseEvent))]
    [RequireComponent(typeof(ConfirmEvent))]
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

        [HideInInspector] public ConfirmEvent ConfirmEvent;

        [HideInInspector] public OnObjectiveUpdated OnObjectiveUpdated;

        private void Awake()
        {
            DialogueOpeningEvent = GetComponent<DialogueOpeningEvent>();
            DialogueClosingEvent = GetComponent<DialogueClosingEvent>();
            
            InspectableObjectFinishedReadingEvent = GetComponent<InspectableObjectFinishedReadingEvent>();
            InspectableObjectOpeningEvent = GetComponent<InspectableObjectOpeningEvent>();
            InspectableObjectClosingEvent = GetComponent<InspectableObjectClosingEvent>();
            InspectableObjectCloseEvent = GetComponent<InspectableObjectCloseEvent>();

            ConfirmEvent = GetComponent<ConfirmEvent>();

            OnObjectiveUpdated = GetComponent<OnObjectiveUpdated>();
        }
    }
}
