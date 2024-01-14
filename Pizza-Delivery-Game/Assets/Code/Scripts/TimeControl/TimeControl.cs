using System;
using UnityEngine;

namespace TimeControl
{
    [DisallowMultipleComponent]
    public class TimeControl : MonoBehaviour
    {
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

        private void OnAnyGamePaused(object sender, EventArgs e)
        {
            Time.timeScale = 0f;
        }
        
        private void OnAnyGameUnpaused(object sender, EventArgs e)
        {
            Time.timeScale = 1f;
        }
    }
}
