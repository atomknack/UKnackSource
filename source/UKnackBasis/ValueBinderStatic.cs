using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UIElements;

namespace UKnack;

public static class ValueBinderStatic
{
    public static VisualElement FindSomeKindOfTextStorage(this VisualElement layout, string id)
    {
        VisualElement result = layout.Q<Label>(id);
        return result == null ? layout.Q<TextField>(id) : result;
    }
    public static VisualElement Hide(this VisualElement element, bool condition)
    {
        if (element == null)
            return null;
        if (condition == false)
            return element;
        element.style.display = DisplayStyle.None;
        return element;
    }
    public static VisualElement TryAssignTextWithoutNotification(this VisualElement el, string text)
    {
        if (el == null)
            return null;
        if (el is Label label)
        {
            label.text = text;
            return el;
        }
        if (el is TextField field)
        {
            field.SetValueWithoutNotify(text);
            return el;
        }
        return null;
    }
}
