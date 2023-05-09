#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.UIElements;

namespace UKnack.Attributes.KnackPickers;

public partial class ValidPicker
{
    private class TextSearchField : TextField
    {
        private readonly ListViewItems _listViewItems;
        public string placeholder { get => _placeholderLabel.text; set => _placeholderLabel.text = value; }
        public override string value { get => base.value; set => PlaceholderVisibility(base.value = value); }

        public TextSearchField(ListViewItems listView) : base()
        {
            _listViewItems = listView;
            _hasFocus = false;
            _placeholderLabel = new Label();
            _placeholderLabel.focusable = false;
            _placeholderLabel.style.position = Position.Absolute;
            _placeholderLabel.style.left = 4;
            _placeholderLabel.style.top = 2;
            this.Add(_placeholderLabel);
            this.RegisterCallback<FocusInEvent>(FocusIn);
            this.RegisterCallback<FocusOutEvent>(FocusOut);
            this.RegisterCallback<KeyDownEvent>(KeyDown);
        }

        public override void SetValueWithoutNotify(string newValue)
        {
            base.SetValueWithoutNotify(newValue);
            PlaceholderVisibility(newValue);
        }


        private string PlaceholderVisibility(string newValue)
        {
            if (_placeholderLabel == null)
                return newValue;
            if (string.IsNullOrEmpty(newValue))
                MaybeMakePlaceholderVisible();
            else
                _placeholderLabel.visible = false;
            return newValue;
        }

        private void KeyDown(KeyDownEvent e)
        {

            int maxIndex = _listViewItems._items.Count - 1;

            //Debug.Log($"{maxIndex} Search Field key down {e.keyCode}");

            if (e.keyCode == KeyCode.DownArrow)
            {
                if (maxIndex >= 0)
                {
                    //Debug.Log($"Pressed down {maxIndex}");
                    SelectLine(0);
                }
            }
            if (e.keyCode == KeyCode.UpArrow)
            {
                if (maxIndex >= 0)
                {
                    //Debug.Log($"Pressed up {maxIndex}");
                    SelectLine(maxIndex);
                }

            }

            void SelectLine(int index)
            {
                _listViewItems.listView.SetSelection(index);
                _listViewItems.ScrollToSelected();
                _listViewItems.listView.Focus();
            }
        }

        private bool _hasFocus;
        void FocusIn(FocusInEvent ev)
        {
            _hasFocus = true;
            MakePlaceholderInvisible();
        }
        void FocusOut(FocusOutEvent ev)
        {
            _hasFocus = false;
            MaybeMakePlaceholderVisible();
        }
        void MakePlaceholderInvisible()
        {
            _placeholderLabel.visible = false;
        }

        void MaybeMakePlaceholderVisible()
        {
            if (_hasFocus)
                return;
            if (string.IsNullOrEmpty(value))
            {
                _placeholderLabel.visible = true;
            }
        }

        private Label _placeholderLabel;

    }

}
#endif