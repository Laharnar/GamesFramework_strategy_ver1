using UnityEngine;

[UnityEngine.CreateAssetMenu(fileName = "New SimpleFiring", menuName = "Framework/AI/New SimpleFiring", order = 1)]
public class SimpleFiring : SOTreeLeaf, ISOTagNode {
    float t;
    public FloatData rate;

    public TransformData bullet;
    public override NodeResult Execute() {
        KeyCheck();
        AITargeter s = source as AITargeter;
        float t = times[s];
        if (Time.time >= t) {
            if (s.Firing == null) {
                Debug.Log("Missing firing reference, ignoring node.");
                return NodeResult.None;
            }
            bool limitOnLastFrame = s.Firing.reachedLimitLastFrame;
            bool limitedCurFrame = s.Firing.IsLimited();
            if (limitOnLastFrame && !limitedCurFrame) { // wait 1 full cd before re-firing.
                t = Time.time + (float)rate.GetValue();
            } else {
                if (!limitedCurFrame) {
                    Transform tr = s.Firing.Fire(bullet, s.transform);
                    //Transform tr = Instantiate((Transform)bullet.GetValue(), s.transform.position, s.transform.rotation);
                    //tr.GetComponent<AITargeter>().OnSpawned(s.GetComponentInParent<TreeBehaviour>());
                    t = Time.time + (float)rate.GetValue();
                }
            }
        }
        times[source as AITargeter] = t;
            
        return NodeResult.Success;
    }

    private static void KeyCheck() {
        if (source != null && !times.ContainsKey((AITargeter)source)) {
            times.Add((AITargeter)source, Time.time);
        }
    }

    public string _tag;
    public string tag {
        get {
            return _tag;
        }

        set {
            _tag = value;
        }
    }
    // source/time
    public static System.Collections.Generic.Dictionary<AITargeter, float> times= new System.Collections.Generic.Dictionary<AITargeter, float>();
    
}
