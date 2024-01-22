using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace TimeControl
{
    public class TimeControl : IInitializable, IDisposable
    {
        private void Pause()
            => Time.timeScale = 0f;

        private void Unpause()
            => Time.timeScale = 1f;
        
        public void Initialize()
        {
            global::TimeControl.OnAnyGameUnpaused.Event += OnAnyGameUnpaused;
            global::TimeControl.OnAnyGamePaused.Event += OnAnyGamePaused;
            SceneManager.activeSceneChanged += SceneManager_OnActiveSceneChanged;
        }

        public void Dispose()
        {
            global::TimeControl.OnAnyGameUnpaused.Event -= OnAnyGameUnpaused;
            global::TimeControl.OnAnyGamePaused.Event -= OnAnyGamePaused;
            SceneManager.activeSceneChanged -= SceneManager_OnActiveSceneChanged;
        }

        private void SceneManager_OnActiveSceneChanged(Scene arg0, Scene arg1)
        {
            Dispose();
        }

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
