using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ConditionalShowAttribute))]
public class ConditionalShowPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ConditionalShowAttribute condAttribute = (ConditionalShowAttribute)attribute;
        SerializedProperty conditionalVariable = property.serializedObject.FindProperty(condAttribute.conditionalVariableName);

        if (conditionalVariable != null && conditionalVariable.propertyType == SerializedPropertyType.Boolean)
        {
            bool showProperty = conditionalVariable.boolValue;
            if (showProperty)
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        ConditionalShowAttribute condAttribute = (ConditionalShowAttribute)attribute;
        SerializedProperty conditionalVariable = property.serializedObject.FindProperty(condAttribute.conditionalVariableName);

        if (conditionalVariable != null && conditionalVariable.propertyType == SerializedPropertyType.Boolean)
        {
            bool showProperty = conditionalVariable.boolValue;
            if (showProperty)
            {
                return EditorGUI.GetPropertyHeight(property, label, true);
            }
        }

        return 0f;
    }
}