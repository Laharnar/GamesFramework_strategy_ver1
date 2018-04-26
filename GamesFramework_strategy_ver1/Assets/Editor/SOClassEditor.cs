using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SOClass))]
public class SOClassEditor:Editor {
    SOClass source;
    string assemblyName = "GamesFramework_strategy_ver1";//Assembly-CSharp

    public void OnEnable() {
        source = (SOClass)target;
        SOClass.allClasses = ReflectionHelper.GetAllNonMonoNames(assemblyName).ToArray();
    }

    public override void OnInspectorGUI() {
        // Show all possible classes this SO can represent.
        string nspace = "...";
        if (GUILayout.Button("Reload")) {

            SOClass.allClasses = ReflectionHelper.GetAllNonMonoNames(assemblyName).ToArray();
        }
        EditorGUILayout.LabelField("Selected class: "+source.selected);
        EditorGUILayout.LabelField("Counted classes: "+ SOClass.allClasses.Length);
        source.selected = EditorGUILayout.Popup(source.selected, SOClass.allClasses);
        if (GUILayout.Button("Try init")) {
            try { 
                Type typ = typeof(PointTarget).Assembly.GetType(SOClass.allClasses[source.selected]);
                Debug.Log(typ);
                source.element =(PointTarget) Activator.CreateInstance(typ);
            } catch (Exception e) {
                Debug.Log(e);
            }
        }
        if (source.element != null) {
            List<string> list = new List<string>();
            EditorGUILayout.LabelField("Methods:");
            EditorGUI.indentLevel++;
            (from t in source.element.GetType().GetMethods()
                select t.Name).ToList().ForEach(t => EditorGUILayout.LabelField(t));
            EditorGUI.indentLevel--;

            EditorGUILayout.LabelField("Values:"+ source.element.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic).Length);
            EditorGUI.indentLevel++;
            (from t in source.element.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic)
             select t).ToList().ForEach(t => t.SetValue(source.element, ShowValue(source.element, t)));
            EditorGUI.indentLevel--;
        }
        if (GUI.changed)
            EditorUtility.SetDirty(source);
        
    }

    private object ShowValue(object element, object tt) {
        FieldInfo t = (FieldInfo)tt;
        object val = t.GetValue(element);
        if (val.GetType() == typeof(float)) {
            val = EditorGUILayout.FloatField(t.Name, (float)val);
        } else if (val.GetType() == typeof(int)) {
            val = EditorGUILayout.IntField(t.Name, (int)val);
        } else if (val.GetType() == typeof(Vector3)) {
            val = EditorGUILayout.Vector3Field(t.Name, (Vector3)val);
        } else if (typeof(Enum).IsAssignableFrom(val.GetType())) {
            val = EditorGUILayout.EnumPopup(t.Name, (Enum)val);
        } else if (val.GetType() == typeof(string)) {
            val = EditorGUILayout.TextField(t.Name, (string)val);
        }
        return val;
    }
}
