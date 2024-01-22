using System;
using Misc;
using UnityEngine.UIElements;
using static Utilities.VisualElementCreationTool;

namespace UI.MainMenu
{
    public class CreditsWindow : VisualElement
    {
        public const string CONFIRM_BUTTON_NAME = "confirm-btn";
        
        private const string USS_CONTAINER = "container";
        private const string USS_WINDOW = "window";
        private const string USS_WINDOW_HEADER = "header";
        private const string USS_WINDOW_HEADER_TITLE = "title";
        private const string USS_WINDOW_MAIN = "main";
        private const string USS_WINDOW_MAIN_SEMI = "semi";
        private const string USS_WINDOW_MAIN_MESSAGE = "message";
        
        public static Action OnAnyButtonClicked;
        
        public Action OnConfirm;
        
        public new class UxmlFactory : UxmlFactory<PopupWindow> { }

        public CreditsWindow()
        {
            styleSheets.Add(GameResources.Retrieve.CreditsWindowStylesheet);
            AddToClassList(USS_CONTAINER);
            
            VisualElement window = Create(USS_WINDOW);
            hierarchy.Add(window);

            VisualElement header = Create(USS_WINDOW_HEADER);
            window.Add(header);
            var title = Create<Label>(USS_WINDOW_HEADER_TITLE);
            title.text = "CREDITS";
            header.Add(title);

            VisualElement main = Create(USS_WINDOW_MAIN);
            window.Add(main);
            var assetsAndResourcesTitle = Create<Label>(USS_WINDOW_MAIN_SEMI);
            assetsAndResourcesTitle.text = "Assets and Resources";
            var assetsAndResourcesMessage = Create<Label>(USS_WINDOW_MAIN_MESSAGE);
            assetsAndResourcesMessage.text =
                "I would like to extend my gratitude to the following platforms and their communities for providing a wealth of high-quality resources that greatly contributed to the development of this game: Pixabay, Freesound, Sketchfab, Unity Asset Store.";
            var appreciationTitle =Create<Label>(USS_WINDOW_MAIN_SEMI);
            appreciationTitle.text = "Message to You";
            var appreciationMessage = Create<Label>(USS_WINDOW_MAIN_MESSAGE);
            appreciationMessage.text =
                "The generosity and talent of the creators on these platforms have been instrumental in bringing my game to life. Thank you!";
            
            main.Add(assetsAndResourcesTitle);
            main.Add(assetsAndResourcesMessage);
            main.Add(appreciationTitle);
            main.Add(appreciationMessage);

            var confirmButton = Create<Button>("confirm-button");
            confirmButton.text = "CONFIRM";
            confirmButton.name = CONFIRM_BUTTON_NAME;
            confirmButton.clicked += () =>
            {
                OnAnyButtonClicked?.Invoke();
                OnConfirm?.Invoke();
            };
            main.Add(confirmButton);
        }
    }
}
