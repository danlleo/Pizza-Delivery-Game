using System;
using System.Collections.Generic;
using Environment.LaboratoryFirstLevel;
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
        [SerializeField] private ObjectiveSO _getAccessToRoomAObjective;
        [SerializeField] private ObjectiveSO _getAccessToRoomBObjective;
        [SerializeField] private ObjectiveSO _getAccessToRoomCObjective;
        [SerializeField] private ObjectiveSO _fixPipesObjective;
        [SerializeField] private ObjectiveSO _proceedToSecondLevelObjective;
        
        protected override List<ObjectiveSO> ObjectiveList => _objectiveList;
        protected override float CreationDelayTimeInSeconds => _creationDelayTimeInSeconds;

        private void OnEnable()
        {
            PickedUpKeycardAStaticEvent.OnAnyPickedUpKeycardA += OnAnyPickedUpKeycardA;
            PickedUpKeycardBStaticEvent.OnAnyPickedUpKeycardB += OnAnyPickedUpKeycardB;
            PickedUpKeycardCStaticEvent.OnAnyPickedUpKeycardC += OnAnyPickedUpKeycardC;
        }

        private void OnDisable()
        {
            PickedUpKeycardAStaticEvent.OnAnyPickedUpKeycardA -= OnAnyPickedUpKeycardA;
            PickedUpKeycardBStaticEvent.OnAnyPickedUpKeycardB -= OnAnyPickedUpKeycardB;
            PickedUpKeycardCStaticEvent.OnAnyPickedUpKeycardC -= OnAnyPickedUpKeycardC;
        }

        private void OnAnyPickedUpKeycardA(object sender, EventArgs e)
        {
            FinishObjective(_getAccessToRoomAObjective);
        }
        
        private void OnAnyPickedUpKeycardB(object sender, EventArgs e)
        {
            FinishObjective(_getAccessToRoomBObjective);
        }

        private void OnAnyPickedUpKeycardC(object sender, EventArgs e)
        {
            FinishObjective(_getAccessToRoomCObjective);
        }
    }
}