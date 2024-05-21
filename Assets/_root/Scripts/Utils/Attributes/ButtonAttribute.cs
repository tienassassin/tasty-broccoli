using System;
using UnityEngine;

namespace CardMatch.Editor {
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class ButtonAttribute : PropertyAttribute
    {
        public string ButtonLabel { get; private set; }

        public ButtonAttribute(string buttonLabel = null)
        {
            ButtonLabel = buttonLabel;
        }
    }
}