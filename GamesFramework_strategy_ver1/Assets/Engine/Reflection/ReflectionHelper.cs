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