#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Reflection;
using UKnack.Attributes.KnackPickers;
using UnityEditor;
using UnityEngine;

namespace UKnack.Attributes.KnackAttributeDrawers;

public abstract class AbstractReferenceWithPickerDrawer : OneOfMarksAttributeDrawer
{
    protected Action<UnityEngine.Object> validation = null;
    protected List<Type> _subtypes = null;
    protected string s_showPopupMenu = $"\n\nClick right mouse button on label to show subclass menu.";

    protected int _undoGroupId = 0;
    protected int _undoCount = 0;

    protected override bool BeforeDrawProperty(Rect position, SerializedProperty property, GUIContent label)
    {
        Type uObject = typeof(UnityEngine.Object);
        Type uObjectArray = typeof(UnityEngine.Object[]);
        if (!uObject.IsAssignableFrom(fieldInfo.FieldType)
          && !uObjectArray.IsAssignableFrom(fieldInfo.FieldType)) //fieldType.IsSubclassOf(typeof(UnityEngine.Object)) == false )
        {
            DrawError(position, property);
            return false;
        }

        if (property.propertyType != SerializedPropertyType.ObjectReference)
        {
            EditorGUI.DrawRect(new Rect(position.x - 2, position.y - 2, position.width + 4, position.height + 4), new Color(0.25f, 0.05f, 0.05f));
            string tooltip = $"{nameof(ValidReferenceAttribute)} can only be applied to reference type.";
            EditorGUI.LabelField(position, new GUIContent($"\"{label}\" with {nameof(ValidReferenceAttribute)} cannot be drawed ...", tooltip));
            //Debug.Log(property.propertyType);
            return false;
        }

        return true;

        void DrawError(Rect position, SerializedProperty property)
        {
            EditorGUI.DrawRect(new Rect(position.x, position.y, position.width + 2, position.height), new Color(0.45f, 0.02f, 0.02f));
            EditorGUI.DrawRect(new Rect(position.x, position.y, 20, position.height), new Color(0.25f, 0.01f, 0.01f));
            string tooltip = $"{nameof(ValidReferenceAttribute)} can NOT be used with field of type {fieldInfo.FieldType}";
            EditorGUI.LabelField(position, new GUIContent($"{fieldInfo.Name} with {nameof(ValidReferenceAttribute)} cannot be drawed ...", tooltip));
        }

    }

    protected override void AfterDrawProperty(Rect position, SerializedProperty property, GUIContent label)
    {
        if (_subtypes == null || _subtypes.Count == 0)
        {
            s_showPopupMenu = String.Empty;
            return;
        }


        if (GUI.enabled == false)
            return;

        if (IsPlayingAndDisabled)
            return;

        Event e = Event.current;

        if (_subtypes.Count == 1 && _subtypes[0] == typeof(UnityEngine.GameObject)) // GetTypeFromFieldInfo(fieldInfo))
        {
            if (e.type == EventType.MouseDown && e.button == 0 && KnackPickerButtonPosition(position).Contains(e.mousePosition))
            {
                ShowPicker(property, _subtypes[0], null, GameObjectValidateAsFilterPredicate);
                return;
            }
            return;
        }

        if (e.type == EventType.MouseDown && e.button == 0 && KnackPickerButtonPosition(position).Contains(e.mousePosition))
        {
            ShowPicker(property, _subtypes[0]);
            return;
        }

        if (_subtypes.Count == 1)
            return;

        if (e.type == EventType.MouseDown && e.button == 1 && KnackPickerButtonPosition(position).Contains(e.mousePosition))
        {
            GenericMenu contextMenu = new GenericMenu();
            foreach (var subtype in _subtypes)
                ShowContextMenuForType(contextMenu, property, subtype);
            contextMenu.ShowAsContext();
        }
    }

    protected void ShowContextMenuForType(GenericMenu menu, SerializedProperty property, Type context)
    {
        if (context != null)
            menu.AddItem(new GUIContent($"Pick as {context.FullName}"), false, () => ShowPicker(property, context));
    }

    protected void ShowPicker(SerializedProperty property, Type pickerFilterType, string pickerTitle = null, Predicate<UnityEngine.Object> filter = null)
    {
        var targetObject = property.serializedObject.targetObject;

        GameObject owner = null;
        if (targetObject != null)
        {
            if (targetObject is Component comp)
                owner = comp.gameObject;
            else if (targetObject is GameObject go)
                owner = go;
        }

        Undo.SetCurrentGroupName("Picked by picker");
        _undoGroupId = Undo.GetCurrentGroup();
        _undoCount = 0;

        Undo.RecordObject(targetObject, "Picked");
        Type baseType = GetTypeFromFieldInfo(fieldInfo);

        if (filter == null)
            filter = x => x != null && pickerFilterType.IsAssignableFrom(x.GetType());

        var picker = ValidPicker.OpenPicker(
            pickerTitle == null ? $"Picker for basetype:{baseType}, with filter of {pickerFilterType}" : pickerTitle,
            property,
            baseType,
            owner,
            filter
            );
        picker.validate = validation;
        picker.onItemPick = x =>
        {
            if (property == null)
                return;
            if (x == property.objectReferenceValue)
                return;
            //DoUndo();
            //Undo.RecordObject(targetObject, "Picked");
            property.objectReferenceValue = x;
            property.serializedObject.ApplyModifiedProperties();
            _undoCount++;
            //undo++;

        };
        picker.onCancel = () =>
        {
            if (property == null)
                return;
            //Debug.Log($"{_undoGroupId} {Undo.GetCurrentGroup()} {Undo.GetCurrentGroupName()}");
            //Undo.RevertAllInCurrentGroup();
            if (Undo.GetCurrentGroup() != _undoGroupId && _undoCount > 0)
            {
                Undo.RevertAllDownToGroup(_undoGroupId);
            }

            //DoUndo();
            //Debug.Log($"picker onCancel returned, {_undoGroupId}");
        };
        /*ObjectProviderUtilities.ResolveProviderTypeToProvider(
            (ObjectProviderType.Scene | ObjectProviderType.Assets), 
            ExtractFieldType(fieldInfo),
            targetObject,
            true
            ),
        null
        );
                        */
    }

    protected bool IsPlayingAndDisabled
    {
        get
        {
            var _playmodeDisabled = fieldInfo.GetCustomAttribute(typeof(DisableEditingInPlaymodeAttribute), true);
            if (_playmodeDisabled != null && Application.isPlaying)
                return true;
            return false;
        }
    }

    protected static void AddUniquePickerType(List<Type> list, Type t, Type baseFieldType = null)
    {
        if (t == typeof(UnityEngine.Object))
            return;
        if (baseFieldType != null && baseFieldType == t && t.IsGenericType == false)
            return;
        if (list.Contains(t))
            return;
        list.Add(t);
    }

    protected static void DrawCustomButtonBackground(List<Type> subtypes, Rect position) =>
        DrawCustomButtonBackground(subtypes, position, Color.black);

    protected static void DrawCustomButtonBackground(List<Type> subtypes, Rect position, Color color)
    {
        if (subtypes == null || subtypes.Count == 0)
            return;
        EditorGUI.DrawRect(new Rect(position.x - 2, position.y - 2, 22, position.height + 4), color);
    }

    protected static void DrawCustomPickerButton(IReadOnlyList<Type> subtypes, Rect position, bool disabled)
    {
        if (subtypes == null || subtypes.Count == 0)
            return;
        Rect buttonPosition = KnackPickerButtonPosition(position);

        EditorGUI.DrawRect(buttonPosition, new Color(0.2f, 0.2f, 0.2f, 0.7f));
        Event e = Event.current;
        if (e == null)
            return;
        if (buttonPosition.Contains(e.mousePosition) && disabled == false)
            EditorGUI.DrawRect(buttonPosition, new Color(0.12f, 0.12f, 0.12f));

        if (subtypes.Count > 1)
            EditorGUI.DrawRect(new Rect(buttonPosition.x, buttonPosition.y+buttonPosition.height-2, 10, 1), Color.yellow);
    }

    protected static Rect KnackPickerButtonPosition(Rect propertyPosition) =>
        new Rect(propertyPosition.x, propertyPosition.y, 20, propertyPosition.height);

    bool GameObjectValidateAsFilterPredicate(UnityEngine.Object obj)
    {
        try
        {
            validation(obj);
        }
        catch
        {
            return false;
        }
        return true;
    }

}
#endif