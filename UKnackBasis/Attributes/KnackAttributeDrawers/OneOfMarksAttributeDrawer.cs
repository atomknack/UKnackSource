#if UNITY_EDITOR

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UKnack.Attributes;
using UnityEditor;
using UnityEngine;

namespace UKnack.Attributes.KnackAttributeDrawers;


public abstract partial class OneOfMarksAttributeDrawer : PropertyDrawer
{
    private PropertyAttribute[] _AllPropertyAttributeAttributes;
    protected PropertyAttribute[] AllPropertyAttributeAttributes
    {
        get
        {
            if (_AllPropertyAttributeAttributes == null)
                _AllPropertyAttributeAttributes = (PropertyAttribute[])fieldInfo.GetCustomAttributes(typeof(PropertyAttribute), true);
            return _AllPropertyAttributeAttributes;
        }
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (CannotDrawBecauseIncorrectNumberOfAttributes(position))
            return;

        if (BeforeDrawProperty(position, property, label) == false)
            return;

        DrawProperty(position, property, label);

        AfterDrawProperty(position, property, label);
    }
    protected abstract bool BeforeDrawProperty(Rect position, SerializedProperty property, GUIContent label);
    protected abstract void DrawProperty(Rect position, SerializedProperty property, GUIContent label);
    protected abstract void AfterDrawProperty(Rect position, SerializedProperty property, GUIContent label);


    public bool CannotDrawBecauseIncorrectNumberOfAttributes(Rect position)
    {
        int count = MarkAttributeTypes.countOfMarks(AllPropertyAttributeAttributes);
        if (count == 0)
        {
            EditorGUI.DrawRect(new Rect(position.x - 2, position.y - 2, position.width + 4, position.height + 4), new Color(0.6f, 0.1f, 0.0f));
            EditorGUI.LabelField(position, new GUIContent("This drawer used incorrectly", "should only be used with PropertyDrawerOneOfAttribute or it child"));
            return true;
        }
        if (count > 1)
        {
            var attributesAsString = string.Join(" or ", MarkAttributeTypes.GetMarks(AllPropertyAttributeAttributes).Select(x => x.ToString()));
            EditorGUI.DrawRect(new Rect(position.x - 2, position.y - 2, position.width + 4, position.height + 4), new Color(0.6f, 0.1f, 0.0f));
            EditorGUI.LabelField(position, new GUIContent($"Can be only one \"OneOf\" but found many", $"should be only one attribute with drawer inherited from PropertyDrawerOneOfAttribute: \"{attributesAsString}\""));
            return true;
        }
        return false;
    }

    public static Type GetTypeFromFieldInfo(FieldInfo fi)
    {
        if (fi == null)
            return null;

        var type = fi.FieldType;

        if (type.IsArray)
            return type.GetElementType();

        if (typeof(IList).IsAssignableFrom(type))
            return type.GetGenericArguments()[0];

        return type;
    }

}
#endif