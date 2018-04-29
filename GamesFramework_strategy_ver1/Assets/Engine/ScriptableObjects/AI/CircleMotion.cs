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

