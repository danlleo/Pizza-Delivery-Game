using System;
using UnityEngine;
using Zenject;

namespace TimeControl
{
    public class TimeControl : IInitializable, IDisposable
    {
        private void Pause()
            => Time.timeScale = 0f;

        private void Unpause()
            => Time.timeScale = 1f;
        
        private void OnAnyGamePaused(object sender, EventArgs e)
        {
            Pause();
        }
        
        private void OnAnyGameUnpaused(object sender, EventArgs e)
        {
            Unpause();
        }
        
        public void Initialize()
        {
            global::TimeControl.OnAnyGameUnpaused.Event += OnAnyGameUnpaused;
            global::TimeControl.OnAnyGamePaused.Event += OnAnyGamePaused;
        }

        public void Dispose()
        {
            global::TimeControl.OnAnyGameUnpaused.Event -= OnAnyGameUnpaused;
            global::TimeControl.OnAnyGamePaused.Event -= OnAnyGamePaused;
        }
    }
}
