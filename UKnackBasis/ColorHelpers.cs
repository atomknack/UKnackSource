using UnityEngine;

namespace UKnack;

public static partial class ColorHelpers
{
    public static Color InvertedRGB(Color color) =>
        new Color(1 - color.r, 1 - color.g, 1 - color.b, color.a);
    public static Color MultipliedRGB(Color color, float multiplier) =>
        new Color(color.r * multiplier, color.g * multiplier, color.b * multiplier, color.a);
    public static Color WithNewAlpha(Color color, float newAlpha) => new Color(color.r, color.g, color.b, newAlpha);
}