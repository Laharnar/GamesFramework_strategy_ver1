using UnityEngine;
/// <summary>
/// Connects AI behavour trees and movement controllers.
/// </summary>
public static class MovementHelper {
    public static void AttachDirection(this AITargeter target, MovementMode mode, Vector3 dir) {
        target.movingData.AttachPoints(mode, target.transform.position+dir);
    }
    public static void AttachPoint(this AITargeter target, MovementMode mode, Vector3 point) {
        target.movingData.AttachPoints(mode, point);
    }
    public static void AttachPathOfDirs(this AITargeter target, MovementMode mode, Vector3[] point) {
        for (int i = 0; i < point.Length; i++) {
            point[i] = target.transform.position + point[i];
        }
        target.movingData.AttachCommand(new PathCommand(new Path(point)));
    }
    public static void AttachPathOfPoints(this AITargeter target, MovementMode mode, Vector3[] point) {
        target.movingData.AttachCommand(new PathCommand(new Path(point)));
    }
}
