//----------------------------------------------------------------------------------------
// <auto-generated> This code was generated from EffortlessUIElement
// Changes will be lost if the code is regenerated.</auto-generated>
//----------------------------------------------------------------------------------------




#nullable disable

using UKnack.Attributes;
using UKnack.UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace UKnack.Preconcrete.UI.SimpleToolkit;

public abstract class EffortlessUIElement_Button : MonoBehaviour
{
    [SerializeField]
    [ProvidedComponent]
    [DisableEditingInPlaymode]
    private UIDocument _document;

    [SerializeField]
    [DisableEditingInPlaymode]
    private string _buttonName;

    protected (UIDocument document, string buttonName) EffortlessUIElement_Button_Fields 
        => (_document, _buttonName);

    protected Button _button { get; private set; }

    protected abstract void LayoutReadyAndElementFound(VisualElement layout);
    protected abstract void LayoutCleanupBeforeDestruction();

    protected void OnEnable()
    {
        _document = ProvidedComponentAttribute.Provide<UIDocument>(this.gameObject, _document);
        _button = _document.rootVisualElement.Q<Button>(_buttonName);
        ThrowIfNull(_button, _buttonName);
        LayoutReadyAndElementFound(_document.rootVisualElement);
    }

    protected void OnDisable()
    {
        LayoutCleanupBeforeDestruction();
    }

    private static void ThrowIfNull(Button ve, string id)
    {
        if (ve == null)
            throw new System.ArgumentNullException($"Button with id: {id} not found in UIDocument");
    }
}


