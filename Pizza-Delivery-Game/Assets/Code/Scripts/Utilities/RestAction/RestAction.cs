using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Utilities.RestAction
{
    public class RestAction
    {
        private readonly Action _action;
        private readonly float _delayTimeInSeconds;

        public RestAction(Action action, float delayTimeInSeconds)
        {
            _action = action;
            _delayTimeInSeconds = delayTimeInSeconds;
        }

        public async Task<RestAction> Execute()
        {
            await Task.Delay((int)Mathf.Round(_delayTimeInSeconds * 1000f));
            _action();

            return this;
        }

        public RestAction Continue(Action action, float delayTimeInSeconds)
        {
            return new RestAction(async () =>
            {
                await Execute();
                action?.Invoke();
            }, delayTimeInSeconds);
        }
    }
}
