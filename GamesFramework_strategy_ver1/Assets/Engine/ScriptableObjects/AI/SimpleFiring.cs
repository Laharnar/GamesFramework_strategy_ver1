﻿using UnityEngine;
[UnityEngine.CreateAssetMenu(fileName = "New SimpleFiring", menuName = "Framework/AI/New SimpleFiring", order = 1)]
public class SimpleFiring : SOTreeLeaf, ISOTagNode {
    float t;
    public FloatData rate;

    public TransformData bullet;

    public string _tag;
    public string tag {
        get {
            return _tag;
        }

        set {
            _tag = value;
        }
    }

    public override NodeResult Execute() {
        KeyCheck();
        AITargeter s = source as AITargeter;
        float t = times[s];
        if (Time.time > t) {
            Instantiate((Transform)bullet.GetValue(), s.transform.position, s.transform.rotation);
            t = Time.time + (float)rate.GetValue() ;
        }
        times[source as AITargeter] = t;
        return NodeResult.Success;
    }

    private static void KeyCheck() {
        if (source != null && !times.ContainsKey((AITargeter)source))
            times.Add((AITargeter)source, 0);
    }

    // source/time
    public static System.Collections.Generic.Dictionary<AITargeter, float> times= new System.Collections.Generic.Dictionary<AITargeter, float>();
    
}
