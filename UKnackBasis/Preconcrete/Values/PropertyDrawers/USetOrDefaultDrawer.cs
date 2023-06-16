#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace UKnack.Preconcrete.Values.PropertyDrawers;

[CustomPropertyDrawer(typeof(USetOrDefault<>), true)]
public class USetOrDefaultDrawer : PropertyDrawer
{
    protected VisualElement _root;
    protected Label _topLabel;
    protected VisualElement _propertyBlock;

    protected SerializedProperty _property;
    protected SerializedProperty _defaultValue => _property.FindPropertyRelative("_defaultValue");

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        _property = property;
        _root = new VisualElement();
        _root.style.marginTop = 4;
        _root.style.marginBottom = 4;
        VisualElement inRootContainer = new VisualElement();
        inRootContainer.style.backgroundColor = new Color(0.1f, 0.2f, 0.2f, 0.2f);
        _topLabel = new Label($"{property.displayName} - {property.type}");
        _topLabel.tooltip = "If value is set from code after game start, then it will be used. \n Otherwise default will be used";
        inRootContainer.Add(_topLabel);
        inRootContainer.style.borderBottomWidth = 1;
        inRootContainer.style.borderLeftWidth = 1;
        inRootContainer.style.borderTopWidth = 1;
        var borderColor = new Color(0.8f, 1, 1, 0.5f);
        inRootContainer.style.borderBottomColor = borderColor;
        inRootContainer.style.borderLeftColor = borderColor;
        inRootContainer.style.borderTopColor = borderColor;

        _propertyBlock = new VisualElement();
        PropertyField defaultValue = new PropertyField(_defaultValue);
        _propertyBlock.Add(defaultValue);
        inRootContainer.Add(_propertyBlock);

        var debugFoldout = new Foldout();
        debugFoldout.text = $"Debug info:";
        //debugFoldout.Add(new Label(property.propertyType.ToString()));
        //debugFoldout.SetValueWithoutNotify(false);

        PropertyField _debugLastValueGet = new PropertyField(property.FindPropertyRelative("_debugLastValueGet"));
        //_DebugLastValueGet.SetEnabled(false);
        PropertyField _debugLastValueSet = new PropertyField(property.FindPropertyRelative("_debugLastValueSet"));

        debugFoldout.Add(_debugLastValueGet);
        debugFoldout.Add(_debugLastValueSet);


        inRootContainer.Add(debugFoldout);
        _root.Add(inRootContainer);
        return _root;
    }
}

#endif