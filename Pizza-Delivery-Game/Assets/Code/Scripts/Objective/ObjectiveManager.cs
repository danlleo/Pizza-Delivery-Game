using System;
using System.Collections.Generic;
using UnityEngine;

namespace Objective
{
    [DisallowMultipleComponent]
    public abstract class ObjectiveManager : MonoBehaviour
    {
        protected abstract List<ObjectiveSO> ObjectiveList { get; }

        private ObjectiveRegistry _objectiveRegistry;
        
        private void Start()
        {
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
            
            _objectiveRegistry.Delete();
        }

        protected void FinishObjective()
        {
            if (_objectiveRegistry.TryGetCurrentObjective(out Objective objective))
            {
                objective.Finish();
            }
        }
    }
}