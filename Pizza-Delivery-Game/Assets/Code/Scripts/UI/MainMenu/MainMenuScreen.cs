using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.MainMenu
{
    [DisallowMultipleComponent]
    public sealed class MainMenuScreen : MonoBehaviour
    {
        [SerializeField] private UIDocument _uiDocument;
        [SerializeField] private StyleSheet _styleSheet;

        public static Action OnAnyNewGameButtonClicked;
        public static Action OnAnyOptionsButtonClicked;
        public static Action OnAnyCreditsButtonClicked;
        public static Action OnAnyQuitButtonClicked;

        private void Start()
        {
            StartCoroutine(Generate());
        }
        
        private void OnValidate()
        {
            if (Application.isPlaying) return;
            StartCoroutine(Generate());
        }
        
        private void FocusFirstElement()
        {
            _uiDocument.rootVisualElement.
                Q<Button>("new-game-button").Focus();
        }

        #region UI Creation

        private IEnumerator Generate()
        {
            yield return null;

            VisualElement root = _uiDocument.rootVisualElement;
            root.Clear();
            root.styleSheets.Add(_styleSheet);

            VisualElement container = Create("container");

            VisualElement gameContainer = Create("game-container");
            container.Add(gameContainer);

            var gameName = Create<Label>();
            gameName.text = "Night Pizza Delivery";
            gameContainer.Add(gameName);
            
            VisualElement buttonGroup = Create("btn-group");
            gameContainer.Add(buttonGroup);

            var newGameButton = Create<Button>("btn");
            newGameButton.clicked += OnAnyNewGameButtonClicked;
            newGameButton.text = "NEW GAME";
            newGameButton.name = "new-game-button";
            buttonGroup.Add(newGameButton);
            
            var optionButton = Create<Button>("btn");
            optionButton.clicked += OnAnyOptionsButtonClicked;
            optionButton.text = "SETTINGS";
            buttonGroup.Add(optionButton);

            var creditsButton = Create<Button>("btn");
            creditsButton.clicked += OnAnyCreditsButtonClicked;
            creditsButton.text = "CREDITS";
            buttonGroup.Add(creditsButton);
            
            var quitGameButton = Create<Button>("btn");
            quitGameButton.clicked += OnAnyQuitButtonClicked;
            quitGameButton.text = "QUIT";
            buttonGroup.Add(quitGameButton);
            
            // var popupWindow = Create<PopupWindow>();
            // root.Add(popupWindow);
            
            root.Add(container);
            
            FocusFirstElement();
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

        #endregion
    }
}
