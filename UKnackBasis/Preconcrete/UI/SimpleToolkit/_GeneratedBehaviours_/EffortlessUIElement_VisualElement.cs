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

public abstract class EffortlessUIElement_VisualElement : MonoBehaviour
{
    [SerializeField]
    [ProvidedComponent]
    [DisableEditingInPlaymode]
    private UIDocument _document;

    [SerializeField]
    [DisableEditingInPlaymode]
    private string _visualElementName;

    protected (UIDocument document, string visualElementName) EffortlessUIElement_VisualElement_Fields 
        => (_document, _visualElementName);

    protected VisualElement _visualElement { get; private set; }

    protected abstract void LayoutReadyAndElementFound(VisualElement layout);
    protected abstract void LayoutCleanupBeforeDestruction();

    protected void OnEnable()
    {
        _document = ProvidedComponentAttribute.Provide<UIDocument>(this.gameObject, _document);
        _visualElement = _document.rootVisualElement.Q<VisualElement>(_visualElementName);
        ThrowIfNotFoundVisualElement(_visualElementName, _visualElement);
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


