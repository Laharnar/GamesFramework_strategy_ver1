﻿using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

// Ai root.
public partial class TreeBehaviour : AITargeter {
    public SOTree tree;

    private void Start() {
        AITargeter[] t = gameObject.GetComponentsInChildren<AITargeter>();
        foreach (var item in t) {
            if (item == this)
                continue;
            AddToLib(item);
            AddToLib(item.tagTarget, this);
        }
    }

    private void Update() {
        SOTree.source = this;
        if (tree)
            tree.StandardNodeExecute();
    }

}


public partial class TreeBehaviour {

    /// <summary>
    /// Global Ai library with all objects in scene.
    /// </summary>
    public static Dictionary<string, List<TreeBehaviour>> aiLib = new Dictionary<string, List<TreeBehaviour>>();

    public static TreeBehaviour[] GetAIFromLib(string tag) {
        return aiLib[tag].ToArray();
    }

    public static void AddToLib(string asKey, TreeBehaviour targeter) {
        string key = asKey;
        if (!aiLib.ContainsKey(key))
            aiLib.Add(key, new List<TreeBehaviour>());
        aiLib[key].Add(targeter);
    }

    static string GetLibKey(TreeBehaviour targeter) {
        return targeter.tagTarget;
    }

    public static AITargeter[] GetAllTargetersByTag(string tag) {
        List<AITargeter> allC = new List<AITargeter>();
        if (aiLib.ContainsKey(tag)) {
            TreeBehaviour[] c = aiLib[tag].ToArray();
            foreach (var item in c) {
                AITargeter[] utl = item.GetAiSourcesByTag(tag);
                allC.AddRange(utl);
            }
        }
        return allC.ToArray();
    }

    internal static AITargeter[] GetAiSourcesByTagUnderRoot(TreeBehaviour source, string tag) {
        List<AITargeter> result = new List<AITargeter>();
        if (aiLib.ContainsKey(tag)) {
            result.AddRange(source.GetAiSourcesByTag(tag));
        }
        return result.ToArray();
    }

}

public partial class TreeBehaviour {

    /// <summary>
    /// Local collection of tagged sources on this root.
    /// Key:tag+scriptId
    /// </summary>
    Dictionary<string, List<AITargeter>> unitTargetLib = new Dictionary<string, List<AITargeter>>();

    private AITargeter[] GetAiSourcesByTag(string tag) {
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