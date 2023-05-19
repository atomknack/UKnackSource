using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace UKnack.Attributes;

[Conditional("UNITY_EDITOR")]
[System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false)]
public class DisableEditingInPlaymodeAttribute : PropertyAttribute
{
}