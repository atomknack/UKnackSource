using UKnack.Common;
using UnityEngine;

namespace UKnack.Concrete.Commmon
{
    [CreateAssetMenu(fileName = "DebugScriptableLogger", menuName = "UKnack/Common/DebugScriptableLogger")]
    public class ScriptableLoggerDebugLogOnlyInEditor : ScriptableObjectWithReadOnlyName
    {
        public string loggerPrefix;

        public virtual void LogValue(string value)
        {
            DoActualLogging($"{name}{(string.IsNullOrEmpty(loggerPrefix) ? "" : ",")}{loggerPrefix}: {value}");
        }

        public virtual void LogValue(bool value)
        {
            DoActualLogging($"{name}{(string.IsNullOrEmpty(loggerPrefix) ? "" : ",")}{loggerPrefix}, bool value: {value}");
        }

        private void DoActualLogging(string str)
        {
#if UNITY_EDITOR
            Debug.Log(str);
#endif
        }

    }
}