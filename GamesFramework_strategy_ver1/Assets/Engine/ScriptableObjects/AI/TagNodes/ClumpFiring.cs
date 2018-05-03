using UnityEngine;
[UnityEngine.CreateAssetMenu(fileName = "New ClumpFiring", menuName = "Framework/AI/New ClumpFiring", order = 1)]
public class ClumpFiring : SOTreeLeaf, ISOTagNode {
    float t;
    public FloatData rate;

    public TransformData bullet;
    public int pts = 1;
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
           Vector3[] pts=  CircleMotion.GenerateCircleOfDirections(1, this.pts);
            for (int i = 0; i < this.pts; i++) {
                Transform b = Instantiate((Transform)bullet.GetValue(), s.transform.position,new Quaternion ());
                b.up = pts[i];
            }
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
