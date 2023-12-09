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
        [SerializeField] private List<ObjectiveSO> _objectiveList;

        protected override List<ObjectiveSO> ObjectiveList => _objectiveList;

        private void OnEnable()
        {
            StartedUsingPCStaticEvent.OnStarted += StartedUsingPCStaticEvent_OnStarted;
            WokeUpStaticEvent.OnWokeUp += WokeUpStaticEvent_OnWokeUp;
            OpenedDoorStaticEvent.OnOpenedDoor += OnOpenedDoor;
        }

        private void OnDisable()
        {
            StartedUsingPCStaticEvent.OnStarted -= StartedUsingPCStaticEvent_OnStarted;
            WokeUpStaticEvent.OnWokeUp -= WokeUpStaticEvent_OnWokeUp;             
            OpenedDoorStaticEvent.OnOpenedDoor -= OnOpenedDoor;
        }

        private void StartedUsingPCStaticEvent_OnStarted(object sender, EventArgs e)
        {
            ToggleObjectiveWindowStaticEvent.Call(this, new ToggleObjectiveWindowStaticEventArgs(false));
        }
        
        private void WokeUpStaticEvent_OnWokeUp(object sender, EventArgs e)
        {
            FinishObjective();
            ToggleObjectiveWindowStaticEvent.Call(this, new ToggleObjectiveWindowStaticEventArgs(true));
        }
        
        private void OnOpenedDoor(object sender, EventArgs e)
        {
            FinishObjective();
            ToggleObjectiveWindowStaticEvent.Call(this, new ToggleObjectiveWindowStaticEventArgs(false));
        }
    }
}