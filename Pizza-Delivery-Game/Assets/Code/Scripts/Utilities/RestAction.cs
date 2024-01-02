using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Utilities
{
    public class RestAction
    {
        private readonly Action _action;
        private readonly float _delayTimeInSeconds;

        private MonoBehaviour _owner;
        private RestAction _nextAction;
        
        private CancellationToken _destroyCancellationToken;

        public RestAction(MonoBehaviour owner)
        {
            _owner = owner;
        }
        
        private RestAction(MonoBehaviour owner, Action action, float delayTimeInSeconds)
        {
            _owner = owner;
            _action = action;
            _delayTimeInSeconds = delayTimeInSeconds;
            _destroyCancellationToken = owner.destroyCancellationToken;
        }

        public void Execute()
        {
            Task.Run(async () =>
            {
                try
                {
                    await Task.Delay((int)Mathf.Round(_delayTimeInSeconds * 1000f), _destroyCancellationToken);
                    _action?.Invoke();

                    _nextAction?.Execute();
                }
                catch (TaskCanceledException)
                {
                    
                }
            }, _destroyCancellationToken);
        }

        public RestAction AddChain(Action action, float delayTimeInSeconds = 0f)
        {
            _nextAction = new RestAction(_owner, action, delayTimeInSeconds);
            return _nextAction;
        }
    }
}
