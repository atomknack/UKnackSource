using UnityEngine;
using UnityEngine.UIElements;
using UKnack.Values;
#nullable disable
using UKnack.Events;
using UKnack.UI;
using UKnack.Attributes;
using UKnack.Preconcrete.Values;

namespace UKnack.Preconcrete.UI.SimpleToolkit;

public abstract class EffortlessUpdaterFromValueSO<T> : MonoBehaviour
{
    [SerializeField]
    [ProvidedComponent]
    [DisableEditingInPlaymode]
    protected UIDocument _document;

    [SerializeField] 
    protected string _nameOfUpdatedUI;

    protected VisualElement _currentUpdatedUIElement;

    protected IValue<T> _value;
    protected abstract IValue<T> GetValidValueProvider();
    protected abstract string RawValueToStringConversion(T value);

    protected void OnEnable()
    {
        _document = ProvidedComponentAttribute.Provide(gameObject, _document);
        VisualElement layout = _document.rootVisualElement;
        _currentUpdatedUIElement = layout.TryFindSomeKindOfTextStorage(_nameOfUpdatedUI);
        UIContainerValidation(_currentUpdatedUIElement, _nameOfUpdatedUI);

        _value = GetValidValueProvider();
        UpdateUIBasedOnStorage(_value.GetValue());
        _value.Subscribe(UpdateUIBasedOnStorage);
    }

    private void UpdateUIBasedOnStorage(T value)
    {
        _currentUpdatedUIElement.TryAssignTextWithoutNotification(RawValueToStringConversion(value));
    }

    protected void OnDisable()
    {
        UIContainerValidation(_currentUpdatedUIElement, _nameOfUpdatedUI);
        _value.UnsubscribeNullSafe(UpdateUIBasedOnStorage);
    }

    private static void UIContainerValidation(VisualElement storage, string nameOfStorage)
    {
        if (string.IsNullOrEmpty(nameOfStorage))
            throw new System.NullReferenceException($"name of storage is null or empty");
        if (string.IsNullOrWhiteSpace(nameOfStorage))
            throw new System.NullReferenceException($"name of storage is null or whitespaces");
        if (storage == null)
            throw new System.NullReferenceException($"Suitable text storage: {nameOfStorage} not found");
    }
}