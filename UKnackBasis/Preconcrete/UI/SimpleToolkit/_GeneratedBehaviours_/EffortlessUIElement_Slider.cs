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

public abstract class EffortlessUIElement_Slider : MonoBehaviour
{
    [SerializeField]
    [ProvidedComponent]
    [DisableEditingInPlaymode]
    private UIDocument _document;

    [SerializeField]
    [DisableEditingInPlaymode]
    private string _sliderName;

    protected (UIDocument document, string sliderName) EffortlessUIElement_Slider_Fields 
        => (_document, _sliderName);

    protected Slider _slider { get; private set; }

    protected abstract void LayoutReadyAndElementFound(VisualElement layout);
    protected abstract void LayoutCleanupBeforeDestruction();

    protected void OnEnable()
    {
        _document = ProvidedComponentAttribute.Provide<UIDocument>(this.gameObject, _document);
        _slider = _document.rootVisualElement.Q<Slider>(_sliderName);
        ThrowIfNull(_slider, _sliderName);
        LayoutReadyAndElementFound(_document.rootVisualElement);
    }

    protected void OnDisable()
    {
        LayoutCleanupBeforeDestruction();
    }

    private static void ThrowIfNull(Slider ve, string id)
    {
        if (ve == null)
            throw new System.ArgumentNullException($"Slider with id: {id} not found in UIDocument");
    }
}


