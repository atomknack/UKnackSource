using System;
using System.Collections.Generic;
using System.Text;

namespace UKnack.Preconcrete.Prefs;

public static class Loader
{
    public static object GetPref(string key, Type prefType) => 
        Settings.s_deserializer(Settings.s_loader(key), prefType);
    public static void SetPref(string key, object pref) => 
        Settings.s_saver(key, Settings.s_serializer(pref));
    public static void SavePending() => Settings.s_savePendingPrefs();
}
