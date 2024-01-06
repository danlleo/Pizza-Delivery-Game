using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.MainMenu
{
    [DisallowMultipleComponent]
    public class MainMenuScreen : MonoBehaviour
    {
        [SerializeField] private UIDocument _uiDocument;
        [SerializeField] private StyleSheet _styleSheet;
        
        private void Start()
        {
            StartCoroutine(Generate());
        }

        private void OnValidate()
        {
            if (Application.isPlaying) return;
            StartCoroutine(Generate());
        }

        private IEnumerator Generate()
        {
            yield return null;
            
            VisualElement root = _uiDocument.rootVisualElement;
            root.Clear();
            root.styleSheets.Add(_styleSheet);

            VisualElement container = Create("container");

            VisualElement header = Create("header");
            container.Add(header);

            var gameName = Create<Label>();
            gameName.text = "Night Pizza Delivery";
            header.Add(gameName);

            VisualElement buttonGroup = Create("btn-group");
            container.Add(buttonGroup);

            var newGameButton = Create<Button>("btn");
            newGameButton.text = "New Game";
            buttonGroup.Add(newGameButton);

            var optionButton = Create<Button>("btn");
            optionButton.text = "Options";
            buttonGroup.Add(optionButton);

            var creditsButton = Create<Button>("btn");
            creditsButton.text = "Credits";
            buttonGroup.Add(creditsButton);
            
            var quitGameButton = Create<Button>("btn");
            quitGameButton.text = "Quit";
            buttonGroup.Add(quitGameButton);
            
            root.Add(container);
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
