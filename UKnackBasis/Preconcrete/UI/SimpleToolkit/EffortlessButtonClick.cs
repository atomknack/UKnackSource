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

    protected override void LayoutReady(VisualElement layout)
    {
        _button.clicked += ButtonClicked;
    }

    protected override void LayoutGonnaBeDestroyedNow()
    {
        _button.clicked -= ButtonClicked;
    }
}