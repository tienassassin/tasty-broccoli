using System;
using UnityEngine;

namespace CardMatch.Core {
    public class MessageDispatcher<T> where T : Delegate {
        private static T _handle;

        public static T Handle() {
            return _handle;
        }
        
        public static void AddListener(T callback) {
            _handle = (T)Delegate.Combine(_handle, callback);
        }
        
        public static void RemoveListener(T callback) {
            _handle = (T)Delegate.Remove(_handle, callback);
        }
    }

    public static class MessageID {
        public delegate void OnScoreUpdated(int score, int combo, int comboLife);

        public delegate void OnCardsLoaded(Sprite[] cardFaces);
    }
}