using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace UKnack.Attributes;

[Conditional("UNITY_EDITOR")]
[System.AttributeUsage(System.AttributeTargets.Field, Inherited = true, AllowMultiple = false)] //Inherited = true, true is default anyway
public abstract class OneOfMarkingsAttribute : PropertyAttribute
{
}
