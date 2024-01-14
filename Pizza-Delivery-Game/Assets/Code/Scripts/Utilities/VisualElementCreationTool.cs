using UnityEngine.UIElements;

namespace Utilities
{
    public static class VisualElementCreationTool
    {
        public static VisualElement Create(params string[] classNames)
        {
            return Create<VisualElement>(classNames);
        }

        public static T Create<T>(params string[] classNames) where T : VisualElement, new()
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