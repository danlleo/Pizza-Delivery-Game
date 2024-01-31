using System;
using System.Collections.Generic;
using Environment.LaboratorySecondLevel;
using UI.Objective;
using UnityEngine;

namespace Objective.Locations
{
    public class SecondLaboratoryLevelObjectives : ObjectiveManager
    {
        [Header("Initial list")]
        [SerializeField] private float _creationDelayTimeInSeconds;
        [Tooltip("Order in which objectives will be set")]
        [SerializeField] private List<ObjectiveSO> _objectiveList;

        [Header("Objectives")] 
        [SerializeField] private ObjectiveSO _findPizzaBoxObjective;
        [SerializeField] private ObjectiveSO _leaveLaboratoryObjective;
        
        protected override List<ObjectiveSO> ObjectiveList => _objectiveList;
        protected override float CreationDelayTimeInSeconds => _creationDelayTimeInSeconds;

        private void OnEnable()
        {
            PizzaBox.OnAnyPlayerPickedUpPizzaBox += PizzaBox_OnAnyPlayerPickedUpPizzaBox;
            Environment.LaboratorySecondLevel.Door.OnAnyLeftLaboratory += Door_OnAnyLeftLaboratory;
        }

        private void OnDisable()
        {
            PizzaBox.OnAnyPlayerPickedUpPizzaBox -= PizzaBox_OnAnyPlayerPickedUpPizzaBox;
            Environment.LaboratorySecondLevel.Door.OnAnyLeftLaboratory -= Door_OnAnyLeftLaboratory;
        }

        private void PizzaBox_OnAnyPlayerPickedUpPizzaBox(object sender, EventArgs e)
        {
            FinishObjective(_findPizzaBoxObjective);
        }
        
        private void Door_OnAnyLeftLaboratory(object sender, EventArgs e)
        {
            FinishObjective(_leaveLaboratoryObjective);
            ToggleObjectiveWindowStaticEvent.Call(this, new ToggleObjectiveWindowStaticEventArgs(false));
        }
    }
}
