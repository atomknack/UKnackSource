using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UKnack.Attributes.KnackAttributeDrawers;

public abstract partial class OneOfMarksAttributeDrawer
{
    internal static class MarkAttributeTypes
    {
        private static Type[] s_markAttributes = new[] {
        typeof(ValidReferenceAttribute),
        typeof(ProvidedComponentAttribute),
        typeof(MarkNullAsColorAttribute),
        };
        internal static void UpdateMarkAttributes_ThisWillAffectAllDerivedFrom_OneOfMarksAttributeDrawer_UseOnlyForUserSpecialCases(Type[] newMarkAttributes) =>
            s_markAttributes = newMarkAttributes;

        public static int countOfMarks(PropertyAttribute[] attributes) =>
            GetMarks(attributes).Length;
        public static Type[] GetMarks(PropertyAttribute[] attributes)
        {
            List<Type> result = new List<Type>();
            foreach (var attr in attributes)
            {
                Type attrType = attr.GetType();
                int count = 0;
                foreach (var mark in s_markAttributes)
                    if (mark.IsAssignableFrom(attrType))
                        count++;
                if (count > 0)
                    result.Add(attrType);
            }
            return result.ToArray();
        }
    }
}