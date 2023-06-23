using UnityEngine.UIElements;

namespace UKnack.UI
{
    public interface ILayoutDependant
    {
        // should be called first, or after LayoutGonnaBeDestroyedNow
        internal abstract void LayoutReady(VisualElement layout);

        // should be called once after all LayoutReadys of any kind, and before destruction of layout
        internal abstract void LayoutGonnaBeDestroyedNow();
    }
}
