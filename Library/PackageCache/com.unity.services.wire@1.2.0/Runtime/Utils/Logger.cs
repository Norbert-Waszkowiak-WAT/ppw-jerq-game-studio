using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Unity.Services.Wire.Internal
{
    class Logger
    {
        // Change tag with your product name
        const string k_Tag = "[Wire]";
        // Change define with your product name
        const string k_VerboseLoggingDefine = "ENABLE_UNITY_WIRE_VERBOSE_LOGGING";


#if !ENABLE_UNITY_SERVICES_VERBOSE_LOGGING
        [Conditional(k_VerboseLoggingDefine)]
#endif
        public static void Log(object message) => Debug.unityLogger.Log(k_Tag, message);
#if !ENABLE_UNITY_SERVICES_VERBOSE_LOGGING
        [Conditional(k_VerboseLoggingDefine)]
#endif
        public static void LogWarning(object message) => Debug.unityLogger.LogWarning(k_Tag, message);
#if !ENABLE_UNITY_SERVICES_VERBOSE_LOGGING
        [Conditional(k_VerboseLoggingDefine)]
#endif
        public static void LogError(object message) => Debug.unityLogger.LogError(k_Tag, message);
#if !ENABLE_UNITY_SERVICES_VERBOSE_LOGGING
        [Conditional(k_VerboseLoggingDefine)]
#endif
        public static void LogException(Exception exception) => Debug.unityLogger.Log(LogType.Exception, k_Tag, exception);

        [Conditional("UNITY_ASSERTIONS")]
        public static void LogAssertion(object message) => Debug.unityLogger.Log(LogType.Assert, k_Tag, message);
    }
}
