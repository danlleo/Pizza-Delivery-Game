using UnityEngine;

namespace Misc
{
    public class ScreenSettings : Singleton<ScreenSettings>
    {
        [Header("Settings")] 
        [SerializeField] private bool _isVsyncEnabled;
        [SerializeField] private bool _isFpsLocked;
        
        protected override void Awake()
        {
            base.Awake();
        
            ToggleVsync();
            ToggleFPSLimit();
        }

        private void ToggleVsync()
            => QualitySettings.vSyncCount = _isVsyncEnabled ? 1 : 0;

        private void ToggleFPSLimit()
        {
            if (_isFpsLocked)
                Application.targetFrameRate = 60;
        }
    }
}
