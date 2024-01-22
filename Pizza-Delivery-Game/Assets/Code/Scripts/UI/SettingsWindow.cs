using System;
using DataPersistence;
using Misc;
using Player;
using Settings;
using UnityEngine;
using UnityEngine.UIElements;
using static Utilities.VisualElementCreationTool;

namespace UI
{
    public class SettingsWindow : VisualElement
    {
        public const string MouseSensitivitySliderName = "mouse-sensitivity-slider";
        
        private const string USS_CONTAINER = "container";
        private const string USS_WINDOW = "window";
        private const string USS_WINDOW_HEADER = "header";
        private const string USS_WINDOW_HEADER_TITLE = "title";
        private const string USS_WINDOW_MAIN = "main";
        private const string USS_WINDOW_MAIN_SETTING_OPTION = "setting-option";
        private const string USS_WINDOW_MAIN_SETTING_OPTION_TEXT = "option-name";
        private const string USS_WINDOW_MAIN_BTN_GROUP = "btn-group";
        private const string USS_WINDOW_MAIN_BTN_GROUP_BTN = "btn";
        
        public static Action OnAnyButtonClicked;
        
        public Action OnConfirm;
        public Action OnCancel;

        public new class UxmlFactory : UxmlFactory<SettingsWindow> { }
        
        public SettingsWindow()
        {
            float mouseSensitivitySliderValue = PlayerPrefs.HasKey(PlayerPrefsKeys.MouseSensitivity)
                ? PlayerPrefs.GetFloat(PlayerPrefsKeys.MouseSensitivity)
                : MouseLook.DefaultMouseSensitivity;

            bool vsyncEnabledValue = PlayerPrefs.HasKey(PlayerPrefsKeys.VSyncStatus) &&
                                     PlayerPrefs.GetInt(PlayerPrefsKeys.VSyncStatus) != 0;
            
            styleSheets.Add(GameResources.Retrieve.SettingsWindowStylesheet);
            AddToClassList(USS_CONTAINER);
            
            VisualElement window = Create(USS_WINDOW);
            hierarchy.Add(window);
            
            VisualElement header = Create(USS_WINDOW_HEADER);
            window.Add(header);
            var title = Create<Label>(USS_WINDOW_HEADER_TITLE);
            title.text = "SETTINGS";
            header.Add(title);
            
            VisualElement main = Create(USS_WINDOW_MAIN);
            window.Add(main);
            
            VisualElement mouseSensitivityOption = Create(USS_WINDOW_MAIN_SETTING_OPTION);
            var mouseSensitivityOptionText = Create<Label>(USS_WINDOW_MAIN_SETTING_OPTION_TEXT);
            mouseSensitivityOptionText.text = "Mouse Sensitivity";

            var mouseSensitivitySlider = Create<Slider>();
            mouseSensitivitySlider.lowValue = 0;
            mouseSensitivitySlider.highValue = 100;
            mouseSensitivitySlider.value = mouseSensitivitySliderValue;
            mouseSensitivitySlider.name = MouseSensitivitySliderName;
            mouseSensitivityOption.Add(mouseSensitivityOptionText);
            mouseSensitivityOption.Add(mouseSensitivitySlider);
            
            VisualElement vsyncOption = Create(USS_WINDOW_MAIN_SETTING_OPTION);
            var vsyncOptionText = Create<Label>(USS_WINDOW_MAIN_SETTING_OPTION_TEXT);
            vsyncOptionText.text = "Enable VSYNC";
            var vsyncOptionCheckBox = Create<Toggle>();
            vsyncOptionCheckBox.value = vsyncEnabledValue;
            vsyncOptionCheckBox.RegisterValueChangedCallback(CheckBoxCallback);
            vsyncOption.Add(vsyncOptionText);
            vsyncOption.Add(vsyncOptionCheckBox);
            
            VisualElement btnGroup = Create(USS_WINDOW_MAIN_BTN_GROUP);
            var confirmButton = Create<Button>(USS_WINDOW_MAIN_BTN_GROUP_BTN);
            confirmButton.clicked += () =>
            {
                SaveSettings(mouseSensitivitySlider.value, vsyncOptionCheckBox.value);
                OnAnyButtonClicked?.Invoke();
                OnConfirm?.Invoke();
            };
            confirmButton.text = "CONFIRM";
            var cancelButton = Create<Button>(USS_WINDOW_MAIN_BTN_GROUP_BTN);
            cancelButton.text = "CANCEL";
            cancelButton.clicked += () =>
            {
                OnAnyButtonClicked?.Invoke();
                OnCancel?.Invoke();
            };
            btnGroup.Add(confirmButton);
            btnGroup.Add(cancelButton);
            
            main.Add(mouseSensitivityOption);
            main.Add(vsyncOption);
            main.Add(btnGroup);
        }

        private void SaveSettings(float mouseSensitivity, bool vsyncEnabled)
        {
            PlayerPrefs.SetFloat(PlayerPrefsKeys.MouseSensitivity, mouseSensitivity);
            PlayerPrefs.SetInt(PlayerPrefsKeys.VSyncStatus, vsyncEnabled ? 1 : 0);
            OnAnySettingsChanged.Call(this, new OnAnySettingsChangedArgs(mouseSensitivity, vsyncEnabled));
        }
        
        private void CheckBoxCallback(ChangeEvent<bool> evt)
        {
            OnAnyButtonClicked?.Invoke();
        }
    }
}
