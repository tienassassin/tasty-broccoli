using System.Reflection;
using CardMatch.Editor;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonoBehaviour), true)]
public class ButtonEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        MonoBehaviour mono = (MonoBehaviour)target;
        MethodInfo[] methods = mono.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (MethodInfo method in methods) {
            var attributes = method.GetCustomAttributes(typeof(ButtonAttribute), true);
            foreach (var attribute in attributes) {
                ButtonAttribute buttonAttribute = (ButtonAttribute)attribute;
                string buttonLabel = string.IsNullOrEmpty(buttonAttribute.ButtonLabel) ? method.Name : buttonAttribute.ButtonLabel;

                if (GUILayout.Button(buttonLabel)) {
                    method.Invoke(mono, null);
                }
            }
        }
    }
}