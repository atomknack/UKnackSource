#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UKnack;
using UKnack.Common;
using UnityEditor;
using UnityEngine.UIElements;
using static UKnack.Attributes.KnackPickers.UnityObjectsFinder;
using static UnityEditor.Progress;

namespace UKnack.Attributes.KnackPickers;

public partial class ValidPicker
{
    private class ListViewItems
    {
        private readonly ValidPicker picker;
        public readonly ListView listView;

        internal List<ItemStruct> _items;
        internal int _selectedIndex;

        const string ITEMLABELNAME = "text";
        const string ITEMICONNAME = "icon";

        public ListViewItems(ValidPicker picker)
        {
            this.picker = picker;
            //_items = new List<ItemStruct>();
            _selectedIndex = -1;

            listView = new ListView();
            listView.selectionType = SelectionType.Single;
            ////_scroller.horizontalScrollerVisibility = ScrollerVisibility.Hidden;
            listView.style.height = Length.Percent(100);//new StyleLength(new Length(99, LengthUnit.Percent));
            listView.style.marginLeft = 10;
            listView.style.marginRight = 4;

            listView.makeItem = MakeItem;
            listView.bindItem = BindItem;
            listView.selectedIndicesChanged += SelectedIndicesChanged;
            listView.itemsChosen += ItemsChosen;
            //listView.itemsSource = _items;
        }

        public void UpdateList(List<ItemStruct> items) //, Predicate<ItemStruct> indexOfSelectedFinder)
        {
            _items = items;

            listView.itemsSource = _items;
            SelectSelectedObject();

            //listView.selectedIndex = _selectedIndex = indexOfSelectedFinder == null ? -1 : _items.FindIndex(indexOfSelectedFinder); //_items.FindIndex(itemstruct => itemstruct.Obj == _selectedObject)

            listView.Rebuild();

        }

        public void SelectSelectedObject()
        {
            listView.selectedIndex = _selectedIndex = _items.FindIndex(itemstruct => itemstruct.Obj == picker._selectedObject);
        }

        public async void ScrollToSelected()
        {
            //Debug.Log($"Scroll to Selected {_selectedIndex} called");
            if (_selectedIndex == -1) return;
            await Task.Delay(5); //we need to wait a little bit, especially in the beninning https://forum.unity.com/threads/listview-scrolltoitem-not-working-during-dragperformevent.821277/
            listView.ScrollToItem(_selectedIndex);
        }

        private VisualElement MakeItem()
        {
            VisualElement item = new VisualElement();

            VisualElement icon = new VisualElement();
            icon.name = ITEMICONNAME;
            icon.style.width = 18;
            icon.style.height = 18;
            icon.style.position = Position.Absolute;

            var label = new Label();// $"{text} test element {id} ");
            label.style.left = 18;
            label.style.marginTop = 2;
            label.name = ITEMLABELNAME;
            //label.style.marginTop = 1;
            item.Add(label);
            item.Add(icon);
            return item;
        }

        private void BindItem(VisualElement element, int index)
        {
            //Debug.Log($"{_items.Count} {index}");
            var item = _items[index];
            if(picker.validate!=null)
            {
                try { 
                    picker.validate(item.Obj);
                    element.style.color = picker.validColor;
                    element.tooltip = $"{item.Obj.name} is valid for field";
                }
                catch (Exception ex)
                {
                    element.style.color = picker.invalidColor;
                    element.tooltip = $"Item is invalid for field, validation exception: {ex}";
                }
            }
            else
            {
                element.style.color = StyleKeyword.Null;
                element.tooltip = String.Empty;
            }
            element.Q<Label>(ITEMLABELNAME).text = item.ToListItemName();
            var iconPlaceholder = element.Q<VisualElement>(ITEMICONNAME);
            iconPlaceholder.style.backgroundImage = item.Obj == null ? null : GetPreviewTexture(item.Obj);
        }

        void SelectedIndicesChanged(IEnumerable<int> indices)
        {
            //Debug.Log($"selected indicies: {(string.Join(", ", indices))}");
            int count = indices.Count();
            if (count == 0)
            {
                _selectedIndex = -1;
            }
            if (count == 1)
            {
                _selectedIndex = indices.First();
                ItemStruct item = _items[_selectedIndex];
                picker._selectedObject = item.Obj;
                picker.onItemPick?.Invoke(item.Obj);

                string infoFieldText = string.Empty;
                switch (item.Location)
                {
                    case ObjectLocation.SceneGameobjects:
                        infoFieldText = CommonStatic.GetFullPath_Recursive((UnityEngine.GameObject)item.Obj);
                        break;
                    case ObjectLocation.SceneComponents:
                        infoFieldText = CommonStatic.GetFullPath_Recursive(((UnityEngine.Component)item.Obj).gameObject);
                        break;
                    case ObjectLocation.Assets:
                        infoFieldText = AssetDatabase.GetAssetPath(item.Obj);
                        break;
                }


                picker._infoField.text = infoFieldText;
            }
            //ScrollToSelected();
        }

        void ItemsChosen(IEnumerable<object> items)
        {
            int count = items.Count();
            //Debug.Log($"Chosen items count (count will be repeated 2 times) ; {count} ; {items.Count()} ;");
            if (count == 1)
            {
                var obj = ((ItemStruct)items.First()).Obj;
                picker.onItemPick?.Invoke(obj);
                picker.onItemChosen?.Invoke(obj);
                picker.Close();
            }

        }

    }
}
#endif


/*

        private void CleanUpElementsInsideScroller()
        {
            //Debug.Log("Cleanup inside scroller called");
            
            foreach (var child in _rootInsideScroller.Children())
            {
                if (child.name.Contains("_"))
                {
                    child.UnregisterCallback<ClickEvent>(ItemClickEvent);
                    child.UnregisterCallback<FocusEvent>(ItemFocusEvent);
                    child.UnregisterCallback<BlurEvent>(ItemBlurEvent);
                }
            }
            _rootInsideScroller.Clear();
        }

private void ItemClickEvent(ClickEvent click)
{
    if (click.currentTarget is VisualElement ve)
    {
        //Debug.Log($"{ve.name}, click count: {click.clickCount}");
        ve.focusable = true;
        ve.Focus();
    }
}
private void ItemFocusEvent(FocusEvent focus)
{
    if (focus.currentTarget is VisualElement ve)
    {
        //Debug.Log(ve.style.backgroundColor);
        ve.style.backgroundColor = Color.blue;
    }
}
private void ItemBlurEvent(BlurEvent blur)
{
    if (blur.currentTarget is VisualElement ve)
    {
        //Debug.Log("blur");
        ve.style.backgroundColor = new StyleColor();
    }
}

*/