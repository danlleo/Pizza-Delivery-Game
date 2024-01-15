using System;
using Misc;
using UnityEngine.UIElements;
using static Utilities.VisualElementCreationTool;

namespace UI
{
    public class PopupWindow : VisualElement
    {
        public static Action OnAnyButtonSelected;
        public static Action OnAnyButtonClicked;
        
        public new class UxmlFactory : UxmlFactory<PopupWindow> { }

        private Label _messageTitle;
        private Label _messageMain;

        public Action OnConfirm;
        public Action OnDecline;
        
        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                if (_messageTitle != null)
                {
                    _messageTitle.text = _title;
                }
            }
        }
        
        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                if (_messageMain != null)
                {
                    _messageMain.text = _message;
                }
            }
        }
        
        private const string USS_POPUP = "popup-window";
        private const string USS_POPUP_CONTAINER = "popup-container";
        private const string USS_POPUP_HEADER = "popup-window-header";
        private const string USS_POPUP_HEADER_BTN_GROUP = "popup-window-header-btn-group";
        
        public PopupWindow()
        {
            styleSheets.Add(GameResources.Retrieve.PopupWindowStylesheet);
            AddToClassList(USS_POPUP_CONTAINER);
            
            VisualElement window = Create(USS_POPUP);
            hierarchy.Add(window);

            VisualElement windowHeader = Create(USS_POPUP_HEADER);
            window.Add(windowHeader);

            _messageTitle = Create<Label>("message-title");
            _messageTitle.text = Title;
            windowHeader.Add(_messageTitle);

            _messageMain = Create<Label>("message-main");
            _messageMain.text = Message;
            windowHeader.Add(_messageMain);
            
            VisualElement windowHeaderBtnGroup = Create(USS_POPUP_HEADER_BTN_GROUP);
            window.Add(windowHeaderBtnGroup);

            var confirmButton = Create<Button>();
            confirmButton.text = "YES";
            confirmButton.clicked += () =>
            {
                OnAnyButtonClicked?.Invoke();
                OnConfirm?.Invoke();
            };
            windowHeaderBtnGroup.Add(confirmButton);
            
            var cancelButton = Create<Button>();
            cancelButton.text = "NO";
            cancelButton.name = "cancel-button";
            cancelButton.clicked += () =>
            {
                OnAnyButtonClicked?.Invoke();
                OnDecline?.Invoke();
            };
            windowHeaderBtnGroup.Add(cancelButton);

            confirmButton.RegisterCallback<FocusEvent>(_ => OnAnyButtonSelected?.Invoke());
            cancelButton.RegisterCallback<FocusEvent>(_ => OnAnyButtonSelected?.Invoke());
        }
    }
}
