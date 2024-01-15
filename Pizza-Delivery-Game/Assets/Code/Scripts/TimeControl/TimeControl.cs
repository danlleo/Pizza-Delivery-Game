using System;
using UnityEngine;

namespace TimeControl
{
    [DisallowMultipleComponent]
    public class TimeControl : MonoBehaviour
    {
        private void Awake()
        {
            Unpause();
        }

        private void OnEnable()
        {
            global::TimeControl.OnAnyGameUnpaused.Event += OnAnyGameUnpaused;
            global::TimeControl.OnAnyGamePaused.Event += OnAnyGamePaused;
        }

        private void OnDisable()
        {
            global::TimeControl.OnAnyGameUnpaused.Event -= OnAnyGameUnpaused;
            global::TimeControl.OnAnyGamePaused.Event -= OnAnyGamePaused;
        }

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
    }
}
