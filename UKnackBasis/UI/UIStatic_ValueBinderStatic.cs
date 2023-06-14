using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UIElements;

namespace UKnack.UI;

public static partial class UIStatic
{
    public static VisualElement TryFindSomeKindOfTextStorage(this VisualElement layout, string id)
    {
        VisualElement result = layout.Q<Label>(id);
        return result == null ? layout.Q<TextField>(id) : result;
    }
    public static VisualElement TryHide(this VisualElement element, bool condition = true, bool bySettingDisplayAsNone = true)
    {
        if (element == null)
            return null;
        if (condition == false)
            return element;

        if (bySettingDisplayAsNone)
            element.style.display = DisplayStyle.None;
        else
            element.visible = false;

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
