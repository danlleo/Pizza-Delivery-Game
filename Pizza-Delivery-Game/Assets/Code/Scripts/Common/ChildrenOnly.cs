using UnityEditor;
using UnityEngine;

namespace Common
{
    [CustomPropertyDrawer(typeof(ChildrenOnlyAttribute))]
    public class ChildrenOnly : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GameObject parent = (property.serializedObject.targetObject as Component)?.gameObject;

            EditorGUI.BeginProperty(position, label, property);

            var isChild = false;
            var componentValue = property.objectReferenceValue as Component;

            if (parent != null && componentValue != null && componentValue.gameObject.transform.IsChildOf(parent.transform))
            {
                isChild = true;
            }

            var gameObjectValue = property.objectReferenceValue as GameObject;

            if (parent != null && gameObjectValue != null && gameObjectValue.transform.IsChildOf(parent.transform))
            {
                isChild = true;
            }

            Rect propertyPosition = position;
            propertyPosition.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(propertyPosition, property, label);

            if (!isChild)
            {
                property.objectReferenceValue = null;

                Rect helpBoxPosition = position;
                helpBoxPosition.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
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
