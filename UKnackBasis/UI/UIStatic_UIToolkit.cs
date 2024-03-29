using UnityEngine.UIElements;
using System.Linq;
using UKnack.UI;

namespace UKnack.UI;

public static partial class UIStatic
{

    public static bool TryAddSafeAndOrderCorrectly(this VisualElement @this, VisualElement toAdd)
    {
        //UnityEngine.Debug.Log($"gonna add to {@this.name}{@this.GetType()} element: {toAdd.name}");
        if (@this == null)
            return false;
        if(@this is VisualElementSortedOnAddition sortedVE)
        {
            //UnityEngine.Debug.Log($"gonna add to sorted {sortedVE.name} as maybe orderred: {toAdd.name}");
            //if (toAdd is VisualElementSortedOnAddition toAddSorted)
            //    UnityEngine.Debug.Log(toAddSorted.OrderPreference);
            VisualElementSortedOnAddition.AddOrdered(sortedVE, toAdd);
            return true;
        }
        if(toAdd is VisualElementSortedOnAddition toAddSorted)
        {
            VisualElementSortedOnAddition.AddOrdered(@this, toAddSorted);
            return true;
        }
        @this.Add(toAdd);
        return true;
    }

    public static TElement GetOrThrow<TElement>(this VisualElement from, string what) where TElement : VisualElement
    {
        if (string.IsNullOrWhiteSpace(what))
            throw new System.Exception($"Identity of the received element must not be empty");
        var result = from.Q<TElement>(what);
        if (result == null)
            throw new System.Exception($"Cannot find {nameof(TElement)} with id {what} from: {from.name} of {from.GetType().Name}");
        return result;
    }

    /*
    public static void RemoveAllChildren(this VisualElement @this) // just @this.Clear();
    {
        var children = @this.Children().ToArray();
        foreach (var child in children)
        {
            child.RemoveFromHierarchy();
        }
    }
    */
}
