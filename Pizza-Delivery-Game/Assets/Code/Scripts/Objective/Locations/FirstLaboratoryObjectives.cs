using System.Collections.Generic;
using Environment.LaboratoryFirstLevel;
using EventBus;
using Keypad;
using UI.Objective;
using UnityEngine;

namespace Objective.Locations
{
    public class FirstLaboratoryObjectives : ObjectiveManager
    {
        [Header("Initial list")]
        [SerializeField] private float _creationDelayTimeInSeconds;
        [Tooltip("Order in which objectives will be set")]
        [SerializeField] private List<ObjectiveSO> _objectiveList;

        [Header("Objectives")] 
        [SerializeField] private ObjectiveSO _exploreLaboratoryObjective;
        [SerializeField] private ObjectiveSO _fixPipesObjective;
        [SerializeField] private ObjectiveSO _findAWayToSecondLevelObjective;
        
        protected override List<ObjectiveSO> ObjectiveList => _objectiveList;
        protected override float CreationDelayTimeInSeconds => _creationDelayTimeInSeconds;

        private EventBinding<FixPipesEvent> _noWrenchEventBinding;
        private EventBinding<PasswordValidationResponseEvent> _passwordValidationEventResponseEventBinding;
        
        private void OnEnable()
        {
            GasLeakedStaticEvent.OnAnyGasLeaked += OnAnyGasLeaked;

            _noWrenchEventBinding = new EventBinding<FixPipesEvent>(Player_OnPipeFix);
            EventBus<FixPipesEvent>.Register(_noWrenchEventBinding);
            
            _passwordValidationEventResponseEventBinding =
                new EventBinding<PasswordValidationResponseEvent>(HandlePasswordValidationResponseEvent);
            EventBus<PasswordValidationResponseEvent>.Register(_passwordValidationEventResponseEventBinding);
        }

        private void OnDisable()
        {
            GasLeakedStaticEvent.OnAnyGasLeaked -= OnAnyGasLeaked;
            
            EventBus<FixPipesEvent>.Deregister(_noWrenchEventBinding);
            EventBus<PasswordValidationResponseEvent>.Deregister(_passwordValidationEventResponseEventBinding);
        }

        private void OnAnyGasLeaked(object sender, GasLeakedStaticEventArgs e)
        {
            FinishObjective(_exploreLaboratoryObjective);
        }

        private void Player_OnPipeFix(FixPipesEvent fixPipesEvent)
        {
            if (fixPipesEvent.HasFixed)
                FinishObjective(_fixPipesObjective);
        }

        private void HandlePasswordValidationResponseEvent(
            PasswordValidationResponseEvent passwordValidationResponseEvent)
        {
            if (!passwordValidationResponseEvent.IsCorrect)
                return;
            
            ToggleObjectiveWindowStaticEvent.Call(this, new ToggleObjectiveWindowStaticEventArgs(false));
            FinishObjective(_findAWayToSecondLevelObjective);
        }
    }
}