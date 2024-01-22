using System;
using System.Diagnostics.CodeAnalysis;
using Settings;
using UnityEngine;
using Zenject;

namespace Misc
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class ScreenSettings : IInitializable, IDisposable
    {
        private bool _isVsyncEnabled;

        public ScreenSettings(bool isVsyncEnabled)
        {
            _isVsyncEnabled = isVsyncEnabled;
        }

        private void ToggleVsync()
            => QualitySettings.vSyncCount = _isVsyncEnabled ? 1 : 0;

        public void Initialize()
        {
            ToggleVsync();
            Settings.OnAnySettingsChanged.Event += OnAnySettingsChanged;
        }
        
        public void Dispose()
        {
            Settings.OnAnySettingsChanged.Event -= OnAnySettingsChanged;
        }
        
        private void OnAnySettingsChanged(object sender, OnAnySettingsChangedArgs e)
        {
            _isVsyncEnabled = e.VSyncEnabled;
            ToggleVsync();
        }
    }
}
