using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Utilities.RestAction
{
    public class RestAction
    {
        private readonly Action _action;
        private readonly float _delayTimeInSeconds;
        
        private readonly TaskCompletionSource<bool> _taskCompletionSource = new();

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
                _taskCompletionSource.SetResult(true);

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
