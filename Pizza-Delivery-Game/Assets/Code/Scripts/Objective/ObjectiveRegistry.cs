using System.Collections.Generic;
using UnityEngine;

namespace Objective
{
    public sealed class ObjectiveRegistry
    {
        private Queue<Objective> _objectiveQueue;

        private Objective _currentObjective;
        
        private ObjectiveRegistry(List<ObjectiveSO> objectiveList)
        {
            _objectiveQueue = new Queue<Objective>();
            
            foreach (ObjectiveSO objective in objectiveList)
            {
                _objectiveQueue.Enqueue(new Objective(objective, this));
            }
            
            SetNextObjective();
        }

        public static ObjectiveRegistry Create(List<ObjectiveSO> objectiveList)
        {
            var objectiveRegistry = new ObjectiveRegistry(objectiveList);

            if (objectiveRegistry.TryGetCurrentObjective(out Objective objective))
            {
                FirstObjectiveSetStaticEvent.Call(objectiveRegistry,
                    new FirstObjectiveSetStaticEventArgs(objective));
            }
            else
            {
                Debug.LogWarning("Objective List is null!");
            }
            
            return objectiveRegistry;
        }

        public void Delete()
        {
            _objectiveQueue.Clear();
        }

        public bool TryGetCurrentObjective(out Objective currentObjective)
        {
            if (_currentObjective == null)
            {
                Debug.LogWarning("Current objective is null");
                currentObjective = null;
                return false;
            }
    
            currentObjective = _currentObjective;
            return true;
        }

        public void SetNextObjective()
        {
            if (_objectiveQueue.Count == 0)
            {
                Debug.LogWarning("Can't set new objective because queue is empty!");
                _currentObjective = null;
                return;
            }
            
            _currentObjective = _objectiveQueue.Dequeue();
        }
    }
}