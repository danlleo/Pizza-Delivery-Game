using System;
using Misc;
using UnityEngine.UIElements;

namespace UI.MainMenu
{
    public class SettingsWindow : VisualElement
    {
        public static Action OnAnyButtonClicked;
        
        public Action OnConfirm;
        public Action OnCancel;
        
        public new class UxmlFactory : UxmlFactory<PopupWindow> { }

        public SettingsWindow()
        {
            styleSheets.Add(GameResources.Retrieve.SettingsWindowStylesheet);
            AddToClassList("container");
            
            VisualElement window = Create("window");
            hierarchy.Add(window);

            VisualElement header = Create("header");
            window.Add(header);
            var title = Create<Label>("title");
            title.text = "SETTINGS";
            header.Add(title);
            
            VisualElement main = Create("main");
            window.Add(main);

            VisualElement mouseSensitivityOption = Create("setting-option");
            var mouseSensitivityOptionText = Create<Label>("option-name");
            mouseSensitivityOptionText.text = "Mouse Sensitivity";
            var mouseSensitivitySlider = Create<Slider>();
            mouseSensitivitySlider.name = "mouse-sensitivity-slider";
            mouseSensitivityOption.Add(mouseSensitivityOptionText);
            mouseSensitivityOption.Add(mouseSensitivitySlider);

            VisualElement vsyncOption = Create("setting-option");
            var vsyncOptionText = Create<Label>("option-name");
            vsyncOptionText.text = "Enable VSYNC";
            var vsyncOptionCheckBox = Create<Toggle>();
            vsyncOptionCheckBox.RegisterValueChangedCallback(CheckBoxCallback);
            vsyncOption.Add(vsyncOptionText);
            vsyncOption.Add(vsyncOptionCheckBox);

            VisualElement btnGroup = Create("btn-group");
            var confirmButton = Create<Button>("btn");
            confirmButton.clicked += () =>
            {
                OnAnyButtonClicked?.Invoke();
                OnConfirm?.Invoke();
            };
            confirmButton.text = "CONFIRM";
            var cancelButton = Create<Button>("btn");
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

        private void CheckBoxCallback(ChangeEvent<bool> evt)
        {
            OnAnyButtonClicked?.Invoke();
        }

        private VisualElement Create(params string[] classNames)
        {
            return Create<VisualElement>(classNames);
        }

        private T Create<T>(params string[] classNames) where T : VisualElement, new()
        {
            var element = new T();

            foreach (string className in classNames)
            {
                element.AddToClassList(className);
            }
            
            return element;
        }
    }
}
