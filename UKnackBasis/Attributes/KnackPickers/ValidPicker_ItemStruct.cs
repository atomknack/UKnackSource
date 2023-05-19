#if UNITY_EDITOR
using System;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

namespace UKnack.Attributes.KnackPickers;

public partial class ValidPicker
{
    private struct ItemStruct: IComparable<ItemStruct>
    {
        //public VisualElement VE;
        public UnityEngine.Object Obj;
        public UnityObjectsFinder.ObjectLocation Location;

        public int CompareTo(ItemStruct other) =>Obj.name.CompareTo(other.Obj.name);
        public string ToListItemName() => 
            $"{(Obj == null ? "Null" : Obj)} ... of origin: {Location}";
    }
}
#endif