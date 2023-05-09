using System;
using UKnack.Attributes;
using UKnack.Commands;
using UKnack.Events;
using UnityEngine;

#if UNITY_EDITOR
//using System.Reflection;
#endif

namespace UKnack.Preconcrete.Commands;

#if UNITY_EDITOR
//    [InitializeOnLoad]
#endif
public abstract class AbstractCommandSubscribedToSOEvent : ICommandMonoBehaviour
{
    [SerializeField]
    [ValidReference(typeof(IEvent))]
    private SOEvent _subscribedTo;

    protected virtual void OnEnable()
    {
        Subscribe(_subscribedTo, this);
    }
    protected virtual void OnDisable()
    {
        UnSubscribe(_subscribedTo, this);
    }
    public static void Subscribe(SOEvent soevent, ISubscriberToEvent subsriber)
    {
        if (soevent == null)
            throw new ArgumentNullException(nameof(soevent));
        soevent.Subscribe(subsriber);
    }

    public static void UnSubscribe(SOEvent soevent, ISubscriberToEvent subsriber)
    {
        soevent.UnsubscribeNullSafe(subsriber);
    }

    /*
#if UNITY_EDITOR
    Action onEn;
    Action onDe;
    public AbstractCommandSubscribedToSOEvent()
    {
        onEn = OnEnable;
        onDe = OnDisable;
        EditorApplication.playModeStateChanged += LogPlayModeState;
    }
    ~AbstractCommandSubscribedToSOEvent()
    {
        EditorApplication.playModeStateChanged -= LogPlayModeState;
    }

    private void LogPlayModeState(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode)
            EditorApplication.playModeStateChanged -= LogPlayModeState;

        if (state != PlayModeStateChange.EnteredPlayMode)
            return;

        foreach (var m in typeof(AbstractCommandSubscribedToSOEvent).GetMethods())
            Debug.Log(m);

        Debug.Log("");

        foreach (var m in GetType().GetMethods())
            Debug.Log(m);



       Debug.Log(GetType().GetMethod("Execute", BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy));

        if (IsOverride(this.GetType().GetMethod("OnEnable")))
            Debug.Log("To work properly AbstractCommandSubscribedToSOEvent OnEnable method should not be overriden in child classes");
        if (IsOverride(this.GetType().GetMethod("OnDisable")))
            Debug.Log("To work properly AbstractCommandSubscribedToSOEvent OnEnable method should not be overriden in child classes");
        Debug.Log(state);
    }
    public static bool IsOverride(MethodInfo m)
    {
        return m.GetBaseDefinition().DeclaringType != m.DeclaringType;
    }
#endif

    */

}