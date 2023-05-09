#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace UKnack.Preconcrete.Values.PropertyDrawers;

[CustomPropertyDrawer(typeof(ValueDebugInfo<>), true)]
public class ValueDebugInfoDrawer : PropertyDrawer
{
    private SerializedProperty _property;
    private SerializedProperty _debugValue => _property.FindPropertyRelative("_value");
    private SerializedProperty _timeStamp => _property.FindPropertyRelative("_timeStamp");

    private Label _label;

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        _property = property;
        var propertyRoot = new VisualElement();
        var foldout = new Foldout();
        foldout.text = property.displayName;
        foldout.SetValueWithoutNotify(true);

        //Label typesOfSerialized = new Label($"{(_debugValue == null ? "isNull": _debugValue.type)} ; {(_timeStamp == null ? "isNull" : _timeStamp.type)}");
        PropertyField debugValue = new PropertyField(_debugValue);
        debugValue.SetEnabled(false);
        //long longSpamp = _timeStamp.type _timeStamp.longValue
        _label = new Label();
        UpdateTimestampLabel(_timeStamp);
        _label.TrackPropertyValue(_timeStamp, UpdateTimestampLabel);

        //foldout.Add( typesOfSerialized );
        VisualElement container = new VisualElement();
        container.Add(debugValue);
        container.Add(_label);
        container.style.borderTopWidth = 1;
        container.style.borderBottomWidth = 1;
        Color borderColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        container.style.borderTopColor = borderColor;
        container.style.borderBottomColor = borderColor;

        foldout.Add(container);

        propertyRoot.Add(foldout);
        propertyRoot.TrackSerializedObjectValue(property.serializedObject, UpdateSerializedObject);
        return propertyRoot;
    }

    private void UpdateSerializedObject(SerializedObject obj)
    {
        UpdateTimestampLabel(_timeStamp);
    }

    private void UpdateTimestampLabel(SerializedProperty timestamp)
    {
        //Debug.Log(timestamp);
        //Debug.Log(timestamp.type);
        string text = timestamp.longValue == 0 ? "No timestamp" : "Timestamp: " + new DateTime(timestamp.longValue).ToLongTimeString();
        _label.text = "     " + text;
    }
}

#endif