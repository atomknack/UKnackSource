using System;
using System.Linq;

namespace UKnack;

public static class EnumCachedDistinct<GenericEnum> where GenericEnum : struct, Enum
{
    //private static Func<GenericEnum, int> s_intConversionFunc;
    private static string[] s_rawNames;
    private static string[] s_namesSortedByValue;
    private static int[] s_valuesSorted;

    public static ReadOnlySpan<string> Names { get { return s_rawNames;} }
    public static string GetName(GenericEnum @enum) =>
        s_namesSortedByValue[Array.BinarySearch<int>(s_valuesSorted, ToInt(@enum))];

    private static int ToInt(GenericEnum value) => value.GetHashCode(); //s_intConversionFunc(value);

    static EnumCachedDistinct()
    {
        if (!typeof(GenericEnum).IsEnum)
            throw new ArgumentException($"{nameof(GenericEnum)} must be an enumerated type, to be used as {nameof(EnumCachedDistinct<GenericEnum>)}");

        //s_intConversionFunc = GenerateFunc();

        string[] rawNames = Enum.GetNames(typeof(GenericEnum));
        int[] unsortedValues = InitValuesOrThrow(rawNames);

        s_rawNames = rawNames;
        s_valuesSorted = unsortedValues.OrderBy(x => x).ToArray();
        s_namesSortedByValue = SortedNames(rawNames, unsortedValues, s_valuesSorted);
        return;

        static int[] InitValuesOrThrow(string[] enumNames)
        {
            var result = new int[enumNames.Length];
            for (int i = 0; i < result.Length; i++)
                result[i] = ToInt(Enum.Parse<GenericEnum>(enumNames[i]));
            CheckNoDuplicateValuesOrThrow(result, enumNames);
            return result;

            static void CheckNoDuplicateValuesOrThrow(int[] valuesToCheck, string[] names)
            {
                foreach (int value in valuesToCheck)
                    if (valuesToCheck.Count(x => x == value) != 1)
                    {
                        throw new Exception($"{nameof(EnumCachedDistinct<GenericEnum>)} cannot be used with {nameof(GenericEnum)}: " +
                            $"{string.Join(",", names.Where((name, ind) => valuesToCheck[ind] == value))}" +
                            $" have same value {value}");
                    }
            }
        }

        static string[] SortedNames(string[] rawNames, int[] unsortedValues, int[] valuesSorted)
        {
            if (unsortedValues.SequenceEqual(s_valuesSorted))
            {
                return rawNames;
            }
            string[] result = new string[rawNames.Length];
            for (int i = 0; i < rawNames.Length; ++i)
            {
                var unsortedIndex = Array.FindIndex(unsortedValues, x => x == valuesSorted[i]);
                result[i] = rawNames[unsortedIndex];
            }
            return result;
        }
    }

    //private static Func<GenericEnum, int> GenerateFunc(GenericEnum enumValue) => ;
    /*
    //CompiledLambdaFunc test and comparation: https://stackoverflow.com/a/72838343
    //from: https://stackoverflow.com/questions/16960555/how-do-i-cast-a-generic-enum-to-int
    [Obsolete("Need unity il2cpp testing")]
    //https://forum.unity.com/threads/are-c-expression-trees-or-ilgenerator-allowed-on-ios.489498/page-2
    private static Func<GenericEnum, int> GenerateFunc()
    {
        var inputParameter = Expression.Parameter(typeof(GenericEnum));

        var body = Expression.Convert(inputParameter, typeof(int)); // means: (int)input;

        var lambda = Expression.Lambda<Func<GenericEnum, int>>(body, inputParameter);

        var func = lambda.Compile();

        return func;
    }
    */
}
