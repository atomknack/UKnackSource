#nullable disable
using System;
using System.Collections;
using System.Collections.Generic;
using UKnack.Attributes;
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

    protected Button _button { get; private set; }

    protected void OnEnable()
    {
        _document = ProvidedComponentAttribute.Provide<UIDocument>(this.gameObject, _document);
        _button = _document.rootVisualElement.Q<Button>(_buttonName);
        ThrowIfNullVisualElement(_buttonName, _button);
    }

    protected void OnDisable()
    {
        ThrowIfNullVisualElement(_buttonName, _button);
    }
    private static void ThrowIfNullVisualElement(string id, VisualElement ve)
    {
        if (ve == null)
            throw new ArgumentNullException($"button with id: {id} not found in UIDocument");
    }

}