using UnityEngine;
public static class VectorHelper { 
    public static Vector3[] AddVector(Vector3 vec, Vector3[] list) {
        for (int i = 0; i < list.Length; i++) {
            list[i] = list[i] + vec;
        }
        return list;
    }

    public static Vector3 RotatePointAroundAxis(Vector3 point, float angle, Vector3 axis) {
        Quaternion q = Quaternion.AngleAxis(angle, axis);
        return q * point; //Note: q must be first (point * q wouldn't compile)
    }
}

