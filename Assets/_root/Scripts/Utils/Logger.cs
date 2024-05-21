using UnityEngine;
using System.Diagnostics;

namespace CardMatch.Utils {
    public static class Logger {
        [Conditional("UNITY_EDITOR")]
        public static void Log(object message)
        {
            UnityEngine.Debug.Log(message);
        }
    }
}