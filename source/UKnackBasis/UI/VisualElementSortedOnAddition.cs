using System;
using System.Collections.Generic;
using System.Linq;
using UKnackBasis.UI;
using UnityEngine.UIElements;

namespace UKnack.UI;
public class VisualElementSortedOnAddition : VisualElement, IHaveOrderPreference
{
    public new class UxmlFactory : UxmlFactory<VisualElementSortedOnAddition, UxmlTraits> { }
    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlIntAttributeDescription m_IntTraitSortOrder =
            new UxmlIntAttributeDescription { name = "element-sorting-order", defaultValue = 0 };

        public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
        {
            get { yield break; }
        }
        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var sortedVE = ve as VisualElementSortedOnAddition;

            sortedVE.elementSortingOrder = m_IntTraitSortOrder.GetValueFromBag(bag, cc);
        }
    }

    public int elementSortingOrder { get; internal set; }
    public int orderPreference => elementSortingOrder;

    [Obsolete("Use extension method TryAddSafeAndOrderCorrectly or just virtual method AddOrdered instead, never use Add until it becames virtual")]
    public new void Add(VisualElement visualElement)
    {
        throw new Exception("Do not use this method, because it non virtual (Unity Design) at the moment of 2022.11.24");
    }
    public static void AddOrdered(VisualElement parent, VisualElement newChild) //TODO when Unity makes Add virtual, call this from Add
    {
        if (newChild == null)
            return;

        parent.Add(newChild);

        //Debug.Log($"added item with sorting order {VisualElementSortingOrder(newChild)}, commensing order matching:");
        //Debug.Log(string.Join(',', SelectFromHierarchyToList(hierarchy,el=>true).Select(x => VisualElementSortingOrder(x))));
        int currentChildCount = parent.childCount;

        if (currentChildCount <= 1)
            return;

        Hierarchy h = parent.hierarchy;

        var before = GetChildThatShouldBeBeforeNewChild(newChild, currentChildCount, ref h);
        if (before == null)
        {
            newChild.SendToBack();
            return;
        }

        newChild.PlaceInFront(before);
    }

    private static VisualElement GetChildThatShouldBeBeforeNewChild(VisualElement newChild, int currentChildCount, ref Hierarchy h)
    {
        int newChildOrder = SortingOrderOFVisualElement(newChild);
        List<VisualElement> filteredChildren = SelectFromHierarchyToList(ref h, el=> SortingOrderOFVisualElement(el)<=newChildOrder);
        //Debug.Log(string.Join(',', filteredChildren.Select(x=>VisualElementSortingOrder(x))));
        filteredChildren.Remove(newChild);
        if (filteredChildren.Count == 0)
        {
            //Debug.Log($"count was 0 for {newChildOrder}");
            return null;
        }
            
        return filteredChildren.OrderBy(el => SortingOrderOFVisualElement(el)).Last();
    }

    private static List<VisualElement> SelectFromHierarchyToList(ref Hierarchy h, Func<VisualElement, bool> include)
    {
        List<VisualElement> result = new List<VisualElement>();
        int count = h.childCount;
        for(int i=0; i<count; i++)
        {
            if (include(h[i]))
                result.Add(h[i]);
        }
        return result;
    }

    private static int SortingOrderOFVisualElement(VisualElement element)
    {
        int order = 0;
        if (element is IHaveOrderPreference sorted)
        {
            order = sorted.orderPreference;
        }
        return order;
    }

    public VisualElementSortedOnAddition(int elementSortingOrder) : this()
    {
        this.elementSortingOrder = elementSortingOrder;
    }
    public VisualElementSortedOnAddition() : base() { }
}
