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

        public static event Action<float> OnScaleChanged;
        public static event Action OnSpinClicked;
        
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
            VisualElement viewBox = Create("view-box", "bordered-box");
            container.Add(viewBox);


            VisualElement controlBox = Create("control-box", "bordered-box");
            container.Add(controlBox);

            var spinBtn = Create<Button>();
            spinBtn.text = "Spin";
            spinBtn.clicked += OnSpinClicked;
            
            controlBox.Add(spinBtn);

            var scaleSlider = Create<Slider>();
            scaleSlider.lowValue = 0.5f;
            scaleSlider.highValue = 2f;
            scaleSlider.value = 1f;
            scaleSlider.RegisterValueChangedCallback(v => OnScaleChanged?.Invoke(v.newValue));
            controlBox.Add(scaleSlider);
            
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
