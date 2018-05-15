using System;
using System.Collections.Generic;
using UnityEngine;
public class CommandData : MonoBehaviour {
    [SerializeField] internal List<DirectionCommand> directions = new List<DirectionCommand>();

    public bool IsIdle { get { return directions.Count == 0; } }

    public bool IsAlmostIdle { get { return directions.Count == 1; } }

    public DirectionCommand activeDir { get { return directions[0]; } }

    /// <summary>
    /// Sums some number of directions already saved for final vector.
    /// UNTESTED.
    /// </summary>
    /// <param name="count">How many should be summed. -1:all</param>
    public Vector3 SumDirection(int count = -1) {
        if (count == -1) {
            count = directions.Count;
        }
        Vector3 sum = Vector3.zero;
        for (int i = 0; i < count; i++) {
            sum += directions[count].dir;
        }
        return sum;
    }

    /// <summary>
    /// Add directions.
    /// </summary>
    /// <param name="nMode"></param>
    /// <param name="directions"></param>
    public void Attach(MovementMode nMode, params Vector3[] directions) {
        for (int i = 0; i < directions.Length; i++) {
            //reversedDirections.Insert(0, new DirectionCommand(directions[i]) { mode = nMode });
            Vector3[] dirs = Chop(directions[i]);
            for (int j = 0; j < dirs.Length; j++) {
                this.directions.Add(new DirectionCommand(dirs[j]) { mode = nMode });
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dirs"></param>
    /// <returns>Long vector chopped into short ones. </returns>
    /// <remarks>Might leave a small error because of division.</remarks>
    public Vector3[] Chop(Vector3 dirs) {
        List<Vector3> r = new List<Vector3>();
        float sqrMag = dirs.sqrMagnitude;
        if (sqrMag > 400) {
            float len = dirs.magnitude;
            float steps = len / 20f;
            for (int i = 0; i < steps; i++) {
                r.Add(dirs / steps);
            }
        } else {
            return new Vector3[1] { dirs };
        }
        return r.ToArray();
    }

    private void OnDrawGizmosSelected() {
        Vector3 start = transform.position;
        for (int i = 0; i < directions.Count; i++) {
            Gizmos.DrawRay(start, directions[i].dir);
            start += directions[i].dir;
        }
    }

    /// <summary>
    /// Fixes current path with new one.
    /// </summary>
    /// <param name=""></param>
    public void Fix(MovementMode nMode, Vector3[] path, int num = 1) {
        for (int i = 0; i < num && directions.Count > 0; i++) {
            directions.RemoveAt(0);
        }
        for (int i = 0; i < path.Length; i++) {
            directions.Add(new DirectionCommand(path[i]) { mode = nMode });
        }
    }

    internal void RemoveLast() {
        directions.RemoveAt(directions.Count-1);
    }


    /// <summary>
    /// Convert to directions and add them.
    /// </summary>
    /// <param name="points"></param>
    public void AttachPoints(MovementMode nMode, params Vector3[] points) {
        if (points.Length > 0) {
            // note: removed connection between cur pos and final point.
            //Vector3 finalPoint = SumDirection() + startPoint;
            Attach(nMode, points[0] - transform.position);//- finalPoint);
            for (int i = 1; i < points.Length; i++) {
                Attach(nMode, points[i] - points[i - 1]);
            }
        }
    }
}
/*public partial class Movement : MonoBehaviour {
    public Transform xRot, yRot, zRot;
    [HideInInspector] public Vector3 targetPoint;
    public Vector3 targetDir = Vector3.one;

    private void AxisBasedRotation() {

        Vector3 dir = targetPoint - transform.position;//targetDir;
        //targetPoint = transform.position + dir;
        if (yRot) {
            Vector3 yDir = targetPoint - yRot.position;
            //yDir.y = yRot.right.y;
            yRot.right = yDir;
            yRot.transform.localRotation = Quaternion.Euler(0, yRot.transform.localEulerAngles.y, 0);
        }
        if (xRot) {// untested
            Vector3 xDir = targetPoint - xRot.position;
            //xDir.x = xRot.right.x;
            xRot.right = xDir;
            xRot.transform.localRotation = Quaternion.Euler(xRot.transform.localEulerAngles.x, 0, 0);
        }
        if (zRot) {
            Vector3 zDir = targetPoint - zRot.position;
            //zDir.z = zRot.right.z;
            //zDir.y = zRot.parent.right.y;// zRot.right.z;
            zRot.right = zDir;
            zRot.transform.localRotation = Quaternion.Euler(0, 0, zRot.transform.localEulerAngles.z);
        }
    }
}
*/