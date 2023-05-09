using UnityEngine.UIElements;

namespace UKnack.UI;

public interface IDependant
{
    // should not be called first, or after LayoutGonnaBeDestroyedNow
    abstract void LayoutReady(VisualElement layout);
    // should be called after LayoutReady and before LayoutGonnaBeDestroyedNow
    abstract void LayoutReadyAndAllDependantsCalled(VisualElement layout);
    // should be called once after LayoutReadyAndAllDependantsCalled
    abstract void LayoutGonnaBeDestroyedNow();
}