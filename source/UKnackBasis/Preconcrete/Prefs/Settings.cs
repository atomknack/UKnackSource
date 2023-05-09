using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

namespace UKnack.Preconcrete.Prefs;

internal static class Settings
{
    internal delegate void SaverDelegate(string key, string serializedValue);
    internal delegate string LoaderDelegate(string key);

    internal static Func<object, string> s_serializer = DefaultMethods.DefaultXMLSerializer;
    internal static Func<string, Type, object> s_deserializer = DefaultMethods.DefaultXMLDeserializer;

    internal static SaverDelegate s_saver = DefaultMethods.DefaultSaver;
    internal static LoaderDelegate s_loader = DefaultMethods.DefaultLoader;
    internal static Action s_savePendingPrefs = DefaultMethods.DefaultSavePending;

    internal static class DefaultMethods
    {
        internal static string DefaultXMLSerializer(object obj)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, obj);
                return textWriter.ToString();
            }
        }

        internal static object DefaultXMLDeserializer(string xml, Type type)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(type);

            using (StringReader reader = new StringReader(xml))
            {
                return xmlSerializer.Deserialize(reader);
            }
        }

        internal static void DefaultSavePending() =>
            PlayerPrefs.Save();
        internal static void DefaultSaver(string key, string value) =>
            PlayerPrefs.SetString(key, value);
        internal static string DefaultLoader(string key)
        {
            if (PlayerPrefs.HasKey(key) == false)
                throw new Exception($"PlayerPrefs has no key: {key}");
            return PlayerPrefs.GetString(key);
        }
    }

}
