using UnityEditor;
using UnityEngine;

namespace Common
{
    [CustomPropertyDrawer(typeof(ChildrenOnlyAttribute))]
    public class ChildrenOnly : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GameObject parent = (property.serializedObject.targetObject as Component).gameObject;

            EditorGUI.BeginProperty(position, label, property);

            bool isChild = false;
            GameObject referencedObject = null;

            // Check if the property is a GameObject
            if (property.objectReferenceValue is GameObject gameObjectValue)
            {
                referencedObject = gameObjectValue;
            }
            // Check if the property is a Component and get its GameObject
            else if (property.objectReferenceValue is Component componentValue)
            {
                referencedObject = componentValue.gameObject;
            }

            // Check if the referenced GameObject is a child of the parent
            if (referencedObject != null && referencedObject.transform.IsChildOf(parent.transform))
            {
                isChild = true;
            }

            // Draw the property field normally if the referenced object is a child
            if (isChild)
            {
                EditorGUI.PropertyField(position, property, label);
            }
            else
            {
                // Clear the field if the referenced object is not a child, then draw the field and HelpBox
                property.objectReferenceValue = null;

                Rect fieldPosition = position;
                fieldPosition.height = EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(fieldPosition, property, label);

                Rect helpBoxPosition = position;
                helpBoxPosition.y += fieldPosition.height + EditorGUIUtility.standardVerticalSpacing;
                helpBoxPosition.height = EditorGUIUtility.singleLineHeight;

                EditorGUI.HelpBox(helpBoxPosition, "This field only accepts children of the GameObject this script is attached to.", MessageType.Warning);
            }

            EditorGUI.EndProperty();
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var totalHeight = base.GetPropertyHeight(property, label);

            var componentValue = property.objectReferenceValue as Component;
            var gameObjectValue = property.objectReferenceValue as GameObject;
            var parent = (property.serializedObject.targetObject as Component)?.gameObject;

            var isChild = parent != null && (componentValue != null && componentValue.gameObject.transform.IsChildOf(parent.transform) ||
                                             gameObjectValue != null && gameObjectValue.transform.IsChildOf(parent.transform));

            if (!isChild)
            {
                // Add space for the HelpBox plus some vertical spacing
                totalHeight += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            }

            return totalHeight;
        }

    }
}
