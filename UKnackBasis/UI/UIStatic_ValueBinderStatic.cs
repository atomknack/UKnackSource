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
        if (el is TextField field)
        {
            field.SetValueWithoutNotify(text);
            return el;
        }
        if (el is TextElement textElement)
        {
            var valChanged = (INotifyValueChanged<string>)el;
            if (valChanged.value != text)
                valChanged.SetValueWithoutNotify(text);
        }
        //if (el is Label label) //Label is TextElement
        //{
        //    label.text = text;
        //    return el;
        //}
        return null;
    }
}
