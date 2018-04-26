using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
public static class ReflectionHelper {

    public static List<string> GetAllNames(string nspace) {
        /*var q = AppDomain.CurrentDomain.GetAssemblies()
                       .SelectMany(t => t.GetTypes())
                       .Where(t => t.IsClass && t.Namespace == nspace);
                       */
        Debug.Log(Assembly.GetExecutingAssembly().FullName);
        var q = from t in Assembly.GetExecutingAssembly().GetTypes()
                where t.IsClass //&& t.nam == nspace
                select t;

        List<string> list = new List<string>();
        q.ToList().ForEach(t => list.Add(t.ToString()));
        return list;
    }

    public static List<string> GetAllNonMonoNames(string nspace) {
        /*var q = AppDomain.CurrentDomain.GetAssemblies()
                       .SelectMany(t => t.GetTypes())
                       .Where(t => t.IsClass && t.Namespace == nspace);
                       */
        Debug.Log(Assembly.GetExecutingAssembly().FullName);
        var q = from t in Assembly.GetExecutingAssembly().GetTypes()
                where t.IsClass && !typeof(MonoBehaviour).IsAssignableFrom(t.GetType())//&& t.nam == nspace
                select t;

        List<string> list = new List<string>();
        q.ToList().ForEach(t => list.Add(t.ToString()));
        return list;
    }
}
/*
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
        
*/