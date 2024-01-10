using System;
using Misc;
using UnityEngine.UIElements;

namespace UI.MainMenu
{
    public class CreditsWindow : VisualElement
    {
        public static Action OnAnyButtonClicked;
        
        public Action OnConfirm;
        
        public new class UxmlFactory : UxmlFactory<PopupWindow> { }

        public CreditsWindow()
        {
            styleSheets.Add(GameResources.Retrieve.CreditsWindowStylesheet);
            AddToClassList("container");
            
            VisualElement window = Create("window");
            hierarchy.Add(window);

            VisualElement header = Create("header");
            window.Add(header);
            var title = Create<Label>("title");
            title.text = "CREDITS";
            header.Add(title);

            VisualElement main = Create("main");
            window.Add(main);
            var assetsAndResourcesTitle = Create<Label>("semi");
            assetsAndResourcesTitle.text = "Assets and Resources";
            var assetsAndResourcesMessage = Create<Label>("message");
            assetsAndResourcesMessage.text =
                "I would like to extend my gratitude to the following platforms and their communities for providing a wealth of high-quality resources that greatly contributed to the development of this game: Pixabay, Freesound, Sketchfab, Unity Asset Store.";
            var appreciationTitle =Create<Label>("semi");
            appreciationTitle.text = "Message to You";
            var appreciationMessage = Create<Label>("message");
            appreciationMessage.text =
                "The generosity and talent of the creators on these platforms have been instrumental in bringing my game to life. Thank you!";
            
            main.Add(assetsAndResourcesTitle);
            main.Add(assetsAndResourcesMessage);
            main.Add(appreciationTitle);
            main.Add(appreciationMessage);

            var confirmButton = Create<Button>("confirm-button");
            confirmButton.text = "CONFIRM";
            confirmButton.name = "confirm-btn";
            confirmButton.clicked += () =>
            {
                OnAnyButtonClicked?.Invoke();
                OnConfirm?.Invoke();
            };
            main.Add(confirmButton);
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
