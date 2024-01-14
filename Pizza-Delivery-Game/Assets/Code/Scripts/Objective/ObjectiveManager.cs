using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objective
{
    [DisallowMultipleComponent]
    public abstract class ObjectiveManager : MonoBehaviour
    {
        protected abstract List<ObjectiveSO> ObjectiveList { get; }
        protected abstract float CreationDelayTimeInSeconds { get; }
        
        private ObjectiveRegistry _objectiveRegistry;
        
        private IEnumerator Start()
        {
            yield return new WaitForSeconds(CreationDelayTimeInSeconds);
            
            if (ObjectiveList == null)
                throw new Exception("Objective list isn't initialized");
                
            if (ObjectiveList.Count == 0)
                throw new Exception("Objective list is empty!");
            
            _objectiveRegistry = ObjectiveRegistry.Create(ObjectiveList);
        }

        private void OnDestroy()
        {
            if (ObjectiveList == null)
                throw new Exception("Objective list isn't initialized");
            
            _objectiveRegistry?.Delete();
        }

        protected void FinishObjective(ObjectiveSO objective)
        {
            if (objective == null)
                throw new Exception("Objective is null");

            if (!_objectiveRegistry.TryGetCurrentObjective(out Objective currentObjective))
                return;
            
            if (currentObjective.GetObjectiveSO().ID == objective.ID)
                currentObjective.Finish();
        }
    }
}