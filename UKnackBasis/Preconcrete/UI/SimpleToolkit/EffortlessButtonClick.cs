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

    internal override void LayoutReady(VisualElement layout)
    {
        _button.clicked += ButtonClicked;
    }

    internal override void LayoutGonnaBeDestroyedNow()
    {
        _button.clicked -= ButtonClicked;
    }
}