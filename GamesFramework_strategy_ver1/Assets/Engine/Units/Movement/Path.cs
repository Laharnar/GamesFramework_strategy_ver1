using System;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Path {
    [SerializeField]List<Vector3> nodes = new List<Vector3>();

    public Path(Vector3[] point) {
        nodes.AddRange(point);
    }

    public void AddNode(Vector3 node) {
        nodes.Add(node);
    }

    public List<Vector3> GetNodes()  {
        return nodes;
    }
}
