#if UNITY_EDITOR

using UKnack.Attributes;
using UnityEditor;
using UnityEngine;

namespace UKnack.Attributes.KnackAttributeDrawers;

[CustomPropertyDrawer(typeof(DisableEditingInPlaymodeAttribute))]
public class DisableEditingInPlaymodeAttributeDrawer : PropertyDrawer
{
    private bool guiEnabledValue;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        BeforeDrawProperty(position, property, label);

        DrawProperty(position, property, label);

        AfterDrawProperty(position, property, label);
    }
    protected virtual void BeforeDrawProperty(Rect position, SerializedProperty property, GUIContent label)
    {
        guiEnabledValue = GUI.enabled;
        bool playing = Application.isPlaying;
        if (playing)
            GUI.enabled = false;
    }
    protected virtual void AfterDrawProperty(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = guiEnabledValue;
    }

    protected virtual void DrawProperty(Rect position, SerializedProperty property, GUIContent label)
    {
        //Debug.Log(label.tooltip);
        label.tooltip = label.tooltip + " \n [ DisableEditingInPlaymodeAttribute: property cannot be changed in editor when game is playing. ] \n";
        EditorGUI.PropertyField(position, property, label, true);
    }

}
#endif