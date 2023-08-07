#if UNITY_EDITOR

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UKnack.Attributes.KnackPickers;
using UnityEditor;
using UnityEngine;

namespace UKnack.Attributes.KnackAttributeDrawers;

[CustomPropertyDrawer(typeof(ValidReferenceAttribute))]
public class ValidReferenceAttributeDrawer : AbstractReferenceWithPickerDrawer
{
    //private ValidReferenceAttribute _attribute => (ValidReferenceAttribute)attribute;
    //private int _currentPickerWindow = -1;

    protected ValidReferenceAttribute _validReferenceAttribute => (ValidReferenceAttribute)attribute;

    private Type typeForPicker = null;

    public override bool CanCacheInspectorGUI(SerializedProperty property) => false;


    protected override void DrawProperty(Rect position, SerializedProperty property, GUIContent label)
    {
        FillSubtypes();

        DrawCustomButtonBackground(_subtypes, position);

        try
        {
            ValidateProperty(property);
        }
        catch (Exception e)
        {
            FieldSubDrawerOnVerificationException(position, property, label, e);
            return;
        }
        FieldSubDrawerOnSuccess(position, property, label);

        void FillSubtypes()
        {
            if (_subtypes != null)
                return;

            Type baseFieldType = GetTypeFromFieldInfo(fieldInfo);

            _subtypes = new List<Type>();
            if (_validReferenceAttribute.subType1 != null)
                AddUniquePickerType(_subtypes, _validReferenceAttribute.subType1, baseFieldType);
            if (_validReferenceAttribute.subType2 != null)
                AddUniquePickerType(_subtypes, _validReferenceAttribute.subType2, baseFieldType);
            if (_validReferenceAttribute.moreSubtypes != null)
                foreach (Type t in _validReferenceAttribute.moreSubtypes)
                    if (t != null)
                        AddUniquePickerType(_subtypes, t, baseFieldType);
            
            AddUniquePickerType(_subtypes, GetTypeFromFieldInfo(fieldInfo), baseFieldType);

            if (_subtypes.Count > 0)
                return;
            if (baseFieldType != typeof(UnityEngine.GameObject))
                return;
            if (_validReferenceAttribute.containerOfValidationMethod == typeof(ValidReferenceAttribute) &&
                _validReferenceAttribute.prefabValidationMethodName == nameof(ValidReferenceAttribute.NotNull))
                return;

            _subtypes.Add(typeof(UnityEngine.GameObject));
        }
    }

    protected void FieldSubDrawerOnSuccess(Rect position, SerializedProperty property, GUIContent label)
    {

        //bool guiEnabled = GUI.enabled;
        //GUI.enabled = false;
        EditorGUI.DrawRect(new Rect(position.x, position.y, position.width + 2, position.height), DrawersSettings.fieldPassedVerification);
        DrawCustomPickerButton(_subtypes, position, IsPlayingAndDisabled);
        GUIContent newLabel = new GUIContent();
        newLabel.image = label.image;
        newLabel.text = label.text;
        string newTooltip = $"Field passed Verification. {s_showPopupMenu}";
        if (string.IsNullOrWhiteSpace(label.tooltip) == false)
            newTooltip += $"\n\nAdditional tooltip: \n{label.tooltip}";
        newLabel.tooltip = newTooltip;
        EditorGUI.PropertyField(position, property, newLabel, true);
        // GUI.enabled = guiEnabled;
    }

    protected void FieldSubDrawerOnVerificationException(Rect position, SerializedProperty property, GUIContent label, Exception e)
    {
        DrawCustomButtonBackground(_subtypes, position);
        EditorGUI.DrawRect(new Rect(position.x, position.y, position.width + 2, position.height), DrawersSettings.fieldExceptionAtVerification);
        DrawCustomPickerButton(_subtypes, position, IsPlayingAndDisabled);
        EditorGUI.PropertyField(position, property, new GUIContent(label.text, label.image, $"Reference verification exception: {(e.InnerException == null ? e : e.InnerException)}  {s_showPopupMenu}"), true);

    }

    protected void ValidateProperty(SerializedProperty property)
    {
        Type container = _validReferenceAttribute.containerOfValidationMethod;
        if (container == null)
        {
            container = fieldInfo.DeclaringType;
            //container = property.serializedObject.targetObject.GetType();
        }

        var method = container.GetMethod(_validReferenceAttribute.prefabValidationMethodName,
            BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        if (method == null)
            throw new Exception($"Cannot find method: {_validReferenceAttribute.prefabValidationMethodName} in {_validReferenceAttribute.containerOfValidationMethod}");
        if (validation == null)
        {
            if (method.IsStatic)
            {
                if (method.ReturnType.Equals(typeof(void)))
                {
                    validation = (Action<UnityEngine.Object>)Delegate.CreateDelegate(typeof(Action<UnityEngine.Object>), method);
                }
                else
                {
                    var d = (Func<UnityEngine.Object, object>)Delegate.CreateDelegate(typeof(Func<UnityEngine.Object, object>), method);
                    validation = obj => d(obj);
                }
            }
            else
            {
                UnityEngine.Object target = property.serializedObject.targetObject;
                if (method.ReturnType.Equals(typeof(void)))
                {
                    validation = (Action<UnityEngine.Object>)Delegate.CreateDelegate(typeof(Action<UnityEngine.Object>), target, method);
                }
                else
                {
                    var d = (Func<UnityEngine.Object, object>)Delegate.CreateDelegate(typeof(Func<UnityEngine.Object, object>), target, method);
                    validation = obj => d(obj);
                }
            }
        }


        validation(property.objectReferenceValue);
        //method.Invoke(null, new object[] { property.objectReferenceValue });
    }

}
#endif


/*
    private static bool IsIncorrectUse(Type fieldType, Type picked)
    {
        return false;

        if (picked == null) 
            return false;
        //if (picked.IsSubclassOf(fieldType))
        //    return false;
        if (fieldType.IsAssignableFrom(picked))
            return false;

        return true;
    }
*/

/*
private void ShowContextMenuForType(GenericMenu menu, Type context, SerializedProperty property)
{
    if ( context != null && context.IsSubclassOf(fieldInfo.FieldType))
            menu.AddItem(new GUIContent(context.FullName), false, () => SetPick(context, property));
}

private void SetPick(Type subclass, SerializedProperty property)
{
    typeForPicker = subclass;
    _currentPickerWindow = -1001;
    //Task.Delay(1000).ContinueWith(t => Pick(subclass));

    ////EditorWindow[] allWindows = Resources.FindObjectsOfTypeAll<EditorWindow>();
    ////foreach (var window in allWindows)
    ////{
    ////    EditorUtility.SetDirty(window);
    ////}

    //EditorUtility.SetDirty(property);

    //var serObj = property.serializedObject;
    //serObj.Update();
    //foreach(var target in serObj.targetObjects)
    //{
    //    EditorUtility.SetDirty(serObj.targetObjects);
    //}
    //EditorUtility.SetDirty(serObj.targetObjects);
}

private void Pick(Type subclass)
{
    _currentPickerWindow = CreateCustomPickerId();

    //Type a = null;
    //UnityEngine.Debug.Log(a is null);
    //typeof(EditorGUIUtility).GetMethod
    //EditorGUIUtility.ShowObjectPicker(a)
    MethodInfo method = typeof(EditorGUIUtility).GetMethod("ShowObjectPicker", 1,
        new Type[] { typeof(UnityEngine.Object), typeof(bool), typeof(string), typeof(int) });// _attribute.@interface });

    //UnityEngine.Debug.Log(method.ToString());

    //.MakeGenericMethod(new Type[] { _attribute.@interface });
    MethodInfo generic = method.MakeGenericMethod(new Type[] { subclass }); //_attribute.@interface });

    //UnityEngine.Debug.Log(generic.ToString());

    generic.Invoke(this, new object[] { null, true, "", _currentPickerWindow });
    //ShowObjectPicker(Object obj, bool allowSceneObjects, string searchFilter, int controlID);
}

private static int CreateCustomPickerId() =>
    EditorGUIUtility.GetControlID(FocusType.Passive) + 100;
*/
