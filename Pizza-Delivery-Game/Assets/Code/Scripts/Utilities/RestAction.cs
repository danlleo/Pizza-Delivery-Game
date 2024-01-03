using System;
using System.Threading;
using System.Threading.Tasks;
using Misc;
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
            
            if (_owner.GetComponent<UnityMainThread>() == null)
            {
                _owner.gameObject.AddComponent<UnityMainThread>();
            }
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
                    UnityMainThread.Instance.AddJob(() => _action?.Invoke());
                    _nextAction?.Execute();
                }
                catch (TaskCanceledException ex)
                {
                    Debug.LogError("Exception in TaskCanceledException: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Debug.LogError("Exception in RestAction: " + ex.Message);
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