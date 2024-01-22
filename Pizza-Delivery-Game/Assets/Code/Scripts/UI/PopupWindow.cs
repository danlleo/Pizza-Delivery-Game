using System;
using System.Collections.Generic;
using Misc;
using UnityEngine.UIElements;
using static Utilities.VisualElementCreationTool;

namespace UI
{
    public class PopupWindow : VisualElement
    {
        public const string DECLINE_BUTTON_NAME = "cancel-button";
        
        private const string USS_POPUP = "popup-window";
        private const string USS_POPUP_CONTAINER = "popup-container";
        private const string USS_POPUP_HEADER = "popup-window-header";
        private const string USS_POPUP_HEADER_BTN_GROUP = "popup-window-header-btn-group";
        private const string USS_POPUP_HEADER_MESSAGE_TITLE = "message-title";
        private const string USS_POPUP_HEADER_MESSAGE_MAIN = "message-main";
        
        public static Action OnAnyButtonSelected;
        public static Action OnAnyButtonClicked;
        
        public new class UxmlFactory : UxmlFactory<PopupWindow> { }

        private Label _messageTitle;
        private Label _messageMain;

        public Action OnConfirm;
        public Action OnDecline;

        private Button _confirmButton;
        private Button _declineButton;
        
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
        
        public PopupWindow()
        {
            styleSheets.Add(GameResources.Retrieve.PopupWindowStylesheet);
            AddToClassList(USS_POPUP_CONTAINER);
            
            VisualElement window = Create(USS_POPUP);
            hierarchy.Add(window);

            VisualElement windowHeader = Create(USS_POPUP_HEADER);
            window.Add(windowHeader);

            _messageTitle = Create<Label>(USS_POPUP_HEADER_MESSAGE_TITLE);
            _messageTitle.text = Title;
            windowHeader.Add(_messageTitle);

            _messageMain = Create<Label>(USS_POPUP_HEADER_MESSAGE_MAIN);
            _messageMain.text = Message;
            windowHeader.Add(_messageMain);
            
            VisualElement windowHeaderBtnGroup = Create(USS_POPUP_HEADER_BTN_GROUP);
            window.Add(windowHeaderBtnGroup);

            _confirmButton = Create<Button>();
            _confirmButton.text = "YES";
            _confirmButton.clicked += () =>
            {
                OnAnyButtonClicked?.Invoke();
                OnConfirm?.Invoke();
            };
            windowHeaderBtnGroup.Add(_confirmButton);
            
            _declineButton = Create<Button>();
            _declineButton.text = "NO";
            _declineButton.name = DECLINE_BUTTON_NAME;
            _declineButton.clicked += () =>
            {
                OnAnyButtonClicked?.Invoke();
                OnDecline?.Invoke();
            };
            windowHeaderBtnGroup.Add(_declineButton);
            
            _confirmButton.RegisterCallback<FocusEvent>(_ => OnAnyButtonSelected?.Invoke());
            _declineButton.RegisterCallback<FocusEvent>(_ => OnAnyButtonSelected?.Invoke());
        }

        public void RegisterButtonPressEvent(VisualElement root)
        {
            var buttonActions = new Dictionary<Button, Action>()
            {
                {
                    _confirmButton, () =>
                    {
                        OnConfirm?.Invoke();
                        OnAnyButtonClicked?.Invoke();
                    }
                },
                {
                    _declineButton, () =>
                    {
                        OnDecline?.Invoke();
                        OnAnyButtonClicked?.Invoke();
                    }
                }
            };

            root.RegisterCallback<KeyDownEvent>(evt => OnKeyDown(evt, buttonActions));
        }
    }
}
