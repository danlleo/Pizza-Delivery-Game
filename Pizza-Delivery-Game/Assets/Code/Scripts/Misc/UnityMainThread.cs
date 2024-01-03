using System;
using System.Collections.Generic;
using UnityEngine;

namespace Misc
{
    internal class UnityMainThread : MonoBehaviour
    {
        internal static UnityMainThread Instance;
        private readonly Queue<Action> _jobs = new();

        private void Awake() {
            Instance = this;
        }

        private void Update() {
            while (_jobs.Count > 0) 
                _jobs.Dequeue().Invoke();
        }

        internal void AddJob(Action newJob) {
            _jobs.Enqueue(newJob);
        }

        internal void DestroyInstance()
            => Destroy(this);
    }
}
