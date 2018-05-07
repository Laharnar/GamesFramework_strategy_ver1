using System.Linq;
using System.Collections.Generic;
using UnityEngine;

// Can't be static, cuz this data is saved on every root script(TreeBehaviour).
public partial class TreeBehaviour {

    /// <summary>
    /// Local collection of tagged sources on some root.
    /// Key:tag+scriptId
    /// </summary>
    Dictionary<string, List<AITargeter>> unitTargetLib = new Dictionary<string, List<AITargeter>>();

    public AITargeter[] GetAiSourcesByTag(string tag) {
        return unitTargetLib[tag].ToArray();
    }

    public void AddToLib(AITargeter targeter) {
        Debug.Log("Registered : ");
        string key = GetLibKey(targeter);
        if (!unitTargetLib.ContainsKey(key))
            unitTargetLib.Add(key, new List<AITargeter>());
        unitTargetLib[key].Add(targeter);
    }

    string GetLibKey(AITargeter targeter) {
        return targeter.tagTarget;
    }
}