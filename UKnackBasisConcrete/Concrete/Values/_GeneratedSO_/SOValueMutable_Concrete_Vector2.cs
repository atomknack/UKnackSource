//----------------------------------------------------------------------------------------
// <auto-generated> This code was generated from Concrete_ValueSO
// Changes will be lost if the code is regenerated.</auto-generated>
//----------------------------------------------------------------------------------------

using UnityEngine;
using UKnack.Values;
using UKnack.Preconcrete.Values;

namespace UKnack.Concrete.Values
{

[CreateAssetMenu(fileName = "SOValue_Vector2", menuName = "UKnack/SOValueMutable/Vector2", order = 110)]
public sealed class SOValueMutable_Concrete_Vector2 : SOValueMutable<Vector2>
    {
        [SerializeField] 
        private USetOrDefault<Vector2> _value;

        public override Vector2 RawValue { get => _value.Value; protected set => _value.Value = value; }


    }

}
