#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using UKnack.Attributes;
using UnityEditor;
using UnityEngine;

namespace UKnack.Attributes.KnackAttributeDrawers;

[CustomPropertyDrawer(typeof(ProvidedComponentAttribute))]
public class ProvidedComponentAttributeDrawer : AbstractReferenceWithPickerDrawer
{
    private Component _asComponent;
    protected override bool BeforeDrawProperty(Rect position, SerializedProperty property, GUIContent label)
    {
        bool baseResult = base.BeforeDrawProperty(position, property, label);
        if( baseResult == false)
            return false;

        _asComponent = property.serializedObject.targetObject as Component;
        if (_asComponent == null)
        {
            string tooltip = $"{attribute.GetType()} can only be applyed to reference property of Monobehaviour";
            EditorGUI.LabelField(position, new GUIContent($"\"{label.text}\" with Attribute cannot be drawed ...", tooltip));
            return false;
        }
        return true;
    }
    protected override void DrawProperty(Rect position, SerializedProperty property, GUIContent label)
    {

        Type providedType = GetTypeFromFieldInfo(fieldInfo); //((ProvidedByGameobjectAttribute)attribute).providedType;

        FillSubtypes();

        DrawCustomPickerButton(_subtypes, position);
        var subFieldPosition = new Rect(position.x + 22, position.y, position.width - 22, position.height);

        Component currentValue = (Component)property.objectReferenceValue;

        Component provided;
        string exceptionText = string.Empty;
        try
        {
            provided = ProvidedComponentAttribute.Provide(providedType, _asComponent.gameObject, currentValue);
        }
        catch (Exception ex) 
        {
            provided = null;
            exceptionText = ", \n\nException: " +ex.ToString();
        }

        if (Application.isPlaying)
        {
            DrawCustomButtonBackground(_subtypes, position);
            EditorGUI.DrawRect(position, new Color(0.1f, 0.1f, 0.7f, 0.4f));
            string baseTooltip = string.IsNullOrWhiteSpace(label.text) ? string.Empty : $"; base tooltip: {label.text}";
            EditorGUI.PropertyField(subFieldPosition, 
                property, 
                new GUIContent(label.text, label.image, $"Current value of field with {attribute}" + baseTooltip), true);
            return;
        }

        if (provided != null)
        {
            if (currentValue != null)
            {
                /* if (Application.isPlaying)
                {
                    EditorGUI.PropertyField(position, property, new GUIContent(label.text, label.image, "current value of field"), true);
                    return;
                } */
                Color yellowColor = new Color(1f, 0.8f, 0, 0.2f);
                EditorGUI.DrawRect(new Rect(position.x - 2, position.y - 2, position.width + 4, position.height + 4), yellowColor);
                DrawCustomPickerButton(_subtypes, position);
                EditorGUI.PropertyField(subFieldPosition, 
                    property, 
                    new GUIContent(label.text, label.image, "This field can be set to Null, so it can be be provided by gameobject, because gameobject has one"), true);
                return;
            }

            string tooltip = "This property has corresponding providable monobehaviour, you should use retrieve it by GetComponent<> in your init method(Awake, Start, OnEnable, etc ...)";
            DrawCustomButtonBackground(_subtypes, position);
            EditorGUI.DrawRect(position, new Color(0.1f, 0.1f, 0.7f, 0.4f));
            DrawCustomPickerButton(_subtypes, position);

            string by = provided.gameObject == _asComponent.gameObject ? "this gameObject" : $"\"{provided.gameObject.name}\"";
            EditorGUI.LabelField(subFieldPosition, new GUIContent($"\"{label.text}\" provided by {by}", tooltip));
            return;
        }

        //base.DrawProperty(position, property, label);
        //DrawMarked(position, property, label, new Color(1, 0, 0), $"This field should be set to value OR gameobject should have monobehaviour derived from {providedType.Name}");
        var markColor = new Color(1, 0, 0, 0.5f);
        var nullValueTooltip = $"{typeof(ProvidedComponentAttribute).Name}:\n{providedType.Name} not found in gameobject and its parents, you need to have providable component in parent hierarchy or manually set this field{exceptionText}";


        if (property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue == null)
        {
            DrawCustomButtonBackground(_subtypes, position);
            EditorGUI.DrawRect(new Rect(position.x - 2, position.y - 2, position.width + 4, position.height + 4), markColor);
            EditorGUI.PropertyField(position, property, new GUIContent(label.text, label.image, nullValueTooltip), true);
            return;
        }

        EditorGUI.DrawRect(position, Color.black);
        EditorGUI.PropertyField(position, 
            property, 
            new GUIContent(label.text, label.image, $"{typeof(ProvidedComponentAttribute)} unknown state"), true);

    }

    void FillSubtypes()
    {
        if (_subtypes != null)
            return;
        Type baseFieldType = GetTypeFromFieldInfo(fieldInfo);
        _subtypes = new List<Type>();
        AddUniquePickerType(_subtypes, baseFieldType);
    }
    /*
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (CannotDrawBecauseIncorrectNumberOfAttributes(position))
            return;
        BeforeDrawProperty(position, property, label);

        DrawAttributeGUI(position, property, label);

        AfterDrawProperty(position, property, label);
    }
    */
}
#endif