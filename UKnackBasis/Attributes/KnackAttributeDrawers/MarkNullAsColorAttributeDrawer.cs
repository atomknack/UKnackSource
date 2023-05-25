#if UNITY_EDITOR
/// P: in future if needed - version with null findings located at
/// https://github.com/redbluegames/unity-notnullattribute/blob/master/Assets/RedBlueGames/NotNullAttribute/NotNullAttribute.cs

using System;
using System.Collections.Generic;
using UKnack.Attributes;
using UKnack.Common;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UKnack.Attributes.KnackAttributeDrawers;

//[CustomPropertyDrawer(typeof(MarkNullAsRedAttribute))]
[CustomPropertyDrawer(typeof(MarkNullAsColorAttribute))]
public class MarkNullAsColorAttributeDrawer : AbstractReferenceWithPickerDrawer
{
    private MarkNullAsColorAttribute markColor => (MarkNullAsColorAttribute)attribute;

    //protected override bool BeforeDrawProperty(Rect position, SerializedProperty property, GUIContent label) 
    //    => true;

    protected override void DrawProperty(Rect position, SerializedProperty property, GUIContent label)
    {
        string nullValueTooltip = markColor.NullValueTooltip;

        FillSubtypes();

        Color pickerButtonBackground = ColorHelpers.WithNewAlpha(ColorHelpers.MultipliedRGB(ColorHelpers.InvertedRGB(markColor.MarkColor),0.4f),1);
        if (property.objectReferenceValue == null)
        {
            EditorGUI.DrawRect(new Rect(position.x - 2, position.y - 2, position.width + 4, position.height + 4), markColor.MarkColor);
            DrawCustomButtonBackground(_subtypes, position, ColorHelpers.WithNewAlpha(pickerButtonBackground,0.5f));
            DrawCustomPickerButton(_subtypes, position, IsPlayingAndDisabled);
            EditorGUI.PropertyField(position,
                property,
                new GUIContent(label.text,
                    label.image,
                    String.IsNullOrWhiteSpace(label.tooltip) ?
                        nullValueTooltip :
                        $"{nullValueTooltip}, additional tooltip:{label.tooltip}"), true);
            return;
        }
        DrawCustomButtonBackground(_subtypes, position, new Color(0, 0, 0, 0.7f));
        EditorGUI.DrawRect(KnackPickerButtonPosition(position), new Color(1,1,1,0.1f));
        DrawCustomPickerButton(_subtypes, position, IsPlayingAndDisabled);
        EditorGUI.PropertyField(position, property, label, true);

        //DrawMarked(position, property, label, markColor.MarkColor, markColor.NullValueTooltip);

    }

    //protected override void AfterDrawProperty(Rect position, SerializedProperty property, GUIContent label)
    //{
    //}


    public virtual void FillSubtypes()
    {
        FillSubtypesDefault(ref _subtypes, this);
    }

    internal static void FillSubtypesDefault(ref List<Type> subtypes, PropertyDrawer drawer)
    {
        if (subtypes != null)
            return;
        Type baseFieldType = GetTypeFromFieldInfo(drawer.fieldInfo);
        subtypes = new List<Type>();
        AddUniquePickerType(subtypes, baseFieldType);
    }

    /*
     
    public static void DrawMarked(Rect position, SerializedProperty property, GUIContent label, Color markColor, string nullValueTooltip = "This field should be set to value")
    {

        if (property.propertyType != SerializedPropertyType.ObjectReference)
        {
            EditorGUI.DrawRect(new Rect(position.x - 2, position.y - 2, position.width + 4, position.height + 4), new Color(0.25f, 0.05f, 0.05f));
            string tooltip = $"MarkNullAsColor can only be applied to reference type. remove MarkNullAsRed";
            EditorGUI.LabelField(position, new GUIContent($"\"{label}\" with MarkNullAsRed cannot be drawed ...", tooltip));
            Debug.Log(property.propertyType);
            return;
        }

        if (property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue == null)
        {
            EditorGUI.DrawRect(new Rect(position.x - 2, position.y - 2, position.width + 4, position.height + 4), markColor);
            EditorGUI.PropertyField(position, 
                property, 
                new GUIContent(label.text, 
                    label.image,
                    String.IsNullOrWhiteSpace(label.tooltip) ? 
                        nullValueTooltip : 
                        $"{nullValueTooltip}, additional tooltip:{label.tooltip}"), true);
            return;
        }

        EditorGUI.PropertyField(position, property, label, true);
    }
      
     
public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
{
    if(CannotDrawBecauseIncorrectNumberOfAttributes(position))
        return;
    BeforeDrawProperty(position, property, label);
    DrawMarked(position, property, label, markColor.MarkColor, markColor.NullValueTooltip);
    AfterDrawProperty(position, property, label);
}
*/
}
#endif