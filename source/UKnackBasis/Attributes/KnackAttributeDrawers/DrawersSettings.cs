#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace UKnack.Attributes.KnackAttributeDrawers;

internal static class DrawersSettings
{
    public static Color fieldExceptionAtVerification { get; private set; } = new Color(0.8f, 0, 0, 0.5f);
    public static Color fieldPassedVerification { get; private set; } = new Color(0.15f, 0.7f, 0.1f, 0.5f); 
    //private static Color s_passedVerification = Color.green;
    //private static Color s_exceptionAtVerification = Color.red;
    //internal static Color PassedVerification => s_passedVerification;
    //internal static Color ExceptionAtVerification => s_exceptionAtVerification;





    internal static void SetSettings(Color passedVerification, Color exceptionAtVerification)
    {
        //s_passedVerification = passedVerification;
        fieldExceptionAtVerification = exceptionAtVerification;

    }
}

#endif