#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
//using static UnityEditor.FilePathAttribute;

namespace UKnack.Attributes.KnackPickers;

public partial class ValidPicker : EditorWindow
{
    /// <summary>
    /// Called every time user or focus picks item, can be called multiple times with same item
    /// </summary>
    public Action<UnityEngine.Object> onItemPick;
    /// <summary>
    /// Called if user double clicks on item, choosing item and closing picker
    /// </summary>
    public Action<UnityEngine.Object> onItemChosen;
    /// <summary>
    /// Called only once AND ONLY if user pressed ESC button
    /// NOT called on closing by conditions: lost focus, closed by clicking close button
    /// </summary>
    public Action onCancel;
    /// <summary>
    /// Validation method for picked field
    /// </summary>
    public Action<UnityEngine.Object> validate;
    public Color validColor = new Color(0.6f, 1, 0.6f);
    public Color invalidColor = new Color(1, 0.6f, 0.6f);

    SerializedProperty _serializedProperty;
    SerializedObject _serializedObject;

    internal Type _type;
    internal GameObject _owner;
    internal Predicate<UnityEngine.Object> _filter;
    internal UnityEngine.Object _selectedObject = null;


    private static float s_itemScale = 1.5f;
    private ListViewItems _listViewItems;

    private List<ItemStruct> _dataSet;

    private TextSearchField _searchField; //private TextField _searchField;
    private ColoredToolbarToggle _alphabeticalToggle;
    //private ColoredToolbarToggle _validOnlyToggle;
    private Label _infoField;
    private float _lastWindowWidth = 200;
    private bool _dirty = false;
    private bool ESCKeyWasPressed = false;

    private float GetWindowWidth() => position.width;
    public static Texture2D GetPreviewTexture(UnityEngine.Object obj) =>
        obj == null ? null : AssetPreview.GetMiniThumbnail(obj);

    private bool _onCancelWasCalled;

    public static Func<string, SerializedProperty, Type, GameObject, Predicate<UnityEngine.Object>, ValidPicker>
        OpenPicker = OpenPickerStatic;

    private static ValidPicker OpenPickerStatic(
    string title,
    //Action<UnityEngine.Object> onPick,
    SerializedProperty property,
    Type type,
    GameObject owner,
    Predicate<UnityEngine.Object> filter = null
        //,UnityEngine.Object selectedObject = null
        )
    {
        var picker = GetWindow<ValidPicker>(true, title, true);
        picker._serializedProperty = property;
        picker._serializedObject = property.serializedObject;
        picker._selectedObject = picker._serializedProperty.objectReferenceValue;

        picker.ESCKeyWasPressed = false;

        picker.onItemPick = null;//onPick //everytime when item picked;
        picker.onItemChosen = null; //if successufuly chosen, will be called after onItemPick
        picker.onCancel = null;

        picker._onCancelWasCalled = false;

        picker._type = type;
        picker._owner = owner;
        picker._filter = filter;


        picker.InitSelectableElements();
        picker.UpdateSelectableElements();

        return picker;
    }

    private void CreateGUI()
    {
        minSize = new Vector2(340, 400);

        VisualElement root = rootVisualElement;
        rootVisualElement.panel.visualTree.RegisterCallback<KeyDownEvent>(KeyDown, TrickleDown.TrickleDown);

        var top = new VisualElement();
        top.style.flexDirection = FlexDirection.Row;
        top.style.height = 24;
        top.style.marginTop = 2;
        top.style.marginLeft = 6;
        top.style.marginRight = 4;
        top.style.width = new StyleLength(new Length(100, LengthUnit.Percent));

        _listViewItems = new ListViewItems(this);

        _searchField = new TextSearchField(_listViewItems);
        _searchField.multiline = false;
        _searchField.style.height = 20;
        _searchField.placeholder = "Search:";
        _searchField.RegisterCallback<ChangeEvent<string>>(SearchFieldChanged);

        top.Add(_searchField);
        var topSeparator = new VisualElement();
        topSeparator.style.width = 10;
        top.Add(topSeparator);

        _alphabeticalToggle = new ColoredToolbarToggle() {
            text = "A:",
            tooltip = "Sort elements alphabetically",
            style = {
                fontSize = 15,
                height = 20,
            },
            checkedTextColor = new Color(1f, 0.3f, 0.3f),
            uncheckedTextColor = new Color(0.1f, 0, 0)
        };
        _alphabeticalToggle.RegisterCallback<ChangeEvent<bool>>(_ => UpdateSelectableElements());

        /*
        _validOnlyToggle = new ColoredToolbarToggle()
        {
            text = "V:",
            tooltip = "Show only valid items",
            style =
            {
                fontSize = 15,
                height = 20,
            },
            checkedTextColor = new Color(0.3f, 1f, 0.3f),
            uncheckedTextColor = new Color(0, 0.1f, 0)
        };
        _validOnlyToggle.RegisterCallback<ChangeEvent<bool>>(_ => UpdateSelectableElements());
        */


        top.Add(_alphabeticalToggle);
        //top.Add(_validOnlyToggle);
        //_sizeSlider = new Slider(1, 5);
        //_sizeSlider.tooltip = "!!! Scale is not working at the moment, if you know how to correctly scale ListView, please contact me !!!";
        //////_sizeSlider.style.height = 20;
        //_sizeSlider.SetValueWithoutNotify(s_itemScale);
        //_sizeSlider.RegisterValueChangedCallback(CheckItemsScaleChanged);
        //_sizeSlider.style.width = Length.Percent(100);
        //top.Add(_sizeSlider);

        root.Add(top);
        var rootSeparator = new VisualElement();
        rootSeparator.style.height = 4;
        root.Add(rootSeparator);

        root.Add(_listViewItems.listView);

        _infoField = new Label {
            style = {
                width = Length.Percent(100),
                height = 24,
                marginTop = 2,
                paddingTop = 3,
                borderTopWidth = 2,
                borderTopColor = new Color(0.2f,0.2f,0.2f)
            }
        };
        root.Add(_infoField);

        ItemsScaleChanged(s_itemScale);
        //ResizeElementsToScaleAndWindowSize(GetWindowWidth());
        _dirty = true;
    }

    private void SearchFieldChanged(ChangeEvent<string> evt)
    {
        UpdateSelectableElements();
    }

    void UpdateSelectableElements()
    {
        if (_dataSet == null)
            return;

        string search = _searchField.text;
        List<ItemStruct> updatedList = string.IsNullOrWhiteSpace(search) ?
            _dataSet.ToList() :
            _dataSet.Where(item => item.ToListItemName().Contains(search, StringComparison.InvariantCultureIgnoreCase)).ToList();

        if (_alphabeticalToggle.value)
            updatedList.Sort();

        updatedList.Insert(0, new ItemStruct { Location = UnityObjectsFinder.ObjectLocation.Unknown, Obj = null });

        //Debug.Log("UpdateSelectableElements Before UpdateList");
        _listViewItems.UpdateList(updatedList);
        //_listViewItems.ScrollToSelected();

        _dirty = true;


    }

    void InitSelectableElements()
    {
        _infoField.text = String.Empty;
        _searchField.SetValueWithoutNotify(String.Empty);

        _dataSet = ClearedOrNew(_dataSet);

        UnityObjectsFinder.ObjectLocation location = UnityObjectsFinder.ObjectLocation.Unknown;

        foreach (var item in UnityObjectsFinder.LookUpType(_type, _owner, _filter, NewItemType))
        {
            _dataSet.Add(new ItemStruct { Location = location, Obj = item });
        }

        void NewItemType(UnityObjectsFinder.ObjectLocation newLocation)
        {
            location = newLocation;
            ////_rootInsideScroller.Add(new Label($"{(int)newLocation} {newLocation.ToString()}:"));
        }

        List<T> ClearedOrNew<T>(List<T> objects)
        {
            if (objects == null)
                return new List<T>();
            objects.Clear();
            return objects;
        }
    }



    void CheckItemsScaleChanged(ChangeEvent<float> e)
    {
        if (e.newValue == s_itemScale)
            return;
        ItemsScaleChanged(e.newValue);
    }

    protected virtual void ItemsScaleChanged(float newValue)
    {
        s_itemScale = newValue;
        var scale = new Scale(new Vector2(s_itemScale, s_itemScale));
        //_listView.fixedItemHeight = (int)(newValue * 20);

        //_listView.style.transformOrigin = new TransformOrigin(new Length(0, LengthUnit.Percent), new Length(0, LengthUnit.Percent));
        //_listView.style.scale = scale;

        ////Debug.Log(s_itemScale);

        //_rootInsideScroller.style.transformOrigin = new TransformOrigin(new Length(0, LengthUnit.Percent), new Length(0, LengthUnit.Percent));

        //_rootInsideScroller.style.scale = scale;

        ////foreach (var label in _rootInsideScroller.Children())
        ////    label.style.scale = scale;
        _dirty = true;




    }



    private void OnGUI()
    {
        CheckEscAndCallOnCancelIfNotAlreadyCalled("OnGUI"); 
    }

    

    protected virtual void OnLostFocus()
    {
        CheckEscAndCallOnCancelIfNotAlreadyCalled("OnLostFocus");
        Close();
    }

    protected virtual void OnDestroy()
    {
        CheckEscAndCallOnCancelIfNotAlreadyCalled("OnDestroy", true);

        //TODO additional some cleanup if needed
    }

    protected virtual void OnInspectorUpdate()
    {
        if (SerializedObjectOrSerializedPropertyGoneAwry())
            return;
        if (_selectedObject!= _serializedProperty.objectReferenceValue)
        {
            _selectedObject = _serializedProperty.objectReferenceValue;
            _listViewItems.SelectSelectedObject();
            _dirty = true;
        }




        //if (!HasProperty(_serializedObject, _serializedProperty))
        //    Close();
        CheckEscAndCallOnCancelIfNotAlreadyCalled("OnInspectorUpdate");

        float windowWidth = GetWindowWidth();
        if (_lastWindowWidth != windowWidth)
        {
            if (windowWidth < 300)
                windowWidth = 300;
            float topFreeSize = windowWidth - 30;
            _searchField.style.width = topFreeSize * 0.6f;
            //_sizeSlider.style.width = topFreeSize * 0.4f;
            _lastWindowWidth = windowWidth;
            _dirty = true;
        }


        UpdateIfDirty(windowWidth);
    }

    private bool SerializedObjectOrSerializedPropertyGoneAwry()
    {
        try
        {
            if (_serializedProperty != null)
                if (_serializedObject != null)
                {
                    //Debug.Log($"{_serializedProperty.name} from {_serializedObject.ToString()}");
                    int propCount = HasProperty(_serializedObject, _serializedProperty);
                    //Debug.Log(propCount);
                    if (propCount < 0)
                        return LogAndClose();
                }
        }
        catch
        {
            return LogAndClose();
        }

        return false;

        bool LogAndClose()
        {
            Debug.Log("Looks like something happened with serialized property or serialized object, closing Picker window");
            Close();
            return true;
        }
    }

    private void UpdateIfDirty(float windowWidth)
    {
        if (_dirty)
        {
            /*
            var windowRect = position;
            var insideScrolleWidth = new StyleLength(new Length(position.width-10, LengthUnit.Pixel));
            _rootInsideScroller.style.width = insideScrolleWidth;
            Debug.Log(_rootInsideScroller.style.width);

            s_itemScale = s_itemScale < 1 ? 1 : s_itemScale;

            var newItemLength = new StyleLength(new Length(position.width/ s_itemScale, LengthUnit.Pixel));

            Debug.Log(_rootInsideScroller.Children().First().style.width);
            */

            _listViewItems.ScrollToSelected();

            float scale = s_itemScale < 1 ? 1 : s_itemScale;
            float newChildWidth = (windowWidth - 10) / scale;
            //_rootInsideScroller.style.width = newChildWidth;
            //foreach (var child in _rootInsideScroller.Children())
            //{
                ////child.style.width = newChildWidth;
            //}

            //ForceUpdate(_scrollView);

            //////_scroller.MarkDirtyRepaint();
            //////_rootInsideScroller.MarkDirtyRepaint();
            //////rootVisualElement.MarkDirtyRepaint();

            _dirty = false;
        }
    }


    private static int HasProperty(SerializedObject serializedObject, SerializedProperty property)
    {
        int count = 0;
        var next = serializedObject.GetIterator();
        while (next.NextVisible(true))
        {
            //Debug.Log($"{property.name},{property.propertyPath}   {next.name},{next.propertyPath}   {property == next}, {property.propertyPath == next.propertyPath}");
            if (property.propertyPath == next.propertyPath)
                return count;
            count++;
        }
        return -1;
    }

}
#endif

/*

        _alphabeticalToggle = new ToolbarToggle()
        {
            text = "A:",
            style =
            {
                color = new Color(0.2f, 0, 0),
                unityFontStyleAndWeight = FontStyle.Bold,
            },
            tooltip = "Sort elements alphabetically"
        };



//https://forum.unity.com/threads/how-to-refresh-scrollview-scrollbars-to-reflect-changed-content-width-and-height.1260920/
private static void ForceUpdate(ScrollView view)
{
    view.schedule.Execute(() =>
    {
        var fakeOldRect = Rect.zero;
        var fakeNewRect = view.layout;

        using var evt = GeometryChangedEvent.GetPooled(fakeOldRect, fakeNewRect);
        evt.target = view.contentContainer;
        view.contentContainer.SendEvent(evt);
    });
}
*/