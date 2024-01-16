using System;
using System.Collections.Generic;
using Environment.Bedroom;
using Environment.Bedroom.PC;
using UI.Objective;
using UnityEngine;

namespace Objective.Locations
{
    public class BedroomObjectives : ObjectiveManager
    {
        [Header("Initial list")]
        [SerializeField] private float _creationDelayTimeInSeconds;
        [Tooltip("Order in which objectives will be set")]
        [SerializeField] private List<ObjectiveSO> _objectiveList;
        
        [Header("Objectives")] 
        [SerializeField] private ObjectiveSO _usePCObjective;
        [SerializeField] private ObjectiveSO _openDoorObjective;
        
        protected override List<ObjectiveSO> ObjectiveList => _objectiveList;
        protected override float CreationDelayTimeInSeconds => _creationDelayTimeInSeconds;

        private void OnEnable()
        {
            OnAnyStartedUsingPC.Event += StartedUsingPCStaticEventEvent;
            WokeUpStaticEvent.OnWokeUp += WokeUpStaticEvent_OnWokeUp;
            OpenedDoorStaticEvent.OnOpenedDoor += OnOpenedDoor;
        }

        private void OnDisable()
        {
            OnAnyStartedUsingPC.Event -= StartedUsingPCStaticEventEvent;
            WokeUpStaticEvent.OnWokeUp -= WokeUpStaticEvent_OnWokeUp;             
            OpenedDoorStaticEvent.OnOpenedDoor -= OnOpenedDoor;
        }

        private void StartedUsingPCStaticEventEvent(object sender, EventArgs e)
        {
            ToggleObjectiveWindowStaticEvent.Call(this, new ToggleObjectiveWindowStaticEventArgs(false));
        }
        
        private void WokeUpStaticEvent_OnWokeUp(object sender, EventArgs e)
        {
            FinishObjective(_usePCObjective);
            ToggleObjectiveWindowStaticEvent.Call(this, new ToggleObjectiveWindowStaticEventArgs(true));
        }
        
        private void OnOpenedDoor(object sender, EventArgs e)
        {
            FinishObjective(_openDoorObjective);
            ToggleObjectiveWindowStaticEvent.Call(this, new ToggleObjectiveWindowStaticEventArgs(false));
        }
    }
}