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

public abstract class EffortlessUIElement_Toggle : MonoBehaviour
{
    [SerializeField]
    [ProvidedComponent]
    [DisableEditingInPlaymode]
    private UIDocument _document;

    [SerializeField]
    [DisableEditingInPlaymode]
    private string _toggleName;

    protected Toggle _toggle { get; private set; }

    protected abstract void LayoutReadyAndElementFound(VisualElement layout);
    protected abstract void LayoutCleanupBeforeDestruction();

    protected void OnEnable()
    {
        _document = ProvidedComponentAttribute.Provide<UIDocument>(this.gameObject, _document);
        _toggle = _document.rootVisualElement.Q<Toggle>(_toggleName);
        ThrowIfNotFoundVisualElement(_toggleName, _toggle);
        LayoutReadyAndElementFound(_document.rootVisualElement);
    }

    protected void OnDisable()
    {
        LayoutCleanupBeforeDestruction();
    }

    protected static void ThrowIfNotFoundVisualElement(string id, VisualElement ve)
    {
        if (ve == null)
            throw new System.ArgumentNullException($"button with id: {id} not found in UIDocument");
    }
}


