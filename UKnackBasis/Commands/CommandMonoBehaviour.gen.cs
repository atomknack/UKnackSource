//----------------------------------------------------------------------------------------
// <auto-generated> This code was generated from Create_Commands
// Changes will be lost if the code is regenerated.</auto-generated>
//----------------------------------------------------------------------------------------
using UnityEngine;
using UKnack.Events;

namespace UKnack.Commands;

public abstract class CommandMonoBehaviour : MonoBehaviour, ICommand//, ISubscriberToEvent
{
    public abstract void Execute();
    //public virtual void OnEventNotification() => 
    //    Execute();
    public virtual string Description => this.GetType().ToString();
}

    