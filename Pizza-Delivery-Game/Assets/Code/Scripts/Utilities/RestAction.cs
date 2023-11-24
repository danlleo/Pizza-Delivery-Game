using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Utilities
{
    public class RestAction
    {
        private readonly Action _action;
        private readonly float _delayTimeInSeconds;
        
        private RestAction _nextAction;

        public RestAction() { }
        
        private RestAction(Action action, float delayTimeInSeconds)
        {
            _action = action;
            _delayTimeInSeconds = delayTimeInSeconds;
        }

        public void PerformChain()
        {
            Task.Run(async () =>
            {
                await Task.Delay((int)Mathf.Round(_delayTimeInSeconds * 1000f));
                _action?.Invoke();

                _nextAction?.PerformChain();
            });
        }

        public RestAction Continue(Action action, float delayTimeInSeconds)
        {
            _nextAction = new RestAction(action, delayTimeInSeconds);
            return _nextAction;
        }
    }
}
