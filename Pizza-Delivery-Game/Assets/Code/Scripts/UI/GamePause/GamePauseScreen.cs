using System;
using System.Collections;
using System.Collections.Generic;
using Enums.Scenes;
using Misc;
using Misc.Loader;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;
using static Utilities.VisualElementCreationTool;

namespace UI.GamePause
{
    [DisallowMultipleComponent]
    public sealed class GamePauseScreen : MonoBehaviour
    {
        public static Action OnAnyButtonSelected;
        public static Action OnAnyButtonPressed;
        
        [SerializeField] private UIDocument _uiDocument;
        [SerializeField] private StyleSheet _styleSheet;
        
        private Dictionary<Button, Action> _buttonActions;
        
        private EventCallback<KeyDownEvent> _keyDownCallback;

        private Crossfade.Crossfade _crossfade;
        
        [Inject]
        private void Construct(Crossfade.Crossfade crossfade)
        {
            _crossfade = crossfade;
        }
        
        private void OnEnable()
        {
            TimeControl.OnAnyGamePaused.Event += OnAnyGamePaused;
            TimeControl.OnAnyGameUnpaused.Event += OnAnyGameUnpaused;
        }

        private void OnDisable()
        {
            TimeControl.OnAnyGamePaused.Event -= OnAnyGamePaused;
            TimeControl.OnAnyGameUnpaused.Event -= OnAnyGameUnpaused;
        }

        private void OnAnyGamePaused(object sender, EventArgs e)
        {
            StartCoroutine(GenerateUIRoutine());
        }

        private void OnAnyGameUnpaused(object sender, EventArgs e)
        {
            DeleteUI();
        }

        private void DeleteUI()
        {
            _uiDocument.rootVisualElement.UnregisterCallback(_keyDownCallback);
            _uiDocument.rootVisualElement.Clear();
        }
        
        private IEnumerator GenerateUIRoutine()
        {
            yield return null;

            VisualElement root = _uiDocument.rootVisualElement;
            root.Clear();
            root.styleSheets.Add(_styleSheet);
            
            VisualElement container = Create("container");

            VisualElement main = Create("main");
            container.Add(main);

            VisualElement mainContent = Create("main-content");
            main.Add(mainContent);

            var mainContentTitle = Create<Label>("main-content-title");
            mainContentTitle.text = "PAUSE";
            mainContent.Add(mainContentTitle);

            VisualElement mainContentButtonsContainer = Create("main-content-buttons-container");
            mainContent.Add(mainContentButtonsContainer);

            var resumeButton = Create<Button>();
            resumeButton.text = "RESUME";
            resumeButton.name = "resume-button";
            resumeButton.clicked += HandleResumeButtonClick;
            mainContentButtonsContainer.Add(resumeButton);
            
            var optionsButton = Create<Button>();
            optionsButton.text = "OPTIONS";
            optionsButton.clicked += HandleOptionsButtonClick;
            mainContentButtonsContainer.Add(optionsButton);
            
            var mainMenuButton = Create<Button>();
            mainMenuButton.text = "MAIN MENU";
            mainMenuButton.clicked += HandleMainMenuButtonClick;
            mainContentButtonsContainer.Add(mainMenuButton);
            
            var desktopButton = Create<Button>();
            desktopButton.text = "DESKTOP";
            desktopButton.clicked += HandleDesktopButtonClick;
            mainContentButtonsContainer.Add(desktopButton);
            
            _buttonActions = new Dictionary<Button, Action>
            {
                { resumeButton, HandleResumeButtonClick },
                { optionsButton, HandleOptionsButtonClick },
                { mainMenuButton, HandleMainMenuButtonClick },
                { desktopButton, HandleDesktopButtonClick }
            };
            
            _keyDownCallback = evt => OnKeyDown(evt, _buttonActions);
            
            root.Add(container);
            root.Q<Button>("resume-button").Focus();
            root.RegisterCallback(_keyDownCallback);
            
            resumeButton.RegisterCallback<FocusEvent>(_ => OnAnyButtonSelected?.Invoke());
            optionsButton.RegisterCallback<FocusEvent>(_ => OnAnyButtonSelected?.Invoke());
            mainMenuButton.RegisterCallback<FocusEvent>(_ => OnAnyButtonSelected?.Invoke());
            desktopButton.RegisterCallback<FocusEvent>(_ => OnAnyButtonSelected?.Invoke());
        }
        
        private void HandleResumeButtonClick()
        {
            OnAnyButtonPressed?.Invoke();
            TimeControl.OnAnyGameUnpaused.Call(this);
        }
        
        private void HandleOptionsButtonClick()
        {
            OnAnyButtonPressed?.Invoke();
            DeleteUI();
            CreateSettingsWindow(() =>
            {
                DeleteUI();
                EnableMenuButtonsFocus();
                StartCoroutine(GenerateUIRoutine());
            }, () =>
            {
                DeleteUI();
                EnableMenuButtonsFocus();
                StartCoroutine(GenerateUIRoutine());
            });
        }
        
        private void HandleMainMenuButtonClick()
        {
            OnAnyButtonPressed?.Invoke();
            DeleteUI();
            CreatePopupWindow("ARE YOU SURE YOU WANT TO MOVE TO THE MAIN MENU?",
                "Moving to the main menu will loose your current progress.",
                () =>
                {
                    InputAllowance.DisableInput();
                    DeleteUI();
                    _crossfade
                        .FadeIn(InputAllowance.DisableInput, () => Loader.Load(Scene.MainMenuScene), 1f);
                },
                () =>
                {
                    DeleteUI();
                    StartCoroutine(GenerateUIRoutine());
                });
        }

        private void HandleDesktopButtonClick()
        {
            OnAnyButtonPressed?.Invoke();
            DeleteUI();
            CreatePopupWindow("ARE YOU SURE YOU WANT TO QUIT?",
                "Moving to the desktop will loose your current progress.",
                () =>
                {
                    DeleteUI();
                    Application.Quit();
                },
                () =>
                {
                    DeleteUI();
                    StartCoroutine(GenerateUIRoutine());
                });
        }
        
        private void CreatePopupWindow(string titleMessage, string bodyMessage, Action onConfirm, Action onDecline)
        {
            DisableMenuButtonsFocus();
            
            var popupWindow = Create<PopupWindow>();
            popupWindow.Title = titleMessage;
            popupWindow.Message = bodyMessage;
            popupWindow.OnConfirm = onConfirm;
            popupWindow.OnDecline = onDecline;
            
            _uiDocument.rootVisualElement.Add(popupWindow);
            _uiDocument.rootVisualElement.Q<Button>("cancel-button").Focus();
            
            // Add focus event to disable main menu buttons
            popupWindow.RegisterCallback<FocusEvent>(_ => DisableMenuButtonsFocus());
        
            // Add blur event to enable main menu buttons
            popupWindow.RegisterCallback<BlurEvent>(_ => EnableMenuButtonsFocus());
            
            popupWindow.RegisterButtonPressEvent(_uiDocument.rootVisualElement);
        }
        
        private void CreateSettingsWindow(Action onConfirm, Action onCancel)
        {
            DisableMenuButtonsFocus();
         
            var settingsWindow = Create<SettingsWindow>();
            settingsWindow.OnConfirm = onConfirm;
            settingsWindow.OnCancel = onCancel;
            
            _uiDocument.rootVisualElement.Add(settingsWindow);

            settingsWindow.RegisterCallback<FocusEvent>(_ => DisableMenuButtonsFocus());
            settingsWindow.RegisterCallback<FocusEvent>(_ => EnableMenuButtonsFocus());
            
            _uiDocument.rootVisualElement.Q<Slider>("mouse-sensitivity-slider").Focus();

            RegisterFocusCallbacks(settingsWindow);
        }
        
        private void DisableMenuButtonsFocus()
        {
            // Disable focus for all main menu buttons
            List<Button> mainMenuButtons = _uiDocument.rootVisualElement.Query<Button>().ToList();
            
            foreach (Button button in mainMenuButtons)
            {
                button.focusable = false;
            }
        }

        private void EnableMenuButtonsFocus()
        {
            // Enable focus for all main menu buttons
            List<Button> mainMenuButtons = _uiDocument.rootVisualElement.Query<Button>().ToList();
            
            foreach (Button button in mainMenuButtons)
            {
                button.focusable = true;
            }
        }
        
        private void RegisterFocusCallbacks(VisualElement container)
        {
            List<Button> buttons = container.Query<Button>().ToList();
            
            foreach (Button button in buttons)
            {
                button.RegisterCallback<FocusEvent>(_ => OnAnyButtonSelected?.Invoke());
            }

            List<Toggle> toggles = container.Query<Toggle>().ToList();
            
            foreach (Toggle toggle in toggles)
            {
                toggle.RegisterCallback<FocusEvent>(_ => OnAnyButtonSelected?.Invoke());
            }

            List<Slider> sliders = container.Query<Slider>().ToList();
            
            foreach (Slider slider in sliders)
            {
                slider.RegisterCallback<FocusEvent>(_ => OnAnyButtonSelected?.Invoke());
            }
        }
    }
}
