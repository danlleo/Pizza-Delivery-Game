using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class FunctionTimer
    {
        private static List<FunctionTimer> s_activeTimerList;
        private static GameObject s_initGameObject;
        
        public static FunctionTimer Create(Action action, float timer, string timerName = null)
        {
            InitIfNeeded();
            
            var gameObject = new GameObject(nameof(FunctionTimer), typeof(MonoBehaviourHook));
            var functionTimer = new FunctionTimer(action, timer, timerName, gameObject);
            gameObject.GetComponent<MonoBehaviourHook>().OnUpdate = functionTimer.Update;

            s_activeTimerList.Add(functionTimer);
            
            return functionTimer;
        }
        
        private static void InitIfNeeded()
        {
            if (s_initGameObject == null)
            {
                s_initGameObject = new GameObject("FunctionTimer_InitGameObject");
                s_activeTimerList = new List<FunctionTimer>();
            }
        }
        
        private static void RemoveTimer(FunctionTimer functionTimer)
        {
            InitIfNeeded();
            s_activeTimerList.Remove(functionTimer);
        }

        private static void StopTimer(string timerName)
        {
            for (int i = 0; i < s_activeTimerList.Count; i++)
            {
                if (s_activeTimerList[i]._timerName == timerName)
                {
                    s_activeTimerList[i].DestroySelf();
                    i--;
                }
            }
        }
        
        private class MonoBehaviourHook : MonoBehaviour
        {
            public Action OnUpdate;
            
            private void Update()
            {
                OnUpdate?.Invoke();
            }
        }
        
        private Action _action;
        private float _timer;
        private string _timerName;
        private GameObject _gameObject;
        private bool _isDestroyed;

        private FunctionTimer(Action action, float timer, string timerName, GameObject gameObject)
        {
            _action = action;
            _timer = timer;
            _timerName = timerName;
            _gameObject = gameObject;
            _isDestroyed = false;
        }

        private void Update()
        {
            _timer -= Time.deltaTime;

            if (!(_timer <= 0f)) return;
            
            _action?.Invoke();
            DestroySelf();
        }

        private void DestroySelf()
        {
            _isDestroyed = true;
            UnityEngine.Object.Destroy(_gameObject);
            RemoveTimer(this);
        }
    }
}
