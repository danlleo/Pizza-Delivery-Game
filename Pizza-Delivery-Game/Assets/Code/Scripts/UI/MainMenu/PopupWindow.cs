using UnityEngine;
using UnityEngine.UIElements;

namespace UI.MainMenu
{
    [DisallowMultipleComponent]
    public class PopupWindow : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<PopupWindow> { }

        private const string STYLE_RESOURCE = "UIToolkit/PopupWindowStylesheet";
        private const string USS_POPUP = "popup-window";
        private const string USS_POPUP_CONTAINER = "popup-container";
        private const string USS_POPUP_HEADER = "popup-window-header";
        private const string USS_POPUP_HEADER_BTN_GROUP = "popup-window-header-btn-group";
        
        public PopupWindow()
        {
            styleSheets.Add(Resources.Load<StyleSheet>(STYLE_RESOURCE));
            AddToClassList(USS_POPUP_CONTAINER);
            
            VisualElement window = Create();
            window.AddToClassList(USS_POPUP);
            hierarchy.Add(window);

            VisualElement windowHeader = Create();
            windowHeader.AddToClassList(USS_POPUP_HEADER);
            window.Add(windowHeader);

            var message = Create<Label>();
            message.text = "Are you sure?";
            windowHeader.Add(message);
            
            VisualElement windowHeaderBtnGroup = Create();
            windowHeaderBtnGroup.AddToClassList(USS_POPUP_HEADER_BTN_GROUP);
            window.Add(windowHeaderBtnGroup);

            var confirmButton = Create<Button>();
            confirmButton.text = "Confirm";
            windowHeaderBtnGroup.Add(confirmButton);
            
            var cancelButton = Create<Button>();
            cancelButton.text = "Cancel";
            windowHeaderBtnGroup.Add(cancelButton);
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
