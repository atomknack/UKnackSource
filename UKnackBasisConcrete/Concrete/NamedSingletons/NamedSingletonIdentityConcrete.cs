using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using UKnack.Attributes;
using UKnack.Common;
using UKnack.Preconcrete.NamedSingletons;
using UnityEngine;

namespace UKnack.Concrete.NamedSingletons
{
    [Obsolete("Not tested")]
    [CreateAssetMenu(fileName = "NamedSingletonIdentity", menuName = "UKnack/NamedSingletonIdentity", order = 990)]
    public class NamedSingletonIdentityWithLogInEditor : NamedSingletonIdentity
    {
        [SerializeField] 
        private string _description = string.Empty;

        [SerializeField]
        [Tooltip("set true if you want to log in Editor every call to: IsAlreadyRegistered, TryRegister, Unregister")] 
        private bool _logAttemptsInEditor = false;

        [SerializeField] 
        private bool _addDescritionToLogInfo = false;
        public override string Description => _description;

        public override bool IsAlreadyRegistered()
        {
            string info = GetCallerInfo();
            return LogCallerAttempt(info, base.IsAlreadyRegistered());
        }

        public override bool TryRegister(out SuccessfullyRegisteredTag registeredReference, object value = null)
        {
            string info = GetCallerInfo();
            return LogCallerAttempt(info, base.TryRegister(out registeredReference, value), value);
        }

        public override bool Unregister(SuccessfullyRegisteredTag registeredReference, object value = null)
        {
            string info = GetCallerInfo();
            return LogCallerAttempt(info, base.Unregister(registeredReference, value), value);
        }

        private bool LogCallerAttempt(string callerInfo, bool attempt, object value = null)
        {
#if UNITY_EDITOR
            if (_logAttemptsInEditor)
            {
                string logInfo = $"{name}, {GetCallerObjectInfo(value)}, with result = {attempt}: {callerInfo}";
                if (_addDescritionToLogInfo)
                    logInfo += $"; {name}-{Description}";
                UnityEngine.Debug.Log(logInfo);
            }
#endif
            return attempt;
        }

        private static string GetCallerInfo()
        {
            string result = string.Empty;
#if UNITY_EDITOR
            StackTrace trace = new StackTrace();
            var mi = trace.GetFrame(2).GetMethod();
            result = $"{mi.Name} from {mi.DeclaringType} by {trace.GetFrame(1).GetMethod().Name}";
#endif
            return result;
        }

        private static string GetCallerObjectInfo(object obj)
        {
            if (obj == null)
                return "";
            if (obj is UnityEngine.Object uo)
                return $"from '{obj.GetType().Name}-{uo.name}',";
            return $"from '{obj.GetType().FullName}'";
        }
    }
}
