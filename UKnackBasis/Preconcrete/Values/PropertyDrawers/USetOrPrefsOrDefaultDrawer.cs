#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace UKnack.Preconcrete.Values.PropertyDrawers;

[CustomPropertyDrawer(typeof(USetOrPrefsOrDefault<>), true)]
public class USetOrPrefsOrDefaultDrawer : USetOrDefaultDrawer
{
    protected SerializedProperty _playerPrefsName => _property.FindPropertyRelative("_prefsKeyName");

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        var baseResult = base.CreatePropertyGUI(property);
        PropertyField playerPrefsName = new PropertyField(_playerPrefsName);//, $"{_playerPrefsName.displayName}");
        //playerPrefsName.tooltip = playerPrefsName.tooltip + " if this property empty, whitespaces or null no prefs will be used";
        _propertyBlock.Add( playerPrefsName );
        _topLabel.tooltip = "If value is set from code after game start, then it will be used. \n Otherwise if _playerPrefsName not empty (_playerPrefs value updated when value is set) and there is stored value, then it will be used. \n Otherwise if no value was set and there is no stored value in playerPrefs, then default will be used";
        return baseResult;
    }
}

#endif