using System.Collections.Generic;
using UnityEngine;

namespace CardMatch.Core {
    public static class Common {
        private static Dictionary<float, WaitForSeconds> _waitDictionary = new();

        public static WaitForSeconds GetWait(float time) {
            if (_waitDictionary.TryGetValue(time, out var wait)) return wait;
            _waitDictionary[time] = new WaitForSeconds(time);
            return _waitDictionary[time];
        }
    }
}