//----------------------------------------------------------------------------------------
// <auto-generated> This code was generated from EventScriptableObject
// Changes will be lost if the code is regenerated.</auto-generated>
//----------------------------------------------------------------------------------------
    
using System;
using UnityEngine;
using UKnack.Values;
using UKnack.Common;

namespace UKnack.Events;

public abstract class SOPublisher<T1,T2> : ScriptableObjectWithReadOnlyName, IPublisher<T1,T2>
{  
    public abstract void Publish(T1 t1,T2 t2);
}

