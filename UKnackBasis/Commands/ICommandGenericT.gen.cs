//----------------------------------------------------------------------------------------
// <auto-generated> This code was generated from Create_Commands
// Changes will be lost if the code is regenerated.</auto-generated>
//----------------------------------------------------------------------------------------
using UKnack.Common;

namespace UKnack;

public interface ICommand<T> : IHaveDescription
{
    void Execute(T t);
    ///public string Description { get; } //inherited from IHaveDescription
}


