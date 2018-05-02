﻿using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New CircleMotion", menuName = "Framework/AI/New CircleMotion", order = 1)]
public class CircleMotion : SOTreeLeaf {
    public MovementMode mode;
    public int pointsDetail = 32;
    public float size = 1;
    public Vector3 axis;

    public override NodeResult Execute() {
        (source as AITargeter).moving.Attach(mode,
            GenerateCircleOfDirections(size, pointsDetail, axis));
        return NodeResult.Success;
    }
    public static Vector3[] GenerateCircleOfDirections(float size, int points, Vector3 axis) {
        Vector3[] pts = new Vector3[points];
        for (int i = 0; i < points; i++) {
            Vector3 vec = new Vector3(Mathf.Cos((float)i / (float)points * 2 * Mathf.PI), Mathf.Sin((float)i / (float)points * Mathf.PI * 2));
            Quaternion q = Quaternion.Euler(axis);
            
            pts[i] = q * vec * size;

            //Vector3 rot = Vector3.up + Vector3.up - axis;
        }
        return pts;
    }

    
    internal static Vector3[] GenerateCircleOfDirections(Vector3 targetPos, Vector3 startPos, float size, int points, Vector3 axis) {
        Vector3[] cpts = new Vector3[points];
        for (int i = 0; i < points; i++) {
            Vector3 vec = new Vector3(Mathf.Cos((float)i / (float)points * 2 * Mathf.PI), Mathf.Sin((float)i / (float)points * Mathf.PI * 2));
            Quaternion q = Quaternion.Euler(axis);

            cpts[i] += q * vec * size;

            //Vector3 rot = Vector3.up + Vector3.up - axis;
        }
        Vector3[] dirs = new Vector3[points+1];
        dirs[0] = startPos + (targetPos-startPos) + cpts[0];
        for (int i = 0; i < points-1; i++) {
            dirs[i+1] = cpts[i+1] - cpts[i];
        }
        dirs[points] = dirs[points - 1] - dirs[0];
        return dirs;
    }

    public static Vector3[] GenerateCircleOfDirections(float size, int points) {
        Vector3[] pts = new Vector3[points];
        for (int i = 0; i < points; i++) {
            Vector3 vec = new Vector3(Mathf.Cos((float)i / (float)points * 2 * Mathf.PI), Mathf.Sin((float)i / (float)points * Mathf.PI * 2));
            pts[i] = vec * size;
            //Vector3 rot = Vector3.up + Vector3.up - axis;
        }
        return pts;
    }

}

public class DictionaryList<K, VItem> {
    Dictionary<K, List<VItem>> dict = new Dictionary<K, List<VItem>>();
    public void Add(K key, VItem val) {
        if (!dict.ContainsKey(key)) {
            dict.Add(key, new List<VItem>());
        }
        dict[key].Add(val);
    }
}