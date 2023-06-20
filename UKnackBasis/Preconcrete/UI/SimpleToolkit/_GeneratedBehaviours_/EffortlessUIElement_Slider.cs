//----------------------------------------------------------------------------------------
// <auto-generated> This code was generated from EffortlessUIElement
// Changes will be lost if the code is regenerated.</auto-generated>
//----------------------------------------------------------------------------------------




#nullable disable

using UKnack.Attributes;
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

    protected Slider _slider { get; private set; }

    protected virtual void OnEnable()
    {
        _document = ProvidedComponentAttribute.Provide<UIDocument>(this.gameObject, _document);
        _slider = _document.rootVisualElement.Q<Slider>(_sliderName);
        ThrowIfNotFoundVisualElement(_sliderName, _slider);
    }

    protected virtual void OnDisable()
    {
    }

    protected static void ThrowIfNotFoundVisualElement(string id, VisualElement ve)
    {
        if (ve == null)
            throw new System.ArgumentNullException($"button with id: {id} not found in UIDocument");
    }

}


