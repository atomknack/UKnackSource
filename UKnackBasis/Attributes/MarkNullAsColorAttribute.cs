/// P: in future if needed - better and more full version with null findings located at
/// https://github.com/redbluegames/unity-notnullattribute/blob/master/Assets/RedBlueGames/NotNullAttribute/NotNullAttribute.cs
using System;
using System.Diagnostics;
using UnityEngine;

namespace UKnack.Attributes;

/// <summary>
/// MarkEmptyAsRed will draw red if value not set in UnityEditor
/// </summary>
[Obsolete("need retest")]
[Conditional("UNITY_EDITOR")]
[System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false)]
public class MarkNullAsColorAttribute : PropertyAttribute
{
    public readonly Color MarkColor;
    public readonly string NullValueTooltip;
    public MarkNullAsColorAttribute(float r, float g, float b, string nullValueTooltip = "")
    {
        MarkColor= new Color(r, g, b);
        NullValueTooltip= nullValueTooltip;
    }
}