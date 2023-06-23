using UnityEngine.UIElements;

namespace UKnack.UI
{
    public interface ISimpleDependant
    {
        // should be called first, or after LayoutGonnaBeDestroyedNow
        abstract void LayoutReady(VisualElement layout);

        // should be called once after all LayoutReadys of any kind, and before destruction of layout
        abstract void LayoutGonnaBeDestroyedNow();
    }
}
