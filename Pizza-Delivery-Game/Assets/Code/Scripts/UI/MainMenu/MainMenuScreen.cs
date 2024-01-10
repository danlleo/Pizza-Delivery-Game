using System;
using System.Collections;
using System.Collections.Generic;
using Enums.Scenes;
using Misc;
using Misc.Loader;
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

        public static Action OnAnyButtonSelected;
        
        private void OnEnable()
        {
            MainMenuController.OnAnyTriedNewGame += MainMenuController_OnAnyNewGameButtonClicked;
            MainMenuController.OnAnyOptionsOpen += MainMenuController_OnAnyOptionsButtonClicked;
            MainMenuController.OnAnyCreditsOpen += MainMenuController_OnAnyCreditsButtonClicked;
            MainMenuController.OnAnyTryQuit += MainMenuController_OnAnyQuitButtonClicked;
        }

        private void OnDisable()
        {
            MainMenuController.OnAnyTriedNewGame -= MainMenuController_OnAnyNewGameButtonClicked;
            MainMenuController.OnAnyOptionsOpen -= MainMenuController_OnAnyOptionsButtonClicked;
            MainMenuController.OnAnyCreditsOpen -= MainMenuController_OnAnyCreditsButtonClicked;
            MainMenuController.OnAnyTryQuit -= MainMenuController_OnAnyQuitButtonClicked;
        }
        
        private void Start()
        {
            StartCoroutine(Generate());
        }
        
        private void OnValidate()
        {
            if (Application.isPlaying) return;
            StartCoroutine(Generate());
        }

        #region Events

        private void MainMenuController_OnAnyQuitButtonClicked()
        {
            CreatePopupWindow("ARE YOU SURE YOU WANT TO QUIT?", "This will move you to the desktop.", Application.Quit,
                () =>
                {
                    _uiDocument.rootVisualElement.Q<PopupWindow>().RemoveFromHierarchy();
                    EnableMainMenuButtonsFocus();
                    FocusOnFirstButtonInMainMenuScreenGroup();
                });
        }

        private void MainMenuController_OnAnyCreditsButtonClicked()
        {
            throw new NotImplementedException();
        }

        private void MainMenuController_OnAnyOptionsButtonClicked()
        {
            throw new NotImplementedException();
        }

        private void MainMenuController_OnAnyNewGameButtonClicked()
        {
            CreatePopupWindow("ARE YOU SURE YOU WANT TO START A NEW GAME?", "You don't know how this journey will end.",
                () =>
                {
                    DisableMainMenuButtonsFocus();
                    ServiceLocator.ServiceLocator.GetCrossfadeService()
                        .FadeIn(InputAllowance.DisableInput, () => Loader.Load(Scene.BedroomScene));
                },
                () =>
                {
                    _uiDocument.rootVisualElement.Q<PopupWindow>().RemoveFromHierarchy();
                    EnableMainMenuButtonsFocus();
                    FocusOnFirstButtonInMainMenuScreenGroup();
                });
        }

        #endregion
        
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
            
            root.Add(container);
            
            FocusOnFirstButtonInMainMenuScreenGroup();
            
            newGameButton.RegisterCallback<FocusEvent>(_ => OnAnyButtonSelected?.Invoke());
            optionButton.RegisterCallback<FocusEvent>(_ => OnAnyButtonSelected?.Invoke());
            creditsButton.RegisterCallback<FocusEvent>(_ => OnAnyButtonSelected?.Invoke());
            quitGameButton.RegisterCallback<FocusEvent>(_ => OnAnyButtonSelected?.Invoke());
        }

        private void FocusOnCancelButtonInModalWindow()
        {
            _uiDocument.rootVisualElement.Q<Button>("cancel-button").Focus();
        }
        
        private void FocusOnFirstButtonInMainMenuScreenGroup()
        {
            _uiDocument.rootVisualElement.
                Q<Button>("new-game-button").Focus();
        }

        private void DisableMainMenuButtonsFocus()
        {
            // Disable focus for all main menu buttons
            List<Button> mainMenuButtons = _uiDocument.rootVisualElement.Query<Button>().ToList();
            
            foreach (Button button in mainMenuButtons)
            {
                button.focusable = false;
            }
        }

        private void EnableMainMenuButtonsFocus()
        {
            // Enable focus for all main menu buttons
            List<Button> mainMenuButtons = _uiDocument.rootVisualElement.Query<Button>().ToList();
            
            foreach (Button button in mainMenuButtons)
            {
                button.focusable = true;
            }
        }

        private void CreatePopupWindow(string titleMessage, string bodyMessage, Action onConfirm, Action onDecline)
        {
            DisableMainMenuButtonsFocus();
            
            var popupWindow = Create<PopupWindow>();
            popupWindow.Title = titleMessage;
            popupWindow.Message = bodyMessage;
            popupWindow.OnConfirm = onConfirm;
            popupWindow.OnDecline = onDecline;
            _uiDocument.rootVisualElement.Add(popupWindow);

            // Add focus event to disable main menu buttons
            popupWindow.RegisterCallback<FocusEvent>(_ => DisableMainMenuButtonsFocus());

            // Add blur event to enable main menu buttons
            popupWindow.RegisterCallback<BlurEvent>(_ => EnableMainMenuButtonsFocus());
            
            FocusOnCancelButtonInModalWindow();
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
