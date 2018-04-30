﻿using UnityEngine;
/// <summary>
/// 
/// </summary>
/// <remarks>Relies on "factions" and "stats" to get cost.</remarks>
[UnityEngine.CreateAssetMenu(fileName = "New CostBasedFiring", menuName = "Framework/AI/New CostBasedFiring", order = 1)]
public class CostBasedFiring : SOTreeLeaf, ISOTagNode {
    public int cost=1;
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

    public bool InstantFailChecks(AITargeter s) {
        if ( Faction.GetMoney(s.stats.faction) < cost)
            return true;
        else
	    {
            Faction.UseMoney(s.stats.faction, cost);
        }
        return false;
    }

    public override NodeResult Execute() {

        KeyCheck();
        AITargeter s = source as AITargeter;

        float t = times[s];
        if (Time.time > t) {
            if (InstantFailChecks(s)) {
                return NodeResult.Failure;
            }
            Instantiate((Transform)bullet.GetValue(), s.transform.position, s.transform.rotation);
            t = Time.time + (float)rate.GetValue();
        }
        times[source as AITargeter] = t;
        return NodeResult.Success;
    }

    private static void KeyCheck() {
        if (source != null && !times.ContainsKey((AITargeter)source))
            times.Add((AITargeter)source, 0);
    }

    // source/time
    public static System.Collections.Generic.Dictionary<AITargeter, float> times = new System.Collections.Generic.Dictionary<AITargeter, float>();

}
