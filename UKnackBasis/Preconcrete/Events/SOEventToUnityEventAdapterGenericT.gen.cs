//----------------------------------------------------------------------------------------
// <auto-generated> This code was generated from Preconcrete_SOEventToUnityEventAdapter
// Changes will be lost if the code is regenerated.</auto-generated>
//----------------------------------------------------------------------------------------
    
using System;
using UnityEngine;
using UnityEngine.Events;
using UKnack.Attributes;
using UKnack.Events;

namespace UKnack.Preconcrete.Events
{

public abstract class SOEventToUnityEventAdapter<T> : MonoBehaviour
{
    [SerializeField]
    protected UnityEvent<T> _unityEvent;

    protected abstract IEvent<T> _iEvent { get; }
    
    protected void OnEnable()
    {
        if (_unityEvent == null)
            throw new ArgumentNullException(nameof(_unityEvent));
            
        _iEvent.Subscribe(_unityEvent);
    }

    protected void OnDisable()
    {
        if (_unityEvent == null)
            throw new ArgumentNullException(nameof(_unityEvent));
            
        _iEvent.UnsubscribeNullSafe(_unityEvent);
    }
}

}
