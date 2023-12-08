using System;
using System.Collections.Generic;
using Environment.Bedroom.PC;
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
        }

        private void StartedUsingPCStaticEvent_OnStarted(object sender, EventArgs e)
        {
            FinishObjective();
        }
    }
}