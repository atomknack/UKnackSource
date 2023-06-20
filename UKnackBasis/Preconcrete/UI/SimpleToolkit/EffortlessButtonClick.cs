using System;
using System.Collections;
using System.Collections.Generic;
using UKnack.Attributes;
using UnityEngine;
using UnityEngine.UIElements;

namespace UKnack.Preconcrete.UI.SimpleToolkit;

public abstract class EffortlessButtonClick : EffortlessUIElement_Button
{
    protected abstract void ButtonClicked();

    protected override void OnEnable()
    {
        base.OnEnable();
        _button.clicked += ButtonClicked;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _button.clicked -= ButtonClicked;
    }

}