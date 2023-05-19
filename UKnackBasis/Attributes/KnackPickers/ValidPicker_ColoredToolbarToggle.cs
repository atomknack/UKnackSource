#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace UKnack.Attributes.KnackPickers;

public partial class ValidPicker
{
    private class ColoredToolbarToggle : ToolbarToggle
    {
        public ColoredToolbarToggle() : base()
        {
            style.unityFontStyleAndWeight = FontStyle.Bold;
            //style.unityTextOutlineWidth = 1;
            //style.unityTextOutlineColor = Color.white;
            RegisterCallback<ChangeEvent<bool>>(ValueChangedEvent);
        }
        private Color _checkedTextColor = Color.red;
        private Color _uncheckedTextColor = new Color(0.2f, 0, 0);
        public Color checkedTextColor { get { return _checkedTextColor; } set { _checkedTextColor = value; UpdateVisualState(); } }
        public Color uncheckedTextColor { get { return _uncheckedTextColor; } set { _uncheckedTextColor = value; UpdateVisualState(); } }
        public void UpdateVisualState()
        {
            if (value)
            {
                style.color = _checkedTextColor;
            }
            else
            {
                style.color = _uncheckedTextColor;
            }
        }

        private void ValueChangedEvent(ChangeEvent<bool> changed)
        {
            UpdateVisualState();
        }
    }
}
#endif