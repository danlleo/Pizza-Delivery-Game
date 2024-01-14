using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using static Utilities.VisualElementCreationTool;

namespace UI.GamePause
{
    [DisallowMultipleComponent]
    public sealed class GamePauseScreen : MonoBehaviour
    {
        public static Action OnAnyResumeButtonClicked;
        public static Action OnAnyButtonSelected;
        
        [SerializeField] private UIDocument _uiDocument;
        [SerializeField] private StyleSheet _styleSheet;

        // private void Start()
        // {
        //     StartCoroutine(GenerateUIRoutine());
        // }
        //
        // private void OnValidate()
        // {
        //     if (Application.isPlaying) return;
        //     StartCoroutine(GenerateUIRoutine());
        // }

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
            => _uiDocument.rootVisualElement.Clear();
        
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
            resumeButton.clicked += OnAnyResumeButtonClicked;
            mainContentButtonsContainer.Add(resumeButton);
            
            var optionsButton = Create<Button>();
            optionsButton.text = "OPTIONS";
            mainContentButtonsContainer.Add(optionsButton);
            
            var mainMenuButton = Create<Button>();
            mainMenuButton.text = "MAIN MENU";
            mainContentButtonsContainer.Add(mainMenuButton);
            
            var desktopButton = Create<Button>();
            desktopButton.text = "DESKTOP";
            mainContentButtonsContainer.Add(desktopButton);
            
            root.Add(container);
            root.Q<Button>("resume-button").Focus();
            
            resumeButton.RegisterCallback<FocusEvent>(_ => OnAnyButtonSelected?.Invoke());
            optionsButton.RegisterCallback<FocusEvent>(_ => OnAnyButtonSelected?.Invoke());
            mainMenuButton.RegisterCallback<FocusEvent>(_ => OnAnyButtonSelected?.Invoke());
            desktopButton.RegisterCallback<FocusEvent>(_ => OnAnyButtonSelected?.Invoke());
        }
    }
}
